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
    public partial class Form3 : Form
    {
        public string currentDate;
        public int ownerId;
        public List<(int, int)> taskIds = new List<(int,int)> ();
        public Form3(string date, int userId)
        {
            InitializeComponent();
            currentDate = date;
            ownerId = userId;
            label4.Text = "";
            string sqlExpression = $"SELECT * FROM Tasks WHERE OwnerId = '{ownerId}' AND Date = '{currentDate}'";
            using (var connection = new SqliteConnection("Data Source=usersdata.db"))
            {
                connection.Open();
                SqliteCommand command = new SqliteCommand(sqlExpression, connection);
                int ind = 0;
                using (SqliteDataReader reader = command.ExecuteReader()) if (reader.HasRows)
                        while (reader.Read())
                        {
                            var id = reader.GetValue(0);
                            var text = reader.GetValue(2);
                            label4.Text += $"{++ind}. {text}\n";
                            taskIds.Add((int.Parse(id.ToString()), ind));
                        }
                foreach (var item in taskIds)
                {
                    command.CommandText = $"UPDATE Tasks SET NumberTask = '{item.Item2}' WHERE _taskId = {item.Item1}";
                    command.ExecuteNonQuery();
                }
                taskIds.Clear();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            using (var connection = new SqliteConnection("Data Source=usersdata.db"))
            {
                connection.Open();
                SqliteCommand command = new SqliteCommand();
                command.Connection = connection;
                command.CommandText = $"INSERT INTO Tasks (Date, TextTask, OwnerId) VALUES ('{currentDate}', '{textBox1.Text}', '{ownerId}')";
                command.ExecuteNonQuery();
            }
            label4.Text = "";
            string sqlExpression = $"SELECT * FROM Tasks WHERE OwnerId = '{ownerId}' AND Date = '{currentDate}'";
            using (var connection = new SqliteConnection("Data Source=usersdata.db"))
            {
                connection.Open();
                SqliteCommand command = new SqliteCommand(sqlExpression, connection);
                int ind = 0;
                using (SqliteDataReader reader = command.ExecuteReader()) if (reader.HasRows)
                        while (reader.Read())
                        {
                            var id = reader.GetValue(0);
                            var text = reader.GetValue(2);
                            label4.Text += $"{++ind}. {text}\n";
                            taskIds.Add((int.Parse(id.ToString()), ind));   
                        }
                foreach(var item in taskIds)
                {
                    command.CommandText = $"UPDATE Tasks SET NumberTask = '{item.Item2}' WHERE _taskId = {item.Item1}";
                    command.ExecuteNonQuery();
                }
                taskIds.Clear();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            using (var connection = new SqliteConnection("Data Source=usersdata.db"))
            {
                connection.Open();
                SqliteCommand command = new SqliteCommand();
                command.Connection = connection;
                command.CommandText = $"DELETE FROM Tasks WHERE NumberTask = '{textBox2.Text}' AND Date = '{currentDate}'";
                command.ExecuteNonQuery();
            }
            label4.Text = "";
            string sqlExpression = $"SELECT * FROM Tasks WHERE OwnerId = '{ownerId}' AND Date = '{currentDate}'";
            using (var connection = new SqliteConnection("Data Source=usersdata.db"))
            {
                connection.Open();
                SqliteCommand command = new SqliteCommand(sqlExpression, connection);
                int ind = 0;
                using (SqliteDataReader reader = command.ExecuteReader()) if (reader.HasRows)
                        while (reader.Read())
                        {
                            var id = reader.GetValue(0);
                            var text = reader.GetValue(2);
                            label4.Text += $"{++ind}. {text}\n";
                            taskIds.Add((int.Parse(id.ToString()), ind));
                        }
                foreach (var item in taskIds)
                {
                    command.CommandText = $"UPDATE Tasks SET NumberTask = '{item.Item2}' WHERE _taskId = {item.Item1}";
                    command.ExecuteNonQuery();
                }
                taskIds.Clear();
            }
        }
    }
}
