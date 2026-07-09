using CommunityToolkit.Mvvm.ComponentModel;
using Kara.Contexts;
using Kara.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;

namespace Kara.ViewModels
{
    public partial class SmallCategoryViewModel : CategoryViewModel
    {
        public SmallCategoryViewModel(IDbContextFactory<KaraContext> dbContextFactory)
            : base(dbContextFactory)
        {
            category = new SmallCategory();

            Items = [];
        }

        public override async Task LoadAsync()
        {
            await base.LoadAsync();

            using KaraContext context = await factory.CreateDbContextAsync();

            if (category is SmallCategory smallCategory)
            {
                await context
                    .Entry(smallCategory)
                    .Collection(e => e.Items)
                    .LoadAsync();

                foreach (
                    Item item in
                    smallCategory.Items)
                    Items.Add(item);
            }
        }

        [ObservableProperty]
        public partial ObservableCollection<Item> Items { get; set; }
    }
}
