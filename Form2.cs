using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Nikitina_kursach
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }
        private void label1_Click(object sender, EventArgs e)
        {

        }
        private void button1_Click(object sender, EventArgs e)
        {
            using (var connection = new SqliteConnection("Data Source=usersdata.db"))
            {
                connection.Open();
                SqliteCommand command = new SqliteCommand();
                command.Connection = connection;
                command.CommandText = "CREATE TABLE IF NOT EXISTS Tasks(_taskId INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT UNIQUE, Date TEXT NOT NULL, TextTask TEXT NOT NULL, NumberTask INTEGER NOT NULL DEFAULT 0, OwnerId INTEGER NOT NULL, FOREIGN KEY (OwnerId) REFERENCES Users (_id))";
                command.ExecuteNonQuery();
            }
            using (var connection = new SqliteConnection("Data Source=usersdata.db"))
            {
                connection.Open();
                SqliteCommand command = new SqliteCommand();
                command.Connection = connection;
                command.CommandText = "CREATE TABLE IF NOT EXISTS Users(_id INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT UNIQUE, Name TEXT NOT NULL, Login TEXT NOT NULL UNIQUE, Password TEXT NOT NULL)";
                command.ExecuteNonQuery();
            }
            string sqlExpression = $"SELECT * FROM Users WHERE Login = '{textBox1.Text}' AND Password = '{textBox2.Text}'";
            using (var connection = new SqliteConnection("Data Source=usersdata.db"))
            {
                connection.Open();
                SqliteCommand command = new SqliteCommand(sqlExpression, connection);
                using (SqliteDataReader reader = command.ExecuteReader()) if (reader.HasRows)
                        while (reader.Read())
                        {
                            var id = reader.GetValue(0);
                            var name = reader.GetValue(1);
                            var login = reader.GetValue(2);
                            var password = reader.GetValue(3);
                            Form1 f = new Form1(int.Parse(id.ToString()));
                            f.Show();
                            this.Hide();
                        }
                    else MessageBox.Show("Пользователя с такими данными не существует. Зарегистрируйтесь или проверьте правильность введенных данных");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            using (var connection = new SqliteConnection("Data Source=usersdata.db"))
            {
                connection.Open();
                SqliteCommand command = new SqliteCommand();
                command.Connection = connection;
                command.CommandText = $"INSERT INTO Users (Name, Login, Password) VALUES ('{textBox1.Text}', '{textBox1.Text}', '{textBox2.Text}')";
                try
                {
                    command.ExecuteNonQuery();
                    MessageBox.Show("Успешно зарегистрирован");
                }
                catch
                {
                    string sqlExpression = $"SELECT * FROM Users WHERE Login = '{textBox1.Text}'";
                    command = new SqliteCommand(sqlExpression, connection);
                    using (SqliteDataReader reader = command.ExecuteReader())
                    {
                        if (reader.HasRows) MessageBox.Show("Пользователь с данным логином уже существует."); 
                        else MessageBox.Show("Произошла ошибка. Попробуйте снова.");
                    }
                }  
            }  
        }
    }
}
