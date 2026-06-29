using Kara.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kara.FakeModels
{
    public class FakeContextBuilder : IDesignTimeDbContextFactory<KaraContext>
    {
        public KaraContext CreateDbContext(string[] args)
        {
            DbContextOptionsBuilder<KaraContext> builder = new();
            builder.UseSqlServer("Data Source=thinkpadx13\\SQLEXPRESS02;Initial Catalog=StockpileMan;Integrated Security=True;Connect Timeout=30;Encrypt=True;Trust Server Certificate=True;Application Intent=ReadWrite;Multi Subnet Failover=False;Command Timeout=30");

            return new(builder.Options);
        }
    }
}
