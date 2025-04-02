using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Model;

namespace ViewModel1.Data
{
    public abstract class BaseDB<TEntity> where TEntity : BaseEntity, new()
    {
        protected readonly string ConnectionString;

        protected BaseDB()
        {
            ConnectionString = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=\"C:\\Users\\Yoav\\source\\repos\\Meamenn\\Yoav-s-Pr\\Yoav's Pr\\App_Data\\Database1.mdf\";Integrated Security=True";
        }

        protected abstract string GetTableName();

        public List<TEntity> SelectAll()
        {
            var list = new List<TEntity>();
            using (var connection = new SqlConnection(ConnectionString))
            using (var command = new SqlCommand($"SELECT * FROM {GetTableName()}", connection))
            {
                try
                {
                    connection.Open();
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var entity = new TEntity();
                            list.Add((TEntity)PopulateEntity(reader));
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception($"Error selecting all records from {GetTableName()}: {ex.Message}", ex);
                }
            }
            return list;
        }

        public TEntity SelectById(int id)
        {
            using (var connection = new SqlConnection(ConnectionString))
            using (var command = new SqlCommand($"SELECT * FROM {GetTableName()} WHERE Id = @Id", connection))
            {
                try
                {
                    command.Parameters.AddWithValue("@Id", id);
                    connection.Open();
                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            var entity = new TEntity();
                            return (TEntity)PopulateEntity(reader);
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception($"Error selecting record with Id {id} from {GetTableName()}: {ex.Message}", ex);
                }
            }
            return null;
        }

        protected int ExecuteNonQuery(string sql, Dictionary<string, object> parameters = null)
        {
            using (var connection = new SqlConnection(ConnectionString))
            using (var command = new SqlCommand(sql, connection))
            {
                try
                {
                    if (parameters != null)
                    {
                        foreach (var param in parameters)
                        {
                            command.Parameters.AddWithValue(param.Key, param.Value);
                        }
                    }
                    connection.Open();
                    return command.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    throw new Exception($"Error executing query: {sql}. Error: {ex.Message}", ex);
                }
            }
        }

        public abstract void Insert(BaseEntity entity);
        public abstract void Update(BaseEntity entity);
        public abstract void Delete(BaseEntity entity);

        protected abstract BaseEntity PopulateEntity(SqlDataReader reader);
    }
}