using System;
using System.Collections.Generic;
using System.Text;

namespace Kara.Models
{
    public class Location
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public ICollection<Item> Items { get; set; } = new HashSet<Item>();
    }
}
