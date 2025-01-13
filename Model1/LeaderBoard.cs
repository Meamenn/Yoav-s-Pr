using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model1
{
    public class Leaderboard
    {
        private User User;
        private int Points;

        public Leaderboard(User user, int points)
        {
            this.User = user;
            this.Points = points;
        }

        public void UpdatePoints(int newPoints)
        {
            this.Points = newPoints;
        }

        public int GetPoints() { return this.Points; }

        public string GetUserName() { return this.User.GetUsername(); }

        public static int ComparePoints(Leaderboard l1, Leaderboard l2)
        {
            return l2.Points.CompareTo(l1.Points);
        }

        public override string ToString()
        {
            return $"{User.GetUsername}: {Points} points";
        }
    }
}
