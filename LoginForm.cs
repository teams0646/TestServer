using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TestServer
{
    public partial class LoginForm : Form
    {

        public static bool getLoginStatus() {
            
            return true;
            //return loginSuccessful;
        }

        public static bool loginSuccessful = false;
        string connectionString = buildConnectionString();

        private static string buildConnectionString()
        {
            string serverName = "localhost";
            string databaseName = "TestDatabase";
            string userName = "test";
            string password = "root";

            SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder();
            builder.DataSource = serverName;
            builder.InitialCatalog = databaseName;
            builder.UserID = userName;
            builder.Password = password;

            string connectionString = builder.ConnectionString;

            return builder.ConnectionString;
        }

        public LoginForm()
        {
            InitializeComponent();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {

            // Check if the user entered a valid username and password
            string username = txtUsername.Text;
            string password = txtPassword.Text;
            //bool loginSuccessful = ValidateUser(username, password);
            loginSuccessful = false;
            if (username == "root" && password == "rootroot")
            {
                loginSuccessful = true;
            }

            if (loginSuccessful)
            {
                MessageBox.Show("Success!");
                this.Hide(); // Hide the login form
                // Instantiate and initialize the main window form
                // Show the main window form
                
            }
            else
            {
                MessageBox.Show("Invalid username or password. Please try again.");
            }
        }


        private bool ValidateUser(string username, string password)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string query = "SELECT COUNT(*) FROM Users WHERE Username = @Username AND Password = @Password";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@Username", username);
                command.Parameters.AddWithValue("@Password", password);
                int count = (int)command.ExecuteScalar();

                if (count == 1)
                {
                    // Valid username and password
                    return true;
                }
                else
                {
                    // Invalid username or password
                    return false;
                }
            }
        }

        
    }

}
