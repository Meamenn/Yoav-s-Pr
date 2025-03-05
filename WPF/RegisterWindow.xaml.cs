using System.Windows;
using ViewModel1.Data;
using Model;

namespace WpfApp
{
    public partial class RegisterWindow : Window
    {
        public RegisterWindow()
        {
            InitializeComponent();
        }

        private void Register_Click(object sender, RoutedEventArgs e)
        {
            string username = UsernameTextBox.Text;
            string password = PasswordBox.Password;
            string email = EmailTextBox.Text;

            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password) || string.IsNullOrEmpty(email))
            {
                MessageBox.Show("Please fill all fields.");
                return;
            }

            var usersDB = new UsersDB();
            var existingUser = usersDB.SelectByUsername(username);
            if (existingUser != null)
            {
                MessageBox.Show("Username already taken.");
                return;
            }

            var newUser = new User
            {
                Username = username,
                Passcode = password, // In a real app, hash the password
                Mail = email,
                Coins = 100 // Starting coins
            };
            usersDB.Insert(newUser);
            MessageBox.Show($"Registration successful! Welcome, {username}!");
            var mainWindow = new MainWindow();
            mainWindow.Show();
            this.Close();
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            var mainWindow = new MainWindow();
            mainWindow.Show();
            this.Close();
        }
    }
}