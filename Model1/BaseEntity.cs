using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model1
{
    public class BaseEnitity
    {
        private int id;

        public BaseEnitity() { }
        public BaseEnitity(int id)
        {
            this.id = id;
        }
        public int getID()
        {
            return id;
        }
        public void setID(int id)
        {
            this.id = id;
        }
    }
}
