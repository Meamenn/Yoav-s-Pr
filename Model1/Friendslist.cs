using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model1
{
    public class FriendsList : List<Friendship>
    {
        public FriendsList() { }
        public FriendsList(IEnumerable<Friendship> list) : base(list) { }

        public FriendsList(IEnumerable<BaseEntity> list) : base(list.Cast<Friendship>().ToList()) { }

    }
}
