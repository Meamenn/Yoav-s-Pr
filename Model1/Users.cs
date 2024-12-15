using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model1
{
    public class User : BaseEnitity
    {
        private string username;
        private string passcode;
        private string mail;
        private int coins;
        private FriendsList FriendsList;


        public User() { }
        public User(string username, string passcode, string mail, int coins ,int id) : base(id)
        {
            FriendsList = new FriendsList();
            this.username = username;
            this.passcode = passcode;
            this.mail = mail;
            this.coins = coins;
        }

        public User(string username, string passcode, string mail, int id, FriendsList f1, int coins) : base(id)
        {
            FriendsList = f1;
            this.username = username;
            this.passcode = passcode;
            this.mail = mail;
            this.coins = coins;
        }

        public User(string username, string passcode, string mail, int id, FriendsList friendsList, SkinList skins) : base(id)
        {
            username = username;
            passcode = passcode;
            mail = mail;

            if (friendsList != null)
            {
                this.FriendsList = friendsList;
            }
            else
                this.FriendsList = new FriendsList();

            foreach (var skin in skins)
            {
                if (string.Equals(skin.getSkinType(), "color", StringComparison.OrdinalIgnoreCase))
                {
                    this.color.Add(skin);
                }
                else if (string.Equals(skin.getSkinType(), "background", StringComparison.OrdinalIgnoreCase))
                {
                    this.background.Add(skin);
                }
            }

        }

        public void AddFriend(User U2)
        {
            FriendsList.Append(new Friendship(this, U2));
        }
        public FriendsList GetFriendships()
        {
            return (this.FriendsList);
        }
        public string GetPasscode() { return this.passcode; }
        public string GetMail() { return this.mail; }
        public string GetUsername() { return this.username; }


    }
}
