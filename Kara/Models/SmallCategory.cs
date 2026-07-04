using System;
using System.Collections.Generic;
using System.Text;

namespace Kara.Models
{
    public class SmallCategory : Category
    {
        public int? LargeCategoryId { get; set; }
        public LargeCategory? LargeCategory { get; set; }
    }
}
