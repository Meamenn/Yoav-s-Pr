
using System.Data;
using System.Text;

namespace ViewModel1
{
    public class FriendshipsDB
    {
        private string connectionString = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=\"C:\\Users\\Yoav\\source\\repos\\Meamenn\\Yoav-s-Pr\\Yoav's Pr\\App_Data\\Database1.mdf\";Integrated Security=True";
        private SqlConnection connection;
        private SqlCommand command;
        private SqlDataReader reader;

        public FriendshipsDB()
        {
            connection = new SqlConnection(connectionString);
            command = new SqlCommand();
            command.Connection = connection;
        }

        public List<Friendship> SelectAll()
        {
            List<Friendship> friendshipsList = new List<Friendship>();

            try
            {
                command.CommandText = "SELECT * FROM Friendships";
                connection.Open();
                reader = command.ExecuteReader();

                while (reader.Read())
                {
                    friendshipsList.Add(CreateModel());
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

            return friendshipsList;
        }

        public Friendship SelectById(int id)
        {
            try
            {
                command.CommandText = $"SELECT * FROM Friendships WHERE Id = {id}";
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

        private Friendship CreateModel()
        {
            return new Friendship
            {
                Id = (int)reader["Id"],
                UserId1 = (int)reader["userId1"],
                UserId2 = (int)reader["userId2"],
                CreatedAt = (DateTime)reader["created_at"],
                User1Wins = (int)reader["User1Wins"],
                User2Wins = (int)reader["User2Wins"]
            };
        }
    }
    public class FriendshipsDB : BaseDB
    {
        public int Insert(Friendship friendship)
        {
            string sqlStr = string.Format("INSERT INTO Friendships (userId1, userId2, created_at, User1Wins, User2Wins) " +
                                          "VALUES ({0}, {1}, '{2}', {3}, {4})",
                                          friendship.UserId1, friendship.UserId2, friendship.CreatedAt, friendship.User1Wins, friendship.User2Wins);
            return SaveChanges(sqlStr);
        }

        public int Update(Friendship friendship)
        {
            string sqlStr = string.Format("UPDATE Friendships SET userId1={0}, userId2={1}, created_at='{2}', " +
                                          "User1Wins={3}, User2Wins={4} WHERE Id={5}",
                                          friendship.UserId1, friendship.UserId2, friendship.CreatedAt, friendship.User1Wins, friendship.User2Wins, friendship.Id);
            return SaveChanges(sqlStr);
        }

        public int Delete(Friendship friendship)
        {
            StringBuilder sqlBuilder = new StringBuilder();
            sqlBuilder.AppendFormat("DELETE FROM Friendships WHERE Id={0}", friendship.Id);
            return SaveChanges(sqlBuilder.ToString());
        }
    }
    public class Friendship
    {
        public int Id { get; set; }
        public int UserId1 { get; set; }
        public int UserId2 { get; set; }
        public DateTime CreatedAt { get; set; }
        public int User1Wins { get; set; }
        public int User2Wins { get; set; }
    }
}