using System;
using System.Windows;
using System.Windows.Input;

namespace ContactsApp
{
    /// <summary>
    /// Interaction logic for EditableField.xaml
    /// </summary>
    public partial class EditableField
    {
        public static DependencyProperty TextProperty = DependencyProperty.Register("Text", typeof (string),
                                                                                    typeof (EditableField));

        public static DependencyProperty DeletableProperty = DependencyProperty.Register("Deletable", typeof (bool),
                                                                                         typeof (EditableField));

        public new static DependencyProperty FontSizeProperty = DependencyProperty.Register("FontSize", typeof (int),
                                                                                            typeof (EditableField));

        public EditableField()
        {
            InitializeComponent();
        }

        public string Text
        {
            get { return (string) GetValue(TextProperty); }
            set { SetValue(TextProperty, value); }
        }

        public bool Deletable
        {
            get { return (bool) GetValue(DeletableProperty); }
            set { SetValue(DeletableProperty, value); }
        }

        public new int FontSize
        {
            get { return (int) GetValue(FontSizeProperty); }
            set { SetValue(FontSizeProperty, value); }
        }

        public event Action<EditableField, string> Changed = delegate { };
        public event Action<EditableField> Deleted;

        private void Label_Click(object sender, RoutedEventArgs e)
        {
            Label.Visibility = Visibility.Collapsed;
            TextBox.Visibility = Visibility.Visible;
            if (Deletable)
                Delete.Visibility = Visibility.Visible;
            TextBox.SelectAll();
            TextBox.Focus();
        }

        private void Hide()
        {
            Label.Visibility = Visibility.Visible;
            TextBox.Visibility = Visibility.Collapsed;
            Delete.Visibility = Visibility.Collapsed;
        }

        private void TextBox_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                Text = TextBox.Text;
                Changed(this, TextBox.Text);
                Hide();
            }
            if (e.Key == Key.Escape)
                Hide();
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            Deleted(this);
        }
    }
}