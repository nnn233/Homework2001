using Npgsql;
using Timer = System.Windows.Forms.Timer;

namespace Homework2001
{
    public partial class Form1 : Form
    {
        int count = 0;
        readonly Timer timer;
        public Form1()
        {
            InitializeComponent();
            timer = new Timer();
            timer.Tick += new EventHandler(timer_Tick);
            timer.Interval = 20000;
        }

        private void timer_Tick(object? sender, EventArgs e)
        {
            Enabled = true;
            count = 0;
            timer.Stop();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            User? user = CheckLogin(LoginTextBox.Text.Trim());
            if (user != null && CheckPassword(PasswordTextBox.Text.Trim(), user))
            {
                MessageBox.Show("Авторизация прошла успешно");
            }
        }

        private User? CheckLogin(string login)
        {
            if (login == "")
                MessageBox.Show("Поле Логин не может быть пустым");
            else if (GetUserByLogin(login) == null)
            {
                MessageBox.Show("Пользователь с таким логином не найден");
                error();
            }
            else return GetUserByLogin(login);
            return null;
        }

        private Boolean CheckPassword(string password, User user)
        {
            if (password == "")
                MessageBox.Show("Поле Пароль не может быть пустым");
            else if (user.Password != password)
            {
                MessageBox.Show("Неверный пароль");
                error();
            }
            else return true;
            return false;
        }

        private void error()
        {
            count++;
            if (count == 3)
            {
                MessageBox.Show("Превышен лимит ввода логина и пароля, форма будет заблокирована на 20 секунд");
                Enabled = false;
                timer.Start();
            }
        }

        private User GetUserByLogin(string login)
        {
            var connection = new NpgsqlConnection("Host=localhost:5432;" +
            "Username=postgres;" +
            "Password=25481;" +
            "Database=UserDatabase");
            connection.Open();
            string commandText = $"SELECT * FROM users WHERE login='{login}'";
            NpgsqlCommand cmd = new NpgsqlCommand(commandText, connection);
            var reader = cmd.ExecuteReader();
            User result = null;
            while (reader.Read())
            {
                result = new()
                {
                    Id = (int)reader.GetValue(0),
                    Login = (string)reader.GetValue(1),
                    Password = (string)reader.GetValue(2)
                };
            }
            reader.Close();
            return result;
        }
    }
}