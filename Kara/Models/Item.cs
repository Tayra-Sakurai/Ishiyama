using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kara.Models
{
    public class Item
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public int CategoryId { get; set; }
        public Category Category { get; set; } = null!;
        public DateTimeOffset BoughtAt { get; set; } = DateTimeOffset.Now;
        public DateTimeOffset Life { get; set; } = DateTimeOffset.Now;
    }
}
