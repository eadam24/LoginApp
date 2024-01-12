using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace LoginApp
{
    /// <summary>
    /// Interaction logic for LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        public LoginWindow()
        {
            InitializeComponent();
        }

        private void Submit(object sender, RoutedEventArgs e)
        {
            string sqlCon = @"Data Source=LAB108PC04\SQLEXPRESS; Initial Catalog=loginDB_eric; Integrated Security=True;";

            SqlConnection sqlConnection = new SqlConnection(sqlCon);

            try
            {
                if (sqlConnection.State == ConnectionState.Closed)
                    sqlConnection.Open();

                string Query = $"SELECT COUNT(1) FROM UserCred WHERE Username=@Username AND Password=@Password";

                SqlCommand cmd = new SqlCommand(Query, sqlConnection);
                
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("@Username", user_username.Text);

                cmd.Parameters.AddWithValue("@Password", user_password.Password);

                int count = Convert.ToInt32(cmd.ExecuteScalar());

                if (count == 1)
                {
                    MainWindow mw = new MainWindow();
                    mw.Show();
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Try again!");
                }

            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
        }
    }
}
