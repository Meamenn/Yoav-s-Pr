using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model1
{
    public class Friendship
    {
        private User User1;
        private User User2;
        private int User1Wins;
        private int User2Wins;


        public void Win()
        {
            this.User1Wins++;
        }
        public void Defeat()
        {
            this.User2Wins++;
        }
        public Friendship(User U1, User U2)
        {
            this.User1 = U1;
            this.User2 = U2;
            User1Wins = 0;
            User2Wins = 0;
        }

        public int GetUser1Wins() { return this.User1Wins; }
        public int GetUser2Wins() { return this.User2Wins; }

        public Friendship(User U1, User U2, int user1Wins, int user2Wins)
        {
            this.User1 = U1;
            this.User2 = U2;
            this.User1Wins = user1Wins;
            this.User2Wins = user2Wins;
        }
    }
}
