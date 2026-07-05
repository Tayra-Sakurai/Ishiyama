using System;
using System.Collections.Generic;
using System.Text;

namespace Kara.Models
{
    public class SmallCategory : Category
    {
        public ICollection<Item> Items { get; } = new HashSet<Item>();
    }
}
