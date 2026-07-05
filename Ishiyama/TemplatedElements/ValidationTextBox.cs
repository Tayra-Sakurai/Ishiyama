using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Documents;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace Ishiyama.TemplatedElements
{
    [TemplateVisualState(GroupName = "ValidationStates", Name = "Invalid")]
    [TemplateVisualState(GroupName = "ValidationStates", Name = "Valid")]
    [TemplatePart(Name = "ValidationTextBoxTextBox", Type = typeof(TextBox))]
    public sealed partial class ValidationTextBox : Control
    {
        // Global variables
        private INotifyDataErrorInfo? oldDataContext;
        private TextBox? textBox;

        public ValidationTextBox()
        {
            DefaultStyleKey = typeof(ValidationTextBox);

            DataContextChanged += ValidationTextBox_DataContextChanged;
        }

        private void ValidationTextBox_DataContextChanged(FrameworkElement sender, DataContextChangedEventArgs args)
        {
            if (oldDataContext != null)
                oldDataContext.ErrorsChanged -= OldDataContext_ErrorsChanged;

            if (DataContext is INotifyDataErrorInfo notifyDataErrorInfo)
            {
                oldDataContext = notifyDataErrorInfo;
                oldDataContext.ErrorsChanged += OldDataContext_ErrorsChanged;
            }
        }

        private void OldDataContext_ErrorsChanged(object? sender, DataErrorsChangedEventArgs e)
        {
            Validate();
        }

        protected override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            textBox = (TextBox)GetTemplateChild("ValidationTextBoxTextBox");
            textBox.TextChanged += TextBox_TextChanged;
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            Text = ((TextBox)sender).Text;
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

        public static DependencyProperty TextProperty { get; } = DependencyProperty.Register(
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
            ((ValidationTextBox)d).Validate();
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

        public string ErrorMessage
        {
            get => (string)GetValue(ErrorMessageProperty);
            set => SetValue(ErrorMessageProperty, value);
        }

        private static readonly DependencyProperty ErrorMessageProperty = DependencyProperty.Register(
            nameof(ErrorMessage),
            typeof(string),
            typeof(ValidationTextBox),
            new(string.Empty));

        // Methods
        private void Validate()
        {
            if (
                DataContext is not INotifyDataErrorInfo errInfo ||
                PropertyName is not string propertyName)
                return;

            IEnumerable<ValidationResult> errors = errInfo.GetErrors(propertyName).OfType<ValidationResult>();

            if (
                errors.Count() == 0 ||
                errors.All(e => string.IsNullOrEmpty(e.ErrorMessage)))
            {
                ErrorMessage = string.Empty;
                UpdateValidationState(true);
                return;
            }

            string? message = errors.First(e => !string.IsNullOrEmpty(e.ErrorMessage)).ErrorMessage;
            if (string.IsNullOrEmpty(message))
                message = string.Empty;

            ErrorMessage = message;
            UpdateValidationState(false);
        }

        private void UpdateValidationState(bool isValid, bool useTransitions = false)
        {
            string name = isValid ? "Valid" : "Invalid";
            VisualStateManager.GoToState(this, name, useTransitions);
        }
    }
}
