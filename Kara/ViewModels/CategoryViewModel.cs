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
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Threading.Tasks;

namespace Kara.ViewModels
{
    public partial class CategoryViewModel : ObservableValidator, IEntryViewModel<Category>
    {
        protected Category category;
        protected IDbContextFactory<KaraContext> factory;

        public CategoryViewModel(IDbContextFactory<KaraContext> factory)
        {
            this.factory = factory;
            category = new();
            Categories = [];
        }

        [RelayCommand(AllowConcurrentExecutions = false)]
        public virtual async Task LoadAsync()
        {
            using KaraContext context = await factory.CreateDbContextAsync();

            Categories.Clear();

            await foreach (
                Category category in
                context.Categories
                .AsAsyncEnumerable())
                Categories.Add(category);

            Categories.Insert(0, category.Parent);
        }

        [Required]
        public string Name
        {
            get => category.Name;
            set
            {
                SetProperty(category.Name, value, category, (m, v) => m.Name = v, true);
                SaveCommand.NotifyCanExecuteChanged();
            }
        }

        public Category? Parent
        {
            get => category.Parent;
            set
            {
                SetProperty(category.Parent, value, category, (m, v) => m.Parent = v);
                SetProperty(category.ParentId, value?.CategoryId, category, (m, v) => m.ParentId = v);
            }
        }

        [ObservableProperty]
        public partial ObservableCollection<Category?> Categories { get; set; }

        [RelayCommand(AllowConcurrentExecutions = false, CanExecute = nameof(CanSave))]
        protected virtual async Task SaveAsync()
        {
            if (HasErrors)
                return;

            using KaraContext context = await factory.CreateDbContextAsync();

            context.Update(category);
            await context.SaveChangesAsync();
        }

        private bool CanSave()
        {
            ValidateAllProperties();
            return !HasErrors;
        }

        public virtual async Task LoadExistingDataAsync(Category entity)
        {
            using KaraContext context = await factory.CreateDbContextAsync();

            category = entity;
            Name = entity.Name;
            Parent = entity.Parent;
            context.Update(category);

            SaveCommand.NotifyCanExecuteChanged();

            await context.SaveChangesAsync();
        }

        [RelayCommand(AllowConcurrentExecutions = false)]
        protected virtual async Task RemoveAsync()
        {
            using KaraContext context = await factory.CreateDbContextAsync();

            context.Remove(category);
            await context.SaveChangesAsync();

            WeakReferenceMessenger.Default.Send(new CategoryRemovedMessage(category));
        }
    }
}
