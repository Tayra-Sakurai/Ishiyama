using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using Kara.Contexts;
using Kara.Messages;
using Kara.Models;
using Kara.Validations;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kara.ViewModels
{
    public partial class ItemViewModel : ObservableValidator, IEntryViewModel<Item>
    {
        private Item item;
        private readonly IDbContextFactory<KaraContext> factory;

        public ItemViewModel(IDbContextFactory<KaraContext> dbContextFactory)
        {
            factory = dbContextFactory;
            Categories = [];

            using KaraContext context = factory.CreateDbContext();
            item = new()
            {
                SmallCategoryId = context.SmallCategories.First().CategoryId,
            };
        }

        public async Task LoadExistingDataAsync(Item entity)
        {
            using KaraContext context = await factory.CreateDbContextAsync();

            Item? newItem = await context.Items.FindAsync(entity.Id);
            if (newItem != null)
            {
                item = newItem;
                await context.Entry(item)
                    .Reference(e => e.SmallCategory)
                    .LoadAsync();
            }
        }

        [ObservableProperty]
        public partial ObservableCollection<Category> Categories
        {
            get;
            set;
        }

        [Required]
        public string Name
        {
            get => item.Name;
            set
            {
                SetProperty(item.Name, value, item, (m, v) => m.Name = v, true);
                SaveCommand.NotifyCanExecuteChanged();
            }
        }

        [Required]
        [FutureOrPastDateTime(false)]
        public DateTimeOffset BoughtAt
        {
            get => item.BoughtAt;
            set
            {
                SetProperty(item.BoughtAt, value, item, (m, v) => m.BoughtAt = v, true);
                SaveCommand.NotifyCanExecuteChanged();
            }
        }

        [Required]
        public DateTimeOffset Life
        {
            get => item.Life;
            set => SetProperty(item.Life, value, item, (m, v) => m.Life = v, true);
        }

        [Required]
        public SmallCategory? SmallCategory
        {
            get => item.SmallCategory;
            set
            {
                ArgumentNullException.ThrowIfNull(value);

                SetProperty(item.SmallCategoryId, value.CategoryId, item, (m, v) => m.SmallCategoryId = v, true);
            }
        }

        [RelayCommand(AllowConcurrentExecutions = false, CanExecute = nameof(CanSave))]
        public async Task SaveAsync()
        {
            using KaraContext context = await factory.CreateDbContextAsync();

            if (HasErrors)
                return;

            context.Update(item);
            await context.SaveChangesAsync();
        }

        /// <summary>
        /// Loads the categories in the select box.
        /// </summary>
        /// <returns>The asynchronous task.</returns>
        [RelayCommand(AllowConcurrentExecutions = false)]
        public async Task LoadCategoriesAsync()
        {
            using KaraContext context = await factory.CreateDbContextAsync();

            Categories.Clear();

            await foreach (
                Category category in
                context.Categories.AsAsyncEnumerable())
                Categories.Add(category);
        }

        private bool CanSave()
        {
            return !HasErrors;
        }

        [RelayCommand]
        public async Task RemoveAsync()
        {
            using KaraContext context = await factory.CreateDbContextAsync();
            context.Remove(item);
            await context.SaveChangesAsync();

            WeakReferenceMessenger.Default.Send(new ItemRemovedMessage(item));
        }
    }
}
