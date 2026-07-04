using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace Kara.Models
{
    public class LargeCategory : Category
    {
        public ObservableCollection<SmallCategory> SmallCategories { get; } = [];
    }
}
