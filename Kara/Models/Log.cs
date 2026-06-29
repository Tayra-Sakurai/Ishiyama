using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kara.Models
{
    public class Log
    {
        public int Id { get; set; }
        public int CategoryId { get; set; }
        public Category Category { get; set; } = null!;
        public DateTimeOffset DateTimeOffset { get; set; } = DateTimeOffset.Now;
    }
}
