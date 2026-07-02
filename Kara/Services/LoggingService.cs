using Kara.Contexts;
using Kara.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kara.Services
{
    public class LoggingService : ILoggingService<Log>
    {
        private IDbContextFactory<KaraContext> factory;

        public LoggingService(IDbContextFactory<KaraContext> factory)
        {
            this.factory = factory;
        }

        public async Task OutLogAsync(Log log)
        {
            using KaraContext context = await factory.CreateDbContextAsync();

            context.Add(log);
            await context.SaveChangesAsync();
        }
    }
}
