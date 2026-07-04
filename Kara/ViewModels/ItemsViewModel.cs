using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using Kara.Contexts;
using Kara.Messages;
using Kara.Models;
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

        public ItemsViewModel(IDbContextFactory<KaraContext> dbContextFactory)
            : base()
        {
            this.dbContextFactory = dbContextFactory;

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
                .Include(x => x.SmallCategory)
                .ThenInclude(x => x!.LargeCategory)
                .AsAsyncEnumerable())
                Entities.Add(item);

            AddCommand.NotifyCanExecuteChanged();
        }

        [RelayCommand(AllowConcurrentExecutions = false, CanExecute = nameof(CanAdd))]
        public async Task AddAsync()
        {
            using KaraContext context = await dbContextFactory.CreateDbContextAsync();

            Category defaultCategory = context.SmallCategories.First();
            Item newItem = new()
            {
                SmallCategoryId = defaultCategory.CategoryId,
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
            }
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
