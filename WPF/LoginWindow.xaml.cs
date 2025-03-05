using System.Windows;
using ViewModel1.Data;
using Model;

namespace WpfApp
{
    public partial class LoginWindow : Window
    {
        public LoginWindow()
        {
            InitializeComponent();
            // Reuse EpicButtonStyle and MythicTextStyle from MainWindow (add to App.xaml if needed)
        }
        //
        private void Login_Click(object sender, RoutedEventArgs e)
        {
            string username = UsernameTextBox.Text;
            string password = PasswordBox.Password;
            var usersDB = new UsersDB();
            var user = usersDB.SelectByUsername(username);

            if (user != null && user.Passcode == password) // In a real app, use password hashing
            {
                MessageBox.Show($"Welcome, {user.Username}!");
                var mainWindow = new MainWindow();
                mainWindow.Show();
                this.Close();
            }
            else
            {
                MessageBox.Show("Invalid username or password.");
            }
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            var mainWindow = new MainWindow();
            mainWindow.Show();
            this.Close();
        }
    }
}