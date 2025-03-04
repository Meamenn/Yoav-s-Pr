using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class Friendship : BaseEntity
    {
        public int UserId1 { get; set; }
        public int UserId2 { get; set; }
        public int User1Wins { get; set; }
        public int User2Wins { get; set; }

        public Friendship(int userId1, int userId2)
        {
            UserId1 = userId1;
            UserId2 = userId2;
            User1Wins = 0;
            User2Wins = 0;
        }

        public void Win() => User1Wins++;
        public void Defeat() => User2Wins++;

        public int GetUser1Wins() => User1Wins;
        public int GetUser2Wins() => User2Wins;
    }
}
