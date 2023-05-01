using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TestServer.Data;
using TestServer.Networking;
using TestServer.Networking.TestServer;

namespace TestServer
{
    public partial class MainWindowForm : Form
    {
        private readonly Server server;
        private Dictionary<string, Client> clients = assignClients();

        private static Dictionary<string, Client> assignClients(){
            Dictionary<string, Client> generatedClients = new Dictionary<string, Client>
            {
                { "client1", new Client("client1", "192.168.0.1") }
            };

            return generatedClients;
        }

#pragma warning disable CS0169 // The field 'MainWindowForm.db' is never used
        private Database db;
#pragma warning restore CS0169 // The field 'MainWindowForm.db' is never used
        string connectionString = "Data Source=myServerAddress;Initial Catalog=myDataBase;User Id=myUsername;Password=myPassword;";

        public MainWindowForm()
        {
            InitializeComponent();

            server = new Server(8080);
            server.Start();
        }

        private void OnClientConnected(object sender, ClientEventArgs e)
        {
            // Отримати ім'я клієнта
            string clientName = e.Client.ClientName;

            // Додати клієнта до списку на формі
            this.Invoke((MethodInvoker)delegate
            {
                this.clientsListBox.Items.Add(clientName);
            });

            // Відправити повідомлення про підключення всім клієнтам
            string message = $"{clientName} has joined the chat.";
        }


        private void OnClientDisconnected(object sender, ClientEventArgs e)
        {
            // Виконуємо зміну елементів на формі в головному потоці за допомогою Invoke
            Invoke((Action)delegate
            {
                // Отримуємо індекс клієнта в списку
                int index = clientsListBox.Items.IndexOf(e.Client);

                // Якщо індекс не дорівнює -1, то клієнт знайдений в списку
                if (index != -1)
                {
                    // Видаляємо клієнта зі списку на формі
                    clientsListBox.Items.RemoveAt(index);
                }
            });
        }


        private void OnTestAssignedToGroup(object sender, TestEventArgs e)
        {
            // Update the corresponding fields on the form
            GroupTest groupTest = e.GroupTest;
            int groupId = groupTest.GroupId;
            int testId = groupTest.TestId;

            // Find the group name
            string groupName = "";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand("SELECT GroupName FROM UserGroups WHERE GroupId = @groupId", connection);
                command.Parameters.AddWithValue("@groupId", groupId);
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    groupName = reader.GetString(0);
                }
                reader.Close();
            }

            // Find the test name
            string testName = "";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand("SELECT TestName FROM Tests WHERE TestId = @testId", connection);
                command.Parameters.AddWithValue("@testId", testId);
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    testName = reader.GetString(0);
                }
                reader.Close();
            }

            // Update the form
            lblGroupTestName.Text = testName;
            lblGroupTestGroup.Text = groupName;
        }


        private void OnTestAssignedToUser(object sender, TestEventArgs e)
        {
            // Get the user ID and test ID from the event arguments
            int userId = e.UserId;
            int testId = e.TestId;

            // TODO: Add code to assign the test to the user and update relevant fields on the form
            // For example:
            User user = GetUserById(userId);
            Test test = GetTestById(testId);

            if (user != null && test != null)
            {
                // Assign the test to the user
                UserTest userTest = new UserTest(user, test);
                user.UserTests.Add(userTest);

                // Update relevant fields on the form
                UpdateUserTestsList();
                ShowSuccessMessage($"Test '{test.Title}' has been assigned to user '{user.Username}'.");
            }
            else
            {
                ShowErrorMessage("Failed to assign test to user.");
            }
        }

        private void ShowErrorMessage(string errorMessage)
        {
            // Display error message to user (e.g. using a MessageBox)
            MessageBox.Show(errorMessage, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void ShowSuccessMessage(string successMessage)
        {
            // Display success message to user (e.g. using a MessageBox)
            MessageBox.Show(successMessage, "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void UpdateUserTestsList()
        {
            User currentUser = new User("username", "password",new Group("guest"));
            SqlConnection connection = new SqlConnection(connectionString);
            DataGridView dgvUserTests = new DataGridView();
            // Query database to get user's assigned tests and update UI accordingly
            // For example, if using a DataGridView to display the list of tests, you could do:
            string query = "SELECT Tests.TestName, UserTests.AssignedOn FROM Tests INNER JOIN UserTests ON Tests.TestId = UserTests.TestId WHERE UserTests.UserId = @userId;";
            SqlCommand cmd = new SqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@userId", currentUser.Id);
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            adapter.Fill(dt);
            dgvUserTests.DataSource = dt;
        }


        private Test GetTestById(int testId)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "SELECT * FROM Tests WHERE TestId = @TestId";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@TestId", testId);

                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    Test test = new Test()
                    {
                        TestId = Convert.ToInt32(reader["TestId"]),
                        TestName = Convert.ToString(reader["TestName"]),
                        Description = Convert.ToString(reader["TestDescription"]),
                        TestDuration = Convert.ToInt32(reader["TestDuration"])
                    };
                    return test;
                }

                return null;
            }
        }

        private User GetUserById(int userId)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "SELECT * FROM Users WHERE UserId = @UserId";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@UserId", userId);

                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    User user = new User()
                    {
                        Id = Convert.ToInt32(reader["UserId"]),
                        Username = Convert.ToString(reader["UserName"]),
                        Password = Convert.ToString(reader["Password"]),
                        FirstName = Convert.ToString(reader["FirstName"]),
                        LastName = Convert.ToString(reader["LastName"]),
                        Email = Convert.ToString(reader["Email"])
                    };
                    return user;
                }

                return null;
            }
        }

        private void OnTestResultsRequested(object sender, TestEventArgs e)
        {
            User user = GetUserById(e.UserId);
            Test test = GetTestById(e.TestId);

            if (user == null || test == null)
            {
                ShowErrorMessage("User or Test not found.");
                return;
            }

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "SELECT * FROM TestResults WHERE UserId = @UserId AND TestId = @TestId";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@UserId", user.Id);
                command.Parameters.AddWithValue("@TestId", test.TestId);

                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    // Test results found for user and test, display on form
                    // ...
                }
                else
                {
                    ShowErrorMessage("Test results not found for this user and test.");
                }
            }
        }


        private void btnCreateUser_Click(object sender, EventArgs e)
        {
            // Відкрити форму для створення нового користувача
            CreateUserForm createUserForm = new CreateUserForm();
            createUserForm.ShowDialog();

            if (createUserForm.DialogResult == DialogResult.OK)
            {
                // Отримати дані нового користувача з форми
                string name = createUserForm.txtName.Text;
                string email = createUserForm.txtEmail.Text;
                string password = createUserForm.txtPassword.Text;

                // Створити нового користувача
                User newUser = new User(name,password, new Group("guest"));

                // Зберегти нового користувача в базі даних
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string query = "INSERT INTO Users (Name, Email, Password) VALUES (@Name, @Email, @Password)";
                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@Name", newUser.Username);
                    command.Parameters.AddWithValue("@Email", newUser.Email);
                    command.Parameters.AddWithValue("@Password", newUser.Password);
                    command.ExecuteNonQuery();
                }

                // Оновити список користувачів на формі
                UpdateUserList();
                ShowSuccessMessage("New user created successfully.");
            }
        }

        private void UpdateUserList()
        {
            // Clear the current user list
            dgvUsers.Rows.Clear();

            // Query the database for all users
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string query = "SELECT * FROM Users";
                SqlCommand command = new SqlCommand(query, connection);

                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    int id = reader.GetInt32(0);
                    string username = reader.GetString(1);
                    string password = reader.GetString(2);
                    string firstName = reader.GetString(3);
                    string lastName = reader.GetString(4);

                    // Add a new row to the user list with the user's information
                    dgvUsers.Rows.Add(id, username, password, firstName, lastName);
                }

                reader.Close();
            }
        }


        private void btnCreateGroup_Click(object sender, EventArgs e)
        {
            // Відкрити форму для створення нової групи користувачів
            CreateGroupForm createGroupForm = new CreateGroupForm();
            createGroupForm.ShowDialog();

            if (createGroupForm.DialogResult == DialogResult.OK)
            {
                // Отримати дані нової групи користувачів з форми
                string name = createGroupForm.txtName.Text;

                // Створити нову групу користувачів
                UserGroup newGroup = new UserGroup(name);

                // Зберегти нову групу користувачів в базі даних
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string query = "INSERT INTO UserGroups (Name) VALUES (@Name)";
                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@Name", newGroup.GroupName);
                    command.ExecuteNonQuery();
                }

                // Оновити список груп користувачів на формі
                UpdateGroupList();
                ShowSuccessMessage("New group created successfully.");
            }
        }

        private void UpdateGroupList()
        {
            // Очистити список груп користувачів на формі
            lstGroups.Items.Clear();

            // Отримати список груп користувачів з бази даних
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string query = "SELECT * FROM UserGroups";
                SqlCommand command = new SqlCommand(query, connection);
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    int groupId = (int)reader["Id"];
                    string groupName = (string)reader["Name"];

                    // Додати групу користувачів до списку на формі
                    ListViewItem item = new ListViewItem(groupName);
                    item.Tag = groupId;
                    lstGroups.Items.Add(item);
                }
            }
        }


        private void btnAssignTestToGroup_Click(object sender, EventArgs e)
        {
            // Переконатися, що вибрано групу та тест
            if (cmbGroups.SelectedItem == null || cmbTests.SelectedItem == null)
            {
                ShowErrorMessage("Виберіть групу та тест для призначення.");
                return;
            }

            // Отримати обраний тест та групу
            Test selectedTest = (Test)cmbTests.SelectedItem;
            UserGroup selectedGroup = (UserGroup)cmbGroups.SelectedItem;

            // Перевірити, чи тест ще не був призначений для цієї групи
            if (selectedGroup.Tests.Contains(selectedTest))
            {
                ShowErrorMessage("Тест вже призначений для цієї групи.");
                return;
            }

            // Додати тест до групи
            selectedGroup.Tests.Add(selectedTest);

            // Оновити інформацію на формі
            UpdateGroupTestsList();
            ShowSuccessMessage($"Тест '{selectedTest.Title}' було призначено для групи '{selectedGroup.GroupName}'.");
        }

        private void UpdateGroupTestsList()
        {
            // Clear the DataGridView
            dgvGroupTests.Rows.Clear();

            // Get the selected group
            UserGroup selectedGroup = (UserGroup)lstGroups.SelectedItem;

            // If no group is selected, return
            if (selectedGroup == null)
            {
                return;
            }

            // Get the tests assigned to the selected group
            List<Test> tests = GetTestsByGroupId(selectedGroup.Id);

            // Add the tests to the DataGridView
            foreach (Test test in tests)
            {
                dgvGroupTests.Rows.Add(test.TestId, test.TestName);
            }

            // If no tests are assigned to the selected group, show a message
            if (tests.Count == 0)
            {
                lblGroupTestsEmpty.Visible = true;
                dgvGroupTests.Visible = false;
            }
            else
            {
                lblGroupTestsEmpty.Visible = false;
                dgvGroupTests.Visible = true;
            }
        }

        private List<Test> GetTestsByGroupId(int id)
        {
            List<Test> tests = new List<Test>();

            // Query the database for all tests assigned to the group
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string query = "SELECT Tests.Id, Tests.Name, Tests.Description, Tests.TimeLimit FROM TestGroups INNER JOIN Tests ON TestGroups.TestId = Tests.Id WHERE TestGroups.GroupId = @GroupId";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@GroupId", id);

                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    int testId = reader.GetInt32(0);
                    string name = reader.GetString(1);
                    string description = reader.GetString(2);
                    int timeLimit = reader.GetInt32(3);

                    // Create a new Test object and add it to the list
                    Test test = new Test(testId, name, description, timeLimit);
                    tests.Add(test);
                }

                reader.Close();
            }

            return tests;
        }


        private void btnAssignTestToUser_Click(object sender, EventArgs e)
        {
            // Отримати вибраний тест та користувача зі списку
            Test selectedTest = (Test)cmbTests.SelectedItem;
            User selectedUser = (User)cmbUsers.SelectedItem;

            // Перевірити, чи обрані тест та користувач
            if (selectedTest == null || selectedUser == null)
            {
                ShowErrorMessage("Please select a test and a user.");
                return;
            }

            try
            {
                // Створити новий запис UserTest в базі даних
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand("INSERT INTO UserTests (UserId, TestId, AssignedDate) VALUES (@userId, @testId, @assignedDate)", connection);
                    command.Parameters.AddWithValue("@userId", selectedUser.Id);
                    command.Parameters.AddWithValue("@testId", selectedTest.TestId);
                    command.Parameters.AddWithValue("@assignedDate", DateTime.Now);
                    int rowsAffected = command.ExecuteNonQuery();
                    if (rowsAffected > 0)
                    {
                        ShowSuccessMessage($"Test '{selectedTest.Title}' has been assigned to user '{selectedUser.Username}'.");
                        UpdateUserTestsList();
                    }
                    else
                    {
                        ShowErrorMessage("Failed to assign test to user.");
                    }
                }
            }
            catch (Exception ex)
            {
                ShowErrorMessage($"Failed to assign test to user: {ex.Message}");
            }
        }


        private void btnViewTestResults_Click(object sender, EventArgs e)
        {
            // запитати результати тестування для відображення на формі
        }

        private void ServerForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            // Перевіряємо, чи є активні з'єднання з клієнтами
            if (clients.Count > 0)
            {
                // Якщо є, запитуємо користувача про бажання закрити сервер
                DialogResult result = MessageBox.Show("Є активні з'єднання з клієнтами. Бажаєте закрити сервер?", "Закриття сервера", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.No)
                {
                    // Якщо користувач відмовився, відміна закриття форми
                    e.Cancel = true;
                    return;
                }
                else
                {
                    // Якщо користувач погодився, відключаємо всі з'єднання з клієнтами
                    foreach (var client in clients.Values)
                    {
                        client.Disconnect();
                    }
                    clients.Clear();
                }
            }

            
            e.Cancel = false;
        }

        
    }
    }