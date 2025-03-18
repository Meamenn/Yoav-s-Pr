using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using Model;

namespace ViewModel1.Data
{
    public class LeaderBoardDB : BaseDB
    {
        private string connectionString = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=\"C:\\Users\\Yoav\\source\\repos\\Meamenn\\Yoav-s-Pr\\Yoav's Pr\\App_Data\\Database1.mdf\";Integrated Security=True";
        private SqlConnection connection;
        private SqlCommand command;
        private SqlDataReader reader;

        public LeaderBoardDB()
        {
            connection = new SqlConnection(connectionString);
            command = new SqlCommand();
            command.Connection = connection;
        }

        public List<LeaderBoardEntry> SelectAll()
        {
            List<LeaderBoardEntry> leaderBoardList = new List<LeaderBoardEntry>();

            try
            {
                command.CommandText = "SELECT * FROM LeaderBoard";
                connection.Open();
                reader = command.ExecuteReader();

                while (reader.Read())
                {
                    leaderBoardList.Add(CreateModel());
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

            return leaderBoardList;
        }

        public LeaderBoardEntry SelectByUserId(int userId)
        {
            try
            {
                command.CommandText = $"SELECT * FROM LeaderBoard WHERE UserID = {userId}";
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

     

        private LeaderBoardEntry CreateModel()
        {
            return new LeaderBoardEntry
            {
                UserId = (int)reader["UserID"],
                Points = (int)reader["Points"]
            };
        }

        public int Insert(LeaderBoardEntry entry)
        {
            string sqlStr = $"INSERT INTO LeaderBoard (userId, score) " +
                            $"VALUES ({entry.UserId}, {entry.Points})";
            return SaveChanges(sqlStr);
        }

        public int Update(LeaderBoardEntry entry)
        {
            string sqlStr = $"UPDATE LeaderBoard SET userId={entry.UserId}, score={entry.Points} " +
                            $"WHERE Id={entry.UserId}";
            return SaveChanges(sqlStr);
        }

        public int Delete(LeaderBoardEntry entry)
        {
            string sqlStr = $"DELETE FROM LeaderBoard WHERE Id={entry.UserId}";
            return SaveChanges(sqlStr);
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
    }

    public class LeaderBoardEntry
    {
        public int UserId { get; set; }
        public int Points { get; set; }
    }
}


