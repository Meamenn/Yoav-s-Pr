
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model1
{
    internal class Skin : BaseEntity
    {
        
        private string SkinName;
        private string Rarity;
        private int Cost;
        
        public Skin() { }

        public string SkinName1 { get => SkinName; set => SkinName = value; }
        public string Rarity1 { get => Rarity; set => Rarity = value; }
        public int Cost1 { get => Cost; set => Cost = value; }
        //public Skin(string skinName, string rarity, int cost, int id) : base(id)
        //{
        //    this.SkinName = skinName;
        //    this.Rarity = rarity;
        //    this.Cost = cost;
        //}
    }
}
