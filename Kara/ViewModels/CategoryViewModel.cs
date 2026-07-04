using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using Kara.Contexts;
using Kara.Messages;
using Kara.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
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
        }

        [Required]
        public string Name
        {
            get => category.Name;
            set => SetProperty(category.Name, value, category, (m, v) => m.Name = v, true);
        }

        [RelayCommand(AllowConcurrentExecutions = false, CanExecute = nameof(CanSave))]
        public async Task SaveAsync()
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
            context.Update(category);
            await context.SaveChangesAsync();
        }

        [RelayCommand(AllowConcurrentExecutions = false)]
        public async Task RemoveAsync()
        {
            using KaraContext context = await factory.CreateDbContextAsync();

            context.Remove(category);
            await context.SaveChangesAsync();

            WeakReferenceMessenger.Default.Send(new CategoryRemovedMessage(category));
        }
    }
}
