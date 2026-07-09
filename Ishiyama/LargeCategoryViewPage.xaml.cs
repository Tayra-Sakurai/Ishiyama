using CommunityToolkit.Mvvm.DependencyInjection;
using Kara.Models;
using Kara.ViewModels;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace Ishiyama
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class LargeCategoryViewPage : Page
    {
        private LargeCategoryViewModel? viewModel;

        public LargeCategoryViewPage()
        {
            InitializeComponent();
        }

        protected async override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            viewModel = Ioc.Default.GetRequiredService<LargeCategoryViewModel>();
            
            if (e.Parameter is LargeCategory largeCategory)
            {
                await viewModel.LoadExistingDataAsync(largeCategory);
            }

            await viewModel.LoadAsync();
        }

        private void GoBackCommand_ExecuteRequested(XamlUICommand sender, ExecuteRequestedEventArgs args)
        {
            if (Frame.CanGoBack)
                Frame.GoBack();
        }

        private void GoBackCommand_CanExecuteRequested(XamlUICommand sender, CanExecuteRequestedEventArgs args)
        {
            if (Frame.CanGoBack)
            {
                args.CanExecute = true;
                return;
            }

            args.CanExecute = false;
        }
    }
}
