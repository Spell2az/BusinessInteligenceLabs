using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BusinessInteligenceLabs
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }


        private async void btnGetDates_ClickAsync(object sender, EventArgs e)
        {
            var dates = new List<DateTime>();
            lstDates.DataSource = null;
            lstDates.Items.Clear();

            var connectionString = Properties.Settings.Default.Data_set_1ConnectionString;

            using (OleDbConnection connection = new OleDbConnection(connectionString))
            {
                connection.Open();
                var getDates = new OleDbCommand(@"SELECT [Order Date], [Ship Date]
                                                FROM Sheet1", connection);

                var reader = await getDates.ExecuteReaderAsync();
                while (await reader.ReadAsync())
                {
                   dates.Add(reader.GetDateTime(0));
                   dates.Add(reader.GetDateTime(1));
                }
            }

            lstDates.DataSource = dates.Select(d => d.Date).ToList();
        }


        private async void insertTimeDimension(IEnumerable<DateTime> dates)
        {
            var connectionString = Properties.Settings.Default.DestinationDatabaseConnectionString;
            var insertCommand = new SqlCommand(@"INSERT INTO Time (dayName, dayNumber, monthName, monthNumber, weekNumber, year, weekend, date, dayOfYear)
                                                    VALUES (@dayName, @dayNumber, @monthName, @monthNumber, @weekNumber, @year, @weekend, @date, @dayOfYear)");
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                CheckIfDateIsPresentInDatabase(connection, new DateTime());
              }

          
        }

        private bool CheckIfDateIsPresentInDatabase(SqlConnection connection, DateTime date)
        {
            bool isPresent;

            var command = new SqlCommand("SELECT id from Time where date = @date", connection);
            command.Parameters.Add(new SqlParameter("@date", date));

            using (SqlDataReader reader = command.ExecuteReader())
            {
                isPresent = reader.HasRows;
            }

            return isPresent;
        }

        private void InsertDate(SqlConnection connection, DateTime date)
        {
            var insertCommand = new SqlCommand(@"INSERT INTO Time (dayName, dayNumber, monthName, monthNumber, weekNumber, year, weekend, date, dayOfYear)
                                                    VALUES (@dayName, @dayNumber, @monthName, @monthNumber, @weekNumber, @year, @weekend, @date, @dayOfYear)");

            SqlParameter[] parameterList = 
            {
                new SqlParameter("@dayName", date.DayOfWeek),
                new SqlParameter("@dayNumber", date.DayOfWeek),
                new SqlParameter("@monthName", date.DayOfWeek),
                new SqlParameter("@monthNumber", date.DayOfWeek),
                new SqlParameter("@weekNumber", date.DayOfWeek),
                new SqlParameter("@year", date.DayOfWeek),
                new SqlParameter("@weekend", IsWeekend(date)),
                new SqlParameter("@date", date),
                new SqlParameter("@dayOfYear", date.DayOfYear)
            };
            insertCommand.Parameters.AddRange(parameterList);


            bool IsWeekend(DateTime dateTime) => dateTime.ToString("dddd") == "Sunday" || dateTime.ToString("dddd") == "Sunday";
        }
    }
}
