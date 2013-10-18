using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using ContactsLib.Entities;

namespace ContactsApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        public MainWindow(Controller c)
        {
            Controller = c;
            InitializeComponent();
            GroupGraph.Update();
        }

        public Controller Controller { get; set; }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            AddContactPanel.Visibility = Visibility.Collapsed;
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            AddContactPanel.Visibility = Visibility.Visible;
            AddContactName.Text = "";
            AddContactName.Focus();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            if (AddContactName.Text.Length > 0)
            {
                AddContactPanel.Visibility = Visibility.Collapsed;
                Controller.CreateContact(AddContactName.Text);
                AddContactName.Text = "";
                GroupGraph.Update();
            }
        }

        private void RemoveButton_Click(object sender, RoutedEventArgs e)
        {
            Controller.RemoveContact();
            GroupGraph.Update();
        }

        private void AddDetail_Click(object sender, RoutedEventArgs e)
        {
            AddDetailPanel.Visibility = Visibility.Visible;
            AddDetailTitle.Text = ((FrameworkElement) sender).Tag as string;
            AddDetailValue.Text = "";
            AddDetailValue.Focus();
            AddDetailPopup.IsOpen = false;
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            Controller.AddSimpleDetail(AddDetailTitle.Text, AddDetailValue.Text);
            AddDetailTitle.Text = "";
            AddDetailValue.Text = "";
            AddDetailPanel.Visibility = Visibility.Collapsed;
            GroupGraph.Update();
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            AddDetailTitle.Text = "";
            AddDetailValue.Text = "";
            AddDetailPanel.Visibility = Visibility.Collapsed;
        }

        private void AddDetailButton_Click(object sender, RoutedEventArgs e)
        {
            AddDetailPopup.IsOpen = true;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
        }

        private void ContactName_Changed(EditableField arg1, string arg2)
        {
            Controller.ContactList.InvalidateContactList();
        }


        private void Group_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                Controller.ChangeContactGroup(Controller.CurrentContact, ((ComboBox) sender).Text);
                GroupGraph.Update();
            }
        }

        private void EditableField_Deleted(EditableField obj)
        {
            Controller.CurrentContact.Details.Remove(obj.Tag as ContactDetail);
            ((ContactDetail)obj.Tag).Destroy();
            GroupGraph.Update();
        }
    }
}