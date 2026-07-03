using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using Kara.Contexts;
using Kara.Messages;
using Kara.Models;
using Kara.Services;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kara.ViewModels
{
    public partial class ItemsViewModel : ObservableRecipient, IEntriesViewModel<Item>, IRecipient<CategoryAddedMessage>, IRecipient<CategoryRemovedMessage>
    {
        private readonly IDbContextFactory<KaraContext> dbContextFactory;
        private readonly ILoggingService<Log> loggingService;

        public ItemsViewModel(IDbContextFactory<KaraContext> dbContextFactory, ILoggingService<Log> loggingService)
            : base()
        {
            this.dbContextFactory = dbContextFactory;
            this.loggingService = loggingService;

            Entities = [];

            Messenger.Register<CategoryAddedMessage>(this);
            Messenger.Register<CategoryRemovedMessage>(this);
        }

        ~ItemsViewModel()
        {
            Messenger.UnregisterAll(this);
        }

        [ObservableProperty]
        public partial ObservableCollection<Item> Entities { get; set; }

        [RelayCommand(AllowConcurrentExecutions = false)]
        public async Task LoadAsync()
        {
            using KaraContext context = await dbContextFactory.CreateDbContextAsync();

            Entities.Clear();

            await foreach (
                Item item in
                context
                .Items
                .Include(x => x.Category)
                .AsAsyncEnumerable())
                Entities.Add(item);

            AddCommand.NotifyCanExecuteChanged();
        }

        [RelayCommand(AllowConcurrentExecutions = false, CanExecute = nameof(CanAdd))]
        public async Task AddAsync()
        {
            using KaraContext context = await dbContextFactory.CreateDbContextAsync();

            Category defaultCategory = context.Categories.First();
            Item newItem = new()
            {
                CategoryId = defaultCategory.Id,
            };

            context.Add(newItem);
            await context.SaveChangesAsync();
        }

        [RelayCommand(CanExecute = nameof(CanOpenDetail))]
        public void Detail(Item? item)
        {
            if (item == null)
                return;
            Messenger.Send(new ItemDetailOpenMessage(item));
        }

        [RelayCommand(AllowConcurrentExecutions = false, CanExecute = nameof(CanRemove))]
        public async Task RemoveAsync(Item? item)
        {
            if (item is Item itm)
            {
                using (KaraContext context = await dbContextFactory.CreateDbContextAsync())
                {
                    context.Remove(itm);
                    Messenger.Send(new ItemRemovedMessage(itm));
                }

                await loggingService.OutLogAsync(
                    new()
                    {
                        CategoryId = itm.CategoryId,
                        IsUsed = true,
                    });
            }
        }

        /// <summary>
        /// Deletes the record as a disposal.
        /// </summary>
        /// <param name="item">The item to be removed.</param>
        /// <returns>The asynchronous task.</returns>
        [RelayCommand(AllowConcurrentExecutions = false, CanExecute = nameof(CanRemove))]
        public async Task RemoveAsDisposalAsync(Item? item)
        {
            if (item is not Item item1)
                return;

            using (KaraContext context = await dbContextFactory.CreateDbContextAsync())
            {
                context.Remove(item1);
                await context.SaveChangesAsync();
            }

            await loggingService.OutLogAsync(
                new()
                {
                    CategoryId = item1.CategoryId,
                    IsUsed = false,
                });
        }

        private bool CanRemove(Item? item)
        {
            return item is Item;
        }

        private bool CanOpenDetail(Item? item)
        {
            return item is Item;
        }

        private bool CanAdd()
        {
            using KaraContext context = dbContextFactory.CreateDbContext();

            return context.Categories.Count() >= 1;
        }

        public void Receive(CategoryAddedMessage message)
        {
            AddCommand.NotifyCanExecuteChanged();
        }

        public void Receive(CategoryRemovedMessage message)
        {
            AddCommand.NotifyCanExecuteChanged();
        }
    }
}
