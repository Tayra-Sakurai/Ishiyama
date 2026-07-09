using Kara.Contexts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Kara.ViewModels
{
    public partial class LargeCategoryViewModel : CategoryViewModel
    {
        public LargeCategoryViewModel(IDbContextFactory<KaraContext> factory)
            : base(factory) { }
    }
}
