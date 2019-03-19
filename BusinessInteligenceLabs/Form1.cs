using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BusinessInteligenceLabs
{
    public partial class Form1 : Form
    {

        #region Get Connection strings
        private string GetDestinationDbConnectionString =>
            Properties.Settings.Default.DestinationDatabaseConnectionString;

        private string GetDataSet1DbConnectionString =>
            Properties.Settings.Default.Data_set_1ConnectionString;

        #endregion
        bool IsWeekend(DateTime date) => date.ToString("dddd") == "Sunday" || date.ToString("dddd") == "Sunday";
        public Form1()
        {
            InitializeComponent();
        }


        private async void btnGetDates_ClickAsync(object sender, EventArgs e)
        {
            var dates = new List<DateTime>();
            lstDates.DataSource = null;
            lstDates.Items.Clear();

            using (OleDbConnection connection = new OleDbConnection(GetDataSet1DbConnectionString))
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
            Debug.WriteLine("Unique Dates in a list "+ dates.Distinct().Count());
            InsertTimeDimensionRecords(dates);
        }


        private void InsertTimeDimensionRecords(IEnumerable<DateTime> dates)
        {
            var connectionString = Properties.Settings.Default.DestinationDatabaseConnectionString;
           
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                foreach (var date in dates)
                {
                    if (!IsDateIsPresentInDatabase(connection, date))
                    {
                        InsertDate(connection, date);
                    }
                }
                
              }

          
        }

        private bool IsDateIsPresentInDatabase(SqlConnection connection, DateTime date)
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
                                                    VALUES (@dayName, @dayNumber, @monthName, @monthNumber, @weekNumber, @year, @weekend, @date, @dayOfYear)", connection);

            SqlParameter[] parameterList = 
            {
                new SqlParameter("@dayName", date.ToString("dddd")),
                new SqlParameter("@dayNumber", date.DayOfWeek),
                new SqlParameter("@monthName", date.ToString("MMMM")),
                new SqlParameter("@monthNumber", date.Month),
                new SqlParameter("@weekNumber", (int)date.DayOfYear/7),
                new SqlParameter("@year", date.Year),
                new SqlParameter("@weekend", IsWeekend(date)),
                new SqlParameter("@date", date),
                new SqlParameter("@dayOfYear", date.DayOfYear)
            };
            insertCommand.Parameters.AddRange(parameterList);

            using (SqlDataReader reader = insertCommand.ExecuteReader())
            {
                Console.WriteLine($"Records affected {reader.RecordsAffected}");
            }

        }
    }
}
