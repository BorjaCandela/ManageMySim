using MySqlConnector;
using System;
using System.Data;
using System.Windows;
using System.Windows.Controls;
using ManageMySim.Classes;

namespace ManageMySim.Forms
{

    public partial class Login : Window
    {
        public Login()
        {
            InitializeComponent();

            // Set the position at the center of the screen
            double screenWidth = SystemParameters.PrimaryScreenWidth;
            double screenHeight = SystemParameters.PrimaryScreenHeight;
            double windowWidth = Width;
            double windowHeight = Height;
            Left = (screenWidth / 2) - (windowWidth / 2);
            Top = (screenHeight / 2) - (windowHeight / 2);

            // Not sure what this does, something with the login
            Button ButtonLogin = new Button();
            ButtonLogin.Name = "ButtonLogin";
            ButtonLogin.Click += ButtonLogin_Click;
        }

        void ButtonLogin_Click(object sender, RoutedEventArgs e)
        {
            // Get the login Window values to try to login
            string username = textBoxUser.Text;
            string password = textBoxPassword.Password;

            // String connection hardcoded, next step get the values from file
            string m_strMySQLConnectionString;
            m_strMySQLConnectionString = "server=localhost;userid=root;database=simdb";

            // This must be changed to avoid SQL Injection using SqlCommand.Prepare
            string strQuery = "SELECT COUNT(*) FROM maintainer WHERE username='" + username + "' AND password='" + password + "';";

            // Try the credentials, if the user exists, load the Main Window, if not, throws a message
            try
            {
                using (MySqlConnection mysqlconnection = new MySqlConnection(m_strMySQLConnectionString))
                {
                    mysqlconnection.Open();
                    using (MySqlCommand cmd = mysqlconnection.CreateCommand())
                    {
                        cmd.CommandType = CommandType.Text;
                        cmd.CommandTimeout = 5;
                        cmd.CommandText = strQuery;
                        mysqlconnection.Close();

                        // This is create and fill a virtual table
                        DataTable dt = new DataTable();
                        MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                        da.Fill(dt);

                        // If the user exists, enable the MainWindow
                        if (dt.Rows[0][0].ToString() == "1")
                        {
                            Hide();

                            // New Users instance and declared the user's username
                            //Users user = new Users(username);  // This works to get the username but the complete name

                            string name = Utilities.GetName(username);
                            Users user = new Users(name);

                            // Called mainwindow with the user's username and the value true, to pass to the second iteration (Second constructor)
                            new MainWindow(true, user.getUsername()).Show();
                        }
                        else
                        {
                            MessageBox.Show("Invalid username or password");
                        }
                    }
                }

            }
            catch (MySqlException ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }
    }
}

