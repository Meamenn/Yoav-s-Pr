using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using ViewModel1.Data;
using Model1;
using System.Linq;

namespace WpfApp
{
    public class MainViewModel : INotifyPropertyChanged
    {
        private readonly UsersDB _usersDB;
        private readonly FriendshipsDB _friendshipsDB;
        private User _currentUser;
        private ObservableCollection<Friendship> _friends;

        public event PropertyChangedEventHandler PropertyChanged;

        public User CurrentUser
        {
            get => _currentUser;
            set { _currentUser = value; OnPropertyChanged(); }
        }

        public ObservableCollection<Friendship> Friends
        {
            get => _friends;
            set { _friends = value; OnPropertyChanged(); }
        }

        public MainViewModel()
        {
            _usersDB = new UsersDB();
            _friendshipsDB = new FriendshipsDB();

            // Load a sample user (e.g., first in DB)
            CurrentUser = (User)_usersDB.SelectAll().FirstOrDefault() ?? new User { Username = "Guest" };
            Friends = new ObservableCollection<Friendship>(
                _friendshipsDB.SelectAll().Cast<Friendship>().Where(f => f.UserId1 == CurrentUser.Id || f.UserId2 == CurrentUser.Id));
        }

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}