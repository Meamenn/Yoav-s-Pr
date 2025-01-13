
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model1
{
    internal class Skin : BaseEnitity
    {
        private string SkinName;
        private string Rarity;
        private int Cost;

        public Skin() { }
        public Skin(string skinName, string rarity, int cost, int id) : base(id)
        {
            this.SkinName = skinName;
            this.Rarity = rarity;
            this.Cost = cost;
        }
    }
}
