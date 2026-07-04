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
        public Category? Category { get; set; }
        public DateTimeOffset DateTimeOffset { get; set; } = DateTimeOffset.Now;
        public int ActionTypeId { get; set; }
        public ActionType? ActionType { get; set; }
    }
}
