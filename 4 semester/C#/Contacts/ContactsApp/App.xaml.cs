using System.Windows;

namespace ContactsApp
{
    public partial class App
    {
        private void OnStartup(object sender, StartupEventArgs e)
        {
            new MainWindow(new Controller()).Show();
        }
    }
}