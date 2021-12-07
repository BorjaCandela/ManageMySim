using ManageMySim.Forms;
using MySqlConnector;
using System;
using System.Data;
using System.Windows;

namespace ManageMySim.Classes
{
    internal class Utilities
    {

        // This method check if the database is reachable and load the Login Window
        public static void startProgram()
        {
            // String connection hardcoded, next step get the values from file
            string m_strMySQLConnectionString;
            m_strMySQLConnectionString = "server=localhost;userid=root;database=simdb";

            string strQuery = "SELECT 1 FROM DUAL;";  // Verify the connection

            // Do an sql connection try, if fails an exception is thrown
            try
            {

                using (MySqlConnection mysqlconnection = new MySqlConnection(m_strMySQLConnectionString))
                {
                    mysqlconnection.Open();
                    using (MySqlCommand cmd = mysqlconnection.CreateCommand())
                    {
                        cmd.CommandType = CommandType.Text;
                        cmd.CommandTimeout = 10;
                        cmd.CommandText = strQuery;
                        mysqlconnection.Close();

                        // Draw login window if the sql connection does not fail
                        Login login = new Login();
                        login.WindowStyle = WindowStyle.None;
                        login.ResizeMode = ResizeMode.NoResize;
                        login.Topmost = true;
                        login.Show();

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

        // This method returns the Name of the username inserted
        public static string GetName(string name)
        {
            string nombre = null;

            string m_strMySQLConnectionString;
            m_strMySQLConnectionString = "server=localhost;userid=root;database=simdb";

            string strQuery = "SELECT nameTec FROM maintainer WHERE username like '" + name + "';";  // VGet the name of the user


            using (MySqlConnection mysqlconnection = new MySqlConnection(m_strMySQLConnectionString))
            {
                mysqlconnection.Open();
                using (MySqlCommand cmd = mysqlconnection.CreateCommand())
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandTimeout = 10;
                    cmd.CommandText = strQuery;
                    mysqlconnection.Close();

                    // This is create and fill a virtual table
                    DataTable dt = new DataTable();
                    MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                    da.Fill(dt);

                    // ASsing the variable nombre with the first value of the DataTable
                    nombre = dt.Rows[0][0].ToString();

                }

                return nombre;
            }
        }



    }
}
