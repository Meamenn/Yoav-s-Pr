using Model1;

public class User : BaseEntity
{
    public string Username { get; set; }
    public string Passcode { get; set; }
    public string Mail { get; set; }
    public int Coins { get; set; }
    public List<Friendship> FriendsList { get; set; } = new List<Friendship>();

    public User()
    {
        FriendsList = new List<Friendship>();
    }

    public void AddFriend(User u2)
    {
        FriendsList.Add(new Friendship(Id, u2.Id));
    }

    public List<Friendship> GetFriendships() => FriendsList;
    public string GetPasscode() => Passcode;
    public string GetMail() => Mail;
    public string GetUsername() => Username;
}