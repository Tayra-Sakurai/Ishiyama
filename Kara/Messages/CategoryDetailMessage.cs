using CommunityToolkit.Mvvm.Messaging.Messages;
using Kara.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kara.Messages
{
    public class CategoryDetailMessage : ValueChangedMessage<Category>
    {
        public CategoryDetailMessage(Category value)
            : base(value) { }
    }
}
