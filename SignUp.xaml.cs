using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using MessageBox = System.Windows.Forms.MessageBox;

namespace LoginApp
{
    /// <summary>
    /// Interaction logic for SignUp.xaml
    /// </summary>
    public partial class SignUp : Window
    {
        string sqlConnection = @"Data Source=LAB108PC04\SQLEXPRESS; Initial Catalog=loginDB_eric; Integrated Security=True";

        public SignUp()
        {
            InitializeComponent();
        }
       
        private void submit_button_Click(object sender, RoutedEventArgs e)
        {

            try
            {


                if (CheckUsername() && CheckNames() && CheckEmail() && CheckPassword())
                {

                    SqlConnection conn = new SqlConnection(sqlConnection);
                    conn.Open();
                    string query = $"Insert into SignCred(Username, FirstName, LastName, Email, Password) values " +
                        $"('{username.Text}', '{firstname.Text}', '{lastname.Text}', '{user_email.Text}', '{user_pass.Password}')";

                    SqlCommand cmd = new SqlCommand(query, conn);

                    cmd.ExecuteNonQuery();


                    MessageBox.Show($"Created Account for {firstname.Text} {lastname.Text} \n Username: {username.Text}  Password: {user_pass.Password} \n Email: {user_email.Text}");
                }
                else
                {
                    MessageBox.Show("Not succesfull");
                }

            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }

        }

        private bool CheckUsername()
        {
            if(username.Text == string.Empty)
            {
                MessageBox.Show("Username Not Complete", "Incorrect Username!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            return true;
        }

        private bool CheckEmail()
        {
            if(user_email.Text == string.Empty || !user_email.Text.Contains("@"))
            {
                MessageBox.Show("Email Not Complete", "Incorrect Email!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            return true;
        }

        private bool CheckNames()
        {
            if(firstname.Text == string.Empty || lastname.Text == string.Empty)
            {
                MessageBox.Show("Enter Names", "Incorrect Names!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            return true;
        }

        private bool CheckPassword()
        {
            string text = user_pass.Password;

            if (text.Length < 8 || !PasswordContainsChar())
            {
                MessageBox.Show("Try New Password", "Password Not Correct!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            return true;
        }

        private bool PasswordContainsChar()
        {
            char[] arr = { '@', '!', '#', '$', '%', '^', '&', '*' };
            

            foreach(char c in arr)
            {
                if (user_pass.Password.Contains(c.ToString()))
                {
                    return true;
                }
            }
            return false;

        }

        private void usernameClear(object sender, RoutedEventArgs e)
        {
            username.Text = string.Empty;
        }

        private void firstnameClear(object sender, RoutedEventArgs e)
        {
            firstname.Text = string.Empty;

        }

        private void lastnameClear(object sender, RoutedEventArgs e)
        {
            lastname.Text = string.Empty;

        }

        private void emailClear(object sender, RoutedEventArgs e)
        {
            user_email.Text = string.Empty;

        }

        private void open_login_button_Click(object sender, RoutedEventArgs e)
        {
          
            

            LoginWindow loginWindow = new LoginWindow();

            loginWindow.Show();

            this.Close();
        }
    }
}
