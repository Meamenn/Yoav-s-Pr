using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;

namespace Yoav_s_Pr
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // תרגיל 1
            string connectionString = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=\"C:\\Users\\Yoav\\source\\repos\\Yoav's Pr\\Yoav's Pr\\App_Data\\Database1.mdf\";Integrated Security=True";
            string query = "SELECT COUNT(*) FROM Users"; 

            SqlConnection connection = new SqlConnection(connectionString);
            connection.Open(); 

            SqlCommand command = new SqlCommand(query, connection);  

   
            int userCount = (int)command.ExecuteScalar();
            Console.WriteLine($"Number of users in the Users table: {userCount}");

            command.Dispose(); 
            connection.Close(); 
            connection.Dispose();

            //תרגיל 2

                string selectQuery = "SELECT * FROM Users";

                SqlConnection connection2 = new SqlConnection(connectionString);
                connection.Open(); 

                SqlDataAdapter dataAdapter = new SqlDataAdapter(selectQuery, connection);
                SqlCommandBuilder commandBuilder = new SqlCommandBuilder(dataAdapter);

                DataSet dataSet = new DataSet();
                dataAdapter.Fill(dataSet, "Users"); 
                DataTable table = dataSet.Tables["Users"];
                DataRow newUser = table.NewRow();
                newUser["username"] = "newUser";
                newUser["passcode"] = "newPass123";
                newUser["email"] = "newUser@example.com";
                newUser["coins"] = 0;
                table.Rows.Add(newUser);
                table.Rows[0]["username"] = "UpdatedUsername";
                table.Rows[1].Delete();
                dataAdapter.Update(dataSet, "Users");

                commandBuilder.Dispose();  
                dataAdapter.Dispose();
                connection2.Close();  
                connection2.Dispose();

            //תרגיל 3
          
            string query3 = "SELECT COUNT(*) FROM Users WHERE username = @username AND passcode = @passcode";  // אימות משתמש

            Console.Write("Enter username: ");
            string username3 = Console.ReadLine();

            Console.Write("Enter password: ");
            string password3 = Console.ReadLine();

            SqlConnection connection3 = new SqlConnection(connectionString);
            connection3.Open();

            SqlCommand command3 = new SqlCommand(query3, connection3);
            command3.Parameters.Add(new SqlParameter("@username", username3));
            command3.Parameters.Add(new SqlParameter("@passcode", password3));

            int userCount3 = (int)command3.ExecuteScalar();

            if (userCount3 > 0)
            {
                Console.WriteLine("Login successful.");
            }
            else
            {
                Console.WriteLine("Invalid username or password.");
            }

            command3.Dispose();  
            connection3.Close();  
            connection3.Dispose();  
        }
        }

    }
