using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Model;

namespace Model
{
    public class Skin : BaseEntity
    {
        public int Id { get; set; }
        public string SkinName { get; set; }
        public string Rarity { get; set; }
        public int Cost { get; set; }

        public Skin() { }

        public Skin(int id, string skinName, string rarity, int cost)
        {
            Id = id;
            SkinName = skinName;
            Rarity = rarity;
            Cost = cost;
        }
    }
}

namespace ViewModel1.Data
{
    public class SkinDB : BaseDB<Skin>
    {
        public SkinDB() : base()
        {
        }

        public new List<Skin> SelectAll()
        {
            return base.SelectAll();
        }

        public new Skin SelectById(int id)
        {
            return base.SelectById(id);
        }

        public int Insert(Skin skin)
        {
            Insert(skin as BaseEntity);
            return 1; // Assuming one row is inserted
        }

        public int Update(Skin skin)
        {
            Update(skin as BaseEntity);
            return 1; // Assuming one row is updated
        }

        public int Delete(Skin skin)
        {
            Delete(skin as BaseEntity);
            return 1; // Assuming one row is deleted
        }

        protected override BaseEntity PopulateEntity(SqlDataReader reader)
        {
            Skin skin = new Skin();
            skin.Id = (int)reader["Id"];
            skin.SkinName = reader["SkinName"].ToString();
            skin.Rarity = reader["Rarity"].ToString();
            skin.Cost = (int)reader["Cost"];
            return skin;
        }

        public override void Insert(BaseEntity entity)
        {
            Skin skin = entity as Skin;
            if (skin == null)
                throw new ArgumentException("Entity must be of type Skin");

            var parameters = new Dictionary<string, object>
            {
                { "@SkinName", skin.SkinName },
                { "@Rarity", skin.Rarity },
                { "@Cost", skin.Cost }
            };
            ExecuteNonQuery("INSERT INTO Skin (SkinName, Rarity, Cost) VALUES (@SkinName, @Rarity, @Cost)", parameters);
        }

        public override void Update(BaseEntity entity)
        {
            Skin skin = entity as Skin;
            if (skin == null)
                throw new ArgumentException("Entity must be of type Skin");

            var parameters = new Dictionary<string, object>
            {
                { "@SkinName", skin.SkinName },
                { "@Rarity", skin.Rarity },
                { "@Cost", skin.Cost },
                { "@Id", skin.Id }
            };
            ExecuteNonQuery("UPDATE Skin SET SkinName=@SkinName, Rarity=@Rarity, Cost=@Cost WHERE Id=@Id", parameters);
        }

        public override void Delete(BaseEntity entity)
        {
            Skin skin = entity as Skin;
            if (skin == null)
                throw new ArgumentException("Entity must be of type Skin");

            var parameters = new Dictionary<string, object>
            {
                { "@Id", skin.Id }
            };
            ExecuteNonQuery("DELETE FROM Skin WHERE Id=@Id", parameters);
        }

        protected override string GetTableName()
        {
            return "Skin";
        }
    }
}
