using CommunityToolkit.Mvvm.Messaging.Messages;
using Kara.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Kara.Messages
{
    public class LargeCategoryAddingMessage : ValueChangedMessage<LargeCategory>
    {
        public LargeCategoryAddingMessage(LargeCategory value)
            : base(value)
        { }
    }
}
