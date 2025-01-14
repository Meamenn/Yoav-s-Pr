using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model1
{
    public abstract class BaseEntity
    {
        private int id;
        
        public int Id { get => id; set => id = value; }
    }
}
