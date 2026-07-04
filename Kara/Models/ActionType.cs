using System;
using System.Collections.Generic;
using System.Text;

namespace Kara.Models
{
    public class ActionType
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public ICollection<Log> Logs { get; } = new HashSet<Log>();
    }
}
