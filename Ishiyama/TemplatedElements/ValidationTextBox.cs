using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Documents;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace Ishiyama.TemplatedElements
{
    [TemplatePart(Name = "ValidationTextBoxTextBox", Type = typeof(TextBox))]
    [TemplatePart(Name = "ValidationStatus", Type = typeof(TextBlock))]
    [TemplatePart(Name = "ValidationMessage", Type = typeof(TextBlock))]
    public sealed partial class ValidationTextBox : Control
    {
        // Elements
        private TextBox? textBox;
        private TextBlock? validationStatus;
        private TextBlock? validationMessage;

        // Global variables
        private INotifyDataErrorInfo? oldDataContext;

        public ValidationTextBox()
        {
            DefaultStyleKey = typeof(ValidationTextBox);
        }

        protected override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
        }

        // The properties.
        public string Header
        {
            get => (string)GetValue(HeaderProperty);
            set => SetValue(HeaderProperty, value);
        }

        private static readonly DependencyProperty HeaderProperty = DependencyProperty.Register(
            nameof(Header),
            typeof(string),
            typeof(ValidationTextBox),
            new(default(string)));

        public string Text
        {
            get => (string)GetValue(TextProperty);
            set => SetValue(TextProperty, value);
        }

        private static readonly DependencyProperty TextProperty = DependencyProperty.Register(
            nameof(Text),
            typeof(string),
            typeof(ValidationTextBox),
            new(default(string)));

        public string PropertyName
        {
            get => (string)GetValue(PropertyNameProperty);
            set => SetValue(PropertyNameProperty, value);
        }

        private static readonly DependencyProperty PropertyNameProperty = DependencyProperty.Register(
            nameof(PropertyName),
            typeof(string),
            typeof(ValidationTextBox),
            new(default(string), PropertyNamePropertyChangedCallback));

        // Property changed callback
        private static void PropertyNamePropertyChangedCallback(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            throw new NotImplementedException();
        }

        public string PlaceholderText
        {
            get => (string)GetValue(PlaceholderTextProperty);
            set => SetValue(PlaceholderTextProperty, value);
        }

        private static readonly DependencyProperty PlaceholderTextProperty = DependencyProperty.Register(
            nameof(PlaceholderText),
            typeof(string),
            typeof(ValidationTextBox),
            new(default(string)));

        // Methods

    }
}
