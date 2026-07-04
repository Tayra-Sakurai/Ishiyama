using CommunityToolkit.Mvvm.Messaging.Messages;
using Kara.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Kara.Messages
{
    public class SmallCategoryAddingMessage : ValueChangedMessage<SmallCategory>
    {
        public SmallCategoryAddingMessage(SmallCategory value) : base(value)
        {
        }
    }
}
