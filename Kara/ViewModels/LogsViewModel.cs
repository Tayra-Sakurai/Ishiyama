using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
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
    public partial class LogsViewModel : ObservableObject
    {
        private readonly IDbContextFactory<KaraContext> dbContextFactory;

        public LogsViewModel(IDbContextFactory<KaraContext> dbContextFactory)
        {
            this.dbContextFactory = dbContextFactory;
            Logs = [];
        }

        [ObservableProperty]
        public partial ObservableCollection<Log> Logs { get; set; }

        [RelayCommand(AllowConcurrentExecutions = false)]
        public async Task LoadAsync()
        {
            using KaraContext context = await dbContextFactory.CreateDbContextAsync();

            Logs.Clear();

            await foreach (
                Log log
                in context.Logs.AsAsyncEnumerable())
                Logs.Add(log);
        }
    }
}
