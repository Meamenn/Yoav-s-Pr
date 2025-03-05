using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using Model;

namespace ViewModel1.Data
{
    public abstract class BaseDB
    {
        protected readonly string ConnectionString;
        protected SqlConnection Connection;
        protected SqlCommand Command;

        protected BaseDB()
        {
            ConnectionString = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=\"C:\\Users\\Yoav\\source\\repos\\Meamenn\\Yoav-s-Pr\\Yoav's Pr\\App_Data\\Database1.mdf\";Integrated Security=True";
            Connection = new SqlConnection(ConnectionString);
            Command = new SqlCommand { Connection = Connection };
        }

        protected abstract BaseEntity NewEntity();
        protected abstract BaseEntity PopulateEntity(SqlDataReader reader);

        public List<BaseEntity> SelectAll()
        {
            var list = new List<BaseEntity>();
            try
            {
                Command.CommandText = $"SELECT * FROM {GetTableName()}";
                Connection.Open();
                using (var reader = Command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var entity = NewEntity();
                        list.Add(PopulateEntity(reader));
                    }
                }
            }
            catch (Exception ex) { Console.WriteLine($"Error: {ex.Message}"); }
            finally { Connection.Close(); }
            return list;
        }

        public BaseEntity SelectById(int id)
        {
            try
            {
                Command.CommandText = $"SELECT * FROM {GetTableName()} WHERE Id = @Id";
                Command.Parameters.Clear();
                Command.Parameters.AddWithValue("@Id", id);
                Connection.Open();
                using (var reader = Command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        var entity = NewEntity();
                        return PopulateEntity(reader);
                    }
                }
            }
            catch (Exception ex) { Console.WriteLine($"Error: {ex.Message}"); }
            finally { Connection.Close(); }
            return null;
        }

        protected int ExecuteNonQuery(string sql, Dictionary<string, object> parameters = null)
        {
            int records = 0;
            try
            {
                Command.CommandText = sql;
                Command.Parameters.Clear();
                if (parameters != null)
                    foreach (var param in parameters)
                        Command.Parameters.AddWithValue(param.Key, param.Value);
                Connection.Open();
                records = Command.ExecuteNonQuery();
            }
            catch (Exception ex) { Console.WriteLine($"Error: {ex.Message}"); }
            finally { Connection.Close(); }
            return records;
        }

        public abstract void Insert(BaseEntity entity);
        public abstract void Update(BaseEntity entity);
        public abstract void Delete(BaseEntity entity);

        protected abstract string GetTableName();
    }
}