using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlTypes;
using System.Data;
using System.Data.SqlClient;
using Model1;

namespace ViewModel1
{
    public abstract class BaseDB
    {
        protected string connectionString;
        protected SqlConnection connection;
        protected SqlCommand command;
        protected SqlDataReader reader;

        protected BaseDB()
        {
            connectionString = @"Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=\""C:\\Users\\Yoav\\source\\repos\\Meamenn\\Yoav-s-Pr\\Yoav's Pr\\App_Data\\Database1.mdf\"";Integrated Security=True";
            connection = new SqlConnection(connectionString);
            command = new SqlCommand { Connection = connection };
        }

        // Abstract methods to be implemented in derived classes
        protected abstract BaseEntity newEntity();
        protected abstract BaseEntity CreateModel(BaseEntity entity);

        // Generic SelectAll method
        public List<BaseEntity> SelectAll()
        {
            List<BaseEntity> list = new List<BaseEntity>();

            try
            {
                command.CommandText = $"SELECT * FROM {GetType().Name.Replace("DB", "")}";
                connection.Open();
                reader = command.ExecuteReader();

                while (reader.Read())
                {
                    BaseEntity entity = newEntity();
                    list.Add(CreateModel(entity));
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
            finally
            {
                reader?.Close();
                connection.Close();
            }

            return list;
        }

        // Generic SelectById method
        public BaseEntity SelectById(int id)
        {
            BaseEntity entity = null;

            try
            {
                command.CommandText = $"SELECT * FROM {GetType().Name.Replace("DB", "")} WHERE Id = {id}";
                connection.Open();
                reader = command.ExecuteReader();

                if (reader.Read())
                {
                    entity = newEntity();
                    entity = CreateModel(entity);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
            finally
            {
                reader?.Close();
                connection.Close();
            }

            return entity;
        }
        protected int SaveChanges(string command_text)
        {
            int records = 0;

            try
            {
                command.CommandText = command_text;
                connection.Open();
                records = command.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine(e.Message + "\nSQL: " + command.CommandText);
            }
            finally
            {
                if (connection.State == ConnectionState.Open)
                    connection.Close();
            }

            return records;
        }
    }
}
