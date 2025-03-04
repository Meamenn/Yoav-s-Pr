using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using Model1;

namespace ViewModel1.Data
{
    public class UsersDB : BaseDB
    {
        protected override BaseEntity NewEntity() => new User();

        protected override BaseEntity PopulateEntity(SqlDataReader reader)
        {
            return new User
            {
                Id = (int)reader["Id"],
                Username = reader["Username"].ToString(),
                Passcode = reader["Passcode"].ToString(),
                Mail = reader["Mail"].ToString(),
                Coins = (int)reader["Coins"]
            };
        }

        public override void Insert(BaseEntity entity)
        {
            var user = (User)entity;
            string sql = "INSERT INTO Users (Username, Passcode, Mail, Coins) VALUES (@Username, @Passcode, @Mail, @Coins)";
            var parameters = new Dictionary<string, object>
            {
                { "@Username", user.Username },
                { "@Passcode", user.Passcode },
                { "@Mail", user.Mail },
                { "@Coins", user.Coins }
            };
            ExecuteNonQuery(sql, parameters);
        }

        public override void Update(BaseEntity entity)
        {
            var user = (User)entity;
            string sql = "UPDATE Users SET Username = @Username, Passcode = @Passcode, Mail = @Mail, Coins = @Coins WHERE Id = @Id";
            var parameters = new Dictionary<string, object>
            {
                { "@Id", user.Id },
                { "@Username", user.Username },
                { "@Passcode", user.Passcode },
                { "@Mail", user.Mail },
                { "@Coins", user.Coins }
            };
            ExecuteNonQuery(sql, parameters);
        }

        public override void Delete(BaseEntity entity)
        {
            var user = (User)entity;
            string sql = "DELETE FROM Users WHERE Id = @Id";
            ExecuteNonQuery(sql, new Dictionary<string, object> { { "@Id", user.Id } });
        }

        protected override string GetTableName() => "Users";
    }
}