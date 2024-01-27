using Npgsql;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace СП_1
{
    public partial class LoginForm : Form
    {

        private string connectionString = "Server=localhost;Port=5432;Username=postgres;Password=1825;Database=Schoolgg";

        public LoginForm()
        {
            InitializeComponent();
        }


        private void button2_Click(object sender, EventArgs e)
        {
            string enteredUsername = txtUsername.Text;
            string enteredPassword = txtPassword.Text;

            if (AuthenticateUser(enteredUsername, enteredPassword))
            {
                // Пользователь аутентифицирован
                ShowMainForm(enteredUsername);
              
            }
            else
            {
                MessageBox.Show("Неверное имя пользователя или пароль");
            }
        }

        private bool AuthenticateUser(string username, string password)
        {
            using (var connection = new NpgsqlConnection(connectionString))
            {
                connection.Open();

                using (var command = new NpgsqlCommand("SELECT * FROM users WHERE name = @Username AND password = @Password", connection))
                {
                    command.Parameters.AddWithValue("@Username", username);
                    command.Parameters.AddWithValue("@Password", password);

                    using (var reader = command.ExecuteReader())
                    {
                        return reader.HasRows;
                    }
                }
            }
        }

        private void txtUsername_TextChanged(object sender, EventArgs e)
        {

        }

        private void ShowMainForm(string username)
        {
            // Создайте объект формы, на которой будет отображаться имя пользователя
            MainForm mainForm = new MainForm(username);
            mainForm.Show();

            // Закройте текущую форму входа
            this.Hide();
        }

        private void txtPassword_TextChanged(object sender, EventArgs e)
        {
           // textBox2.PasswordChar = '•';
        }
    }
}

    
