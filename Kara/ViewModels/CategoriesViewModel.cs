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

            await foreach (
                LargeCategory category in
                context
                .LargeCategories
                .Include(e => e.Children)
                .OrderBy(
                    e => e.Name,
                    StringComparer.CurrentCultureIgnoreCase)
                .AsAsyncEnumerable())
                Entities.Add(category);
        }

        [RelayCommand]
        private void Add()
        {
            WeakReferenceMessenger.Default.Send(new LargeCategoryAddingMessage(new()));
        }

        [RelayCommand(AllowConcurrentExecutions = false, CanExecute = nameof(CanRemove))]
        private async Task RemoveAsync(Category? category)
        {
            using KaraContext context = await factory.CreateDbContextAsync();

            if (category is not null)
            {
                context.Remove(category);
                await context.SaveChangesAsync();
            }
        }

        [RelayCommand(CanExecute = nameof(CanRemove))]
        public void Detail(Category? entity)
        {
            ArgumentNullException.ThrowIfNull(entity);

            WeakReferenceMessenger.Default.Send(new CategoryDetailMessage(entity));
        }

        private bool CanRemove(Category? category)
        {
            return category is not null;
        }
    }
}
