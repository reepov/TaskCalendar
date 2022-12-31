using Microsoft.Data.Sqlite;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace Nikitina_kursach
{
    public partial class Form1 : Form
    {
        public string[] month = new string[] { "Январь", "Февраль", "Март", "Апрель", "Май", "Июнь", "Июль", "Август", "Сентябрь", "Октябрь", "Ноябрь", "Декабрь" };
        public Button[] dates;
        public DateTime shown = DateTime.Now;
        public int userId;
        public string currentDay;
        public Form1(int Id)
        {
            userId = Id;
            InitializeComponent();
            dates = new Button[]
            {
                button1, button2, button3, button4, button5, button6, button7, button8,
                button9, button10, button11, button12, button13, button14, button15, button16, button17, button18,
                button19, button20, button21, button22, button23, button24, button25, button26, button27, button28,
                button29, button30, button31, button32, button33, button34, button35, button38, button39, button40, button41, button42, button43, button44
            };
            DateTime date = DateTime.Now;
            Remake_Calendar(new DateTime(date.Year, date.Month, 1));
            
        }
        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button36_Click(object sender, EventArgs e)
        {
            shown = shown.AddMonths(-1);
            Remake_Calendar(new DateTime(shown.Year, shown.Month, 1));
        }

        private void button37_Click(object sender, EventArgs e)
        {
            shown = shown.AddMonths(1);
            Remake_Calendar(new DateTime(shown.Year, shown.Month, 1));
        }
        private void Remake_Calendar(DateTime first)
        {
            DateTime date = DateTime.Now;
            label1.Text = $"{month[first.Month - 1]} {first.Year}";
            int k = 1;
            for (int i = 0; i < dates.Length; i++)      
            {
                dates[i].Visible = true;
                dates[i].BackColor = Color.White;
                if (k <= DateTime.DaysInMonth(first.Year, first.Month) && i >= ((int)first.DayOfWeek - 1 + 7) % 7)
                {
                    dates[i].Text = k++.ToString();
                }
                else {
                    dates[i].Visible = false;
                    }

            }
            if(first.Year == date.Year && first.Month == date.Month) dates[(int)first.DayOfWeek + date.Day - 2].BackColor = Color.Aquamarine;
            button36.Text = $"{month[first.AddMonths(-1).Month - 1]} {first.AddMonths(-1).Year}";
            button37.Text = $"{month[first.AddMonths(1).Month - 1]} {first.AddMonths(1).Year}";
            label9.Text = "Список задач на месяц";
            label10.Text = "";
            string sqlExpression = $"SELECT * FROM Tasks WHERE OwnerId = '{userId}' AND Date LIKE '%.{first.ToString("MM.yyyy")}' ORDER BY Date";
            using (var connection = new SqliteConnection("Data Source=usersdata.db"))
            {
                connection.Open();
                SqliteCommand command = new SqliteCommand(sqlExpression, connection);
                int ind = 0;
                using (SqliteDataReader reader = command.ExecuteReader()) if (reader.HasRows)
                        while (reader.Read())
                        {
                            var id = reader.GetValue(0);
                            var data = reader.GetValue(1);
                            var text = reader.GetValue(2);
                            if (DateTime.Parse(data.ToString()) < date)
                            {
                                TimeSpan t = date - DateTime.Parse(data.ToString());
                                int srok = t.Days;
                                label10.Text += $"{++ind}. {text} (Дней просрочено: {Math.Abs(srok)}) \n";
                                label10.Text += $"\tДата выполнения задачи: {data} \n";
                            }
                            else
                            {
                                int srok = (int)(DateTime.Parse(data.ToString()) - date).TotalDays + 1;
                                label10.Text += $"{++ind}. {text} (Дней осталось: {srok}) \n";
                                label10.Text += $"\tДата выполнения задачи: {data} \n";
                            }                   
                        }
            }
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var button = sender as Button;
            currentDay = $"{button.Text}.{shown.ToString("MM.yyyy")}";
            Form3 form = new Form3(currentDay, userId);
            form.Show();
        }

        private void button45_Click(object sender, EventArgs e)
        {
            Remake_Calendar(new DateTime(shown.Year, shown.Month, 1));
        }

        private void button46_Click(object sender, EventArgs e)
        {
            Form5 form = new Form5(shown.Year.ToString(), userId);
            form.Show();
        }

        private void button48_Click(object sender, EventArgs e)
        {

        }

        private void button47_Click(object sender, EventArgs e)
        {
            Form4 f = new Form4();
            f.Show();
        }
    }
}