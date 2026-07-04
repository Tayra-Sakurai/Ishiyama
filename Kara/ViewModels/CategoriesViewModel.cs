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
    public partial class CategoriesViewModel : ObservableObject, IEntriesViewModel<Category>
    {
        private readonly IDbContextFactory<KaraContext> factory;

        public CategoriesViewModel(IDbContextFactory<KaraContext> factory)
        {
            this.factory = factory;
            Entities = [];
        }

        [ObservableProperty]
        public partial ObservableCollection<LargeCategory> Entities { get; set; }

        [RelayCommand(AllowConcurrentExecutions = false)]
        public async Task LoadAsync()
        {
            using KaraContext context = await factory.CreateDbContextAsync();

            Entities.Clear();

            await foreach(
                LargeCategory largeCategory
                in context.LargeCategories
                .Include(e => e.SmallCategories)
                .OrderBy(e => e.CategoryId)
                .AsAsyncEnumerable())
                Entities.Add(largeCategory);
        }

        [RelayCommand(AllowConcurrentExecutions = false, CanExecute = nameof(CanRemove))]
        public async Task RemoveAsync(Category? category)
        {
            ArgumentNullException.ThrowIfNull(category);

            using KaraContext context = await factory.CreateDbContextAsync();

            context.Remove(category);
            await context.SaveChangesAsync();
        }

        private bool CanRemove(Category? category)
        {
            return category is Category;
        }

        [RelayCommand(CanExecute = nameof(CanRemove))]
        public void Detail(Category? category)
        {
            ArgumentNullException.ThrowIfNull(category);

            WeakReferenceMessenger.Default.Send(new CategoryDetailMessage(category));
        }

        [RelayCommand]
        private static void Add()
        {
            WeakReferenceMessenger.Default.Send(new LargeCategoryAddingMessage(new()));
        }

        [RelayCommand(CanExecute = nameof(IsSelectedLarge))]
        private static void AddSmall(Category? category)
        {
            if (category is LargeCategory largeCategory)
                WeakReferenceMessenger.Default.Send(new SmallCategoryAddingMessage(new()
                {
                    LargeCategoryId = largeCategory.CategoryId,
                }));
        }

        private bool IsSelectedLarge(Category? category)
        {
            return category is LargeCategory;
        }
    }
}
