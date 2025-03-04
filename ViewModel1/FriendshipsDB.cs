using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using Model;

namespace ViewModel1.Data
{
    public class FriendshipsDB : BaseDB
    {
        protected override BaseEntity NewEntity() => new Friendship(0, 0);

        protected override BaseEntity PopulateEntity(SqlDataReader reader)
        {
            return new Friendship((int)reader["UserId1"], (int)reader["UserId2"])
            {
                Id = (int)reader["Id"],
                User1Wins = (int)reader["User1Wins"],
                User2Wins = (int)reader["User2Wins"]
            };
        }

        public override void Insert(BaseEntity entity)
        {
            var friendship = (Friendship)entity;
            string sql = "INSERT INTO Friendships (UserId1, UserId2, User1Wins, User2Wins) VALUES (@UserId1, @UserId2, @User1Wins, @User2Wins)";
            var parameters = new Dictionary<string, object>
            {
                { "@UserId1", friendship.UserId1 },
                { "@UserId2", friendship.UserId2 },
                { "@User1Wins", friendship.User1Wins },
                { "@User2Wins", friendship.User2Wins }
            };
            ExecuteNonQuery(sql, parameters);
        }

        public override void Update(BaseEntity entity)
        {
            var friendship = (Friendship)entity;
            string sql = "UPDATE Friendships SET UserId1 = @UserId1, UserId2 = @UserId2, User1Wins = @User1Wins, User2Wins = @User2Wins WHERE Id = @Id";
            var parameters = new Dictionary<string, object>
            {
                { "@Id", friendship.Id },
                { "@UserId1", friendship.UserId1 },
                { "@UserId2", friendship.UserId2 },
                { "@User1Wins", friendship.User1Wins },
                { "@User2Wins", friendship.User2Wins }
            };
            ExecuteNonQuery(sql, parameters);
        }

        public override void Delete(BaseEntity entity)
        {
            var friendship = (Friendship)entity;
            string sql = "DELETE FROM Friendships WHERE Id = @Id";
            ExecuteNonQuery(sql, new Dictionary<string, object> { { "@Id", friendship.Id } });
        }

        protected override string GetTableName() => "Friendships";
    }
}