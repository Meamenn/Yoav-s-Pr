using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModel1
{
    public class UsersDB
    {
        private string connectionString = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=\"C:\\Users\\Yoav\\source\\repos\\Meamenn\\Yoav-s-Pr\\Yoav's Pr\\App_Data\\Database1.mdf\";Integrated Security=True";
        private SqlConnection connection;
        private SqlCommand command;
        private SqlDataReader reader;

        public UsersDB()
        {
            connection = new SqlConnection(connectionString);
            command = new SqlCommand();
            command.Connection = connection;
        }

        public List<User> SelectAll()
        {
            List<User> usersList = new List<User>();

            try
            {
                command.CommandText = "SELECT * FROM Users";
                connection.Open();
                reader = command.ExecuteReader();

                while (reader.Read())
                {
                    usersList.Add(CreateModel());
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

            return usersList;
        }

        public User SelectById(int id)
        {
            try
            {
                command.CommandText = $"SELECT * FROM Users WHERE Id = {id}";
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

        private User CreateModel()
        {
            return new User
            {
                Id = (int)reader["Id"],
                Username = reader["username"].ToString(),
                Passcode = reader["passcode"].ToString(),
                Mail = reader["mail"].ToString(),
                Coins = (int)reader["coins"]
            };
        }
    }
    public class UsersDB : BaseDB
    {
        public int Insert(User user)
        {
            string sqlStr = $"INSERT INTO Users (username, passcode, email, coins) " +
                            $"VALUES ('{user.Username}', '{user.Passcode}', '{user.Email}', {user.Coins})";
            return SaveChanges(sqlStr);
        }

        public int Update(User user)
        {
            string sqlStr = $"UPDATE Users SET username='{user.Username}', passcode='{user.Passcode}', email='{user.Email}', " +
                            $"coins={user.Coins} WHERE Id={user.Id}";
            return SaveChanges(sqlStr);
        }

        public int Delete(User user)
        {
            string sqlStr = $"DELETE FROM Users WHERE Id={user.Id}";
            return SaveChanges(sqlStr);
        }
    }


    public class User
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Passcode { get; set; }
        public string Mail { get; set; }
        public int Coins { get; set; }
    }
}
