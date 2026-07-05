using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kara.Models
{
    public class Category
    {
        public int CategoryId { get; set; }
        public string Name { get; set; } = string.Empty;
        public int? ParentId { get; set; }
        public Category? Parent { get; set; }
        public ObservableCollection<Category> Children { get; } = [];
    }
}
