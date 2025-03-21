﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using Model;

namespace ViewModel1.Data
{
    public class SkinDB : BaseDB
    {
        private string connectionString = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=\"C:\\Users\\Yoav\\source\\repos\\Meamenn\\Yoav-s-Pr\\Yoav's Pr\\App_Data\\Database1.mdf\";Integrated Security=True";
        private SqlConnection connection;
        private SqlCommand command;
        private SqlDataReader reader;

        public SkinDB()
        {
            connection = new SqlConnection(connectionString);
            command = new SqlCommand();
            command.Connection = connection;
        }

        public List<Skin> SelectAll()
        {
            List<Skin> skinsList = new List<Skin>();

            try
            {
                command.CommandText = "SELECT * FROM Skin";
                connection.Open();
                reader = command.ExecuteReader();

                while (reader.Read())
                {
                    skinsList.Add(CreateModel());
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            finally
            {
                reader?.Close();
                if (connection.State == ConnectionState.Open) connection.Close();
            }

            return skinsList;
        }

        public Skin SelectById(int id)
        {
            try
            {
                command.CommandText = $"SELECT * FROM Skin WHERE Id = {id}";
                connection.Open();
                reader = command.ExecuteReader();

                if (reader.Read())
                {
                    return CreateModel();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            finally
            {
                reader?.Close();
                if (connection.State == ConnectionState.Open) connection.Close();
            }

            return null;
        }

        //private override void CreateModel(BaseEntity entity)
        //{
        //    Skin skin = entity as Skin;
        //    base.CreateModel(skin);
        //}

        public int Insert(Skin skin)
        {
            string sqlStr = $"INSERT INTO Skins (name, price) " +
                            $"VALUES ('{skin.SkinName}', {skin.Cost})";
            return SaveChanges(sqlStr);
        }

        public int Update(Skin skin)
        {
            string sqlStr = $"UPDATE Skins SET name='{skin.SkinName}', price={skin.Cost} " +
                            $"WHERE Id={skin.Id}";
            return SaveChanges(sqlStr);
        }

        public int Delete(Skin skin)
        {
            string sqlStr = $"DELETE FROM Skins WHERE Id={skin.Id}";
            return SaveChanges(sqlStr);
        }

        protected override Model.BaseEntity newEntity()
        {
            throw new NotImplementedException();
        }

        protected override Model.BaseEntity CreateModel(Model.BaseEntity entity)
        {
            throw new NotImplementedException();
        }

        protected override BaseEntity NewEntity()
        {
            throw new NotImplementedException();
        }

        protected override BaseEntity PopulateEntity(SqlDataReader reader)
        {
            throw new NotImplementedException();
        }

        public override void Insert(BaseEntity entity)
        {
            throw new NotImplementedException();
        }

        public override void Update(BaseEntity entity)
        {
            throw new NotImplementedException();
        }

        public override void Delete(BaseEntity entity)
        {
            throw new NotImplementedException();
        }

        protected override string GetTableName()
        {
            throw new NotImplementedException();
        }

        public class Skin
        {
            public int Id { get; set; }
            public string SkinName { get; set; }
            public string Rarity { get; set; }
            public int Cost { get; set; }
        }
    }
}

