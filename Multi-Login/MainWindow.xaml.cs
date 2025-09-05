using System;
using System.Collections.Generic;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Data.SqlClient;
using System.Configuration;

namespace Multi_Login
{
    /// <summary>
    /// Interação lógica para MainWindow.xam
    /// </summary>
    public partial class MainWindow : Window
    {
        SqlConnection con = new SqlConnection();
        SqlCommand com = new SqlCommand();
        SqlDataReader dr;

        public MainWindow()
        {
            InitializeComponent();
            con.ConnectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString.ToString();
        }

        private bool VerifyUser(String username, String password)
        {
            con.Open();
            com.Connection = con;
            com.CommandText = "SELECT status FROM Users WHERE username = '"+username+"' and password = '"+password+"'";
            dr = com.ExecuteReader();
            if (dr.Read())
            {
                if (Convert.ToBoolean(dr["Status"]) == true)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

        private bool CreateNewUser(String newUsername, String newPassword)
        {
            try
            {
                con.Open();
                com.Connection = con;
                com.CommandText = "INSERT INTO [dbo].[Users] (Username, Password, Status, Role) VALUES (@username, @password, 1, 'user');";
                com.Parameters.Clear();
                com.Parameters.AddWithValue("username", newUsername);
                com.Parameters.AddWithValue("password", newPassword);
                int rowsAffected = com.ExecuteNonQuery();
                return rowsAffected > 0;
            }
            catch (Exception e)
            {
                return false;
            }
            finally
            {
                con.Close();
            }

        }

        private void Border_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if(e.LeftButton == MouseButtonState.Pressed)
            {
                DragMove();
            }
            
        }

        private void bntLogin_Click(object sender, RoutedEventArgs e)
        {
            if(con.State == System.Data.ConnectionState.Open)
            {
                con.Close();
            }

            if(VerifyUser(txtUsername.Text, txtPassword.Password))
            {
                MessageBox.Show("Login Successfully","Congrats",MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                MessageBox.Show("Username or password is incorrect", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void bntExit_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            RegisterPanel.Visibility = Visibility.Collapsed;
            LoginPanel.Visibility = Visibility.Visible;
        }

        private void bntIr_Registrar_Click(object sender, RoutedEventArgs e)
        {
            LoginPanel.Visibility = Visibility.Collapsed;
            RegisterPanel.Visibility = Visibility.Visible;
        }

        private void btnRegister_Click(object sender, RoutedEventArgs e)
        {
            if(con.State == System.Data.ConnectionState.Open)
            {
                con.Close();
            }

            if (txtNewUsername.Text != null && txtNewPassword.Password != null)
            {
                if(txtNewPassword.Password == txtConfirmPassword.Password)
                {
                    if(CreateNewUser(txtNewUsername.Text, txtNewPassword.Password))
                    {
                        MessageBox.Show("Success on registering your new account", "Congrats", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                    else
                    {
                        MessageBox.Show("Error on registering your new account", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
            }
            
        }

    }
}
