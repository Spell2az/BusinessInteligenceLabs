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
using BusinessInteligenceLabs.Entities;

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
    bool IsWeekend(DateTime date) => date.ToString("dddd") == "Sunday" || date.ToString("dddd") == "Saturday";
    public Form1()
    {
      InitializeComponent();
    }


    private async void btnGetDates_ClickAsync(object sender, EventArgs e)
    {
      var dates = await GetCustomers();
      lstDates.DataSource = null;
      lstDates.Items.Clear();

     lstDates.DataSource = dates.Select(d => d.Name).ToList();
      Debug.WriteLine("Unique Dates in a list " + dates.Distinct().Count());
      //InsertTimeDimensionRecords(dates);
    }


    #region Insert range
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

    private void InsertProductDimensionRecords(IEnumerable<Product> products)
    {
      var connectionString = Properties.Settings.Default.DestinationDatabaseConnectionString;

      using (SqlConnection connection = new SqlConnection(connectionString))
      {
        connection.Open();

        foreach (var product in products)
        {
          if (!IsProductPresentInDatabase(connection, product))
          {
            InsertProduct(connection, product);
          }
        }
      }
    }

    private void InsertCustomerDimensionRecords(IEnumerable<Customer> customers)
    {
      var connectionString = Properties.Settings.Default.DestinationDatabaseConnectionString;

      using (SqlConnection connection = new SqlConnection(connectionString))
      {
        connection.Open();

        foreach (var customer in customers)
        {
          if (!IsCustomerPresentInDatabase(connection, customer))
          {
            InsertCustomer(connection, customer);
          }
        }
      }
    } 
    #endregion
    #region Check if entity is present functions 
    private bool IsProductPresentInDatabase(SqlConnection connection, Product product)
    {
      bool isPresent;

      var command = new SqlCommand("SELECT id from Product where product.Name = @name", connection);
      command.Parameters.Add(new SqlParameter("@name", product.Name));

      using (SqlDataReader reader = command.ExecuteReader())
      {
        isPresent = reader.HasRows;
      }

      return isPresent;
    }
    private bool IsCustomerPresentInDatabase(SqlConnection connection, Customer customer)
    {
      bool isPresent;

      var command = new SqlCommand("SELECT id from Customer where product.Name = @name", connection);
      command.Parameters.Add(new SqlParameter("@name", customer.Name));

      using (SqlDataReader reader = command.ExecuteReader())
      {
        isPresent = reader.HasRows;
      }

      return isPresent;
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
    #endregion
    #region OLEDb Getters 
    private async Task<List<DateTime>> GetDates()
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

      return dates;
    }
    private async Task<List<Customer>> GetCustomers()
    {
      var customers = new List<Customer>();

      using (OleDbConnection connection = new OleDbConnection(GetDataSet1DbConnectionString))
      {
        connection.Open();
        var getDates = new OleDbCommand(@"
                SELECT [Customer Name], Country, City, State, [Postal Code], Region
                FROM Sheet1", connection);

        var reader = await getDates.ExecuteReaderAsync();
        while (await reader.ReadAsync())
        {
          customers.Add(new Customer()
          {
            Name = reader.GetFieldValue<string>(0),
            Country = reader.GetFieldValue<string>(1),
            City = reader.GetFieldValue<string>(2),
            State = reader.GetFieldValue<string>(3),
            PostalCode = reader.GetValue(4).ToString(),
            Region = reader.GetFieldValue<string>(5),

          });
        }
      }

      return customers;
    }
    private async Task<List<Product>> GetProducts()
    {
      var products = new List<Product>();

      using (OleDbConnection connection = new OleDbConnection(GetDataSet1DbConnectionString))
      {
        connection.Open();
        var getDates = new OleDbCommand(@"
                SELECT Category, [Sub-Category], [Product Name]
                FROM Sheet1", connection);

        var reader = await getDates.ExecuteReaderAsync();
        while (await reader.ReadAsync())
        {
          products.Add(new Product()
          {
            Category = reader.GetFieldValue<string>(0),
            Subcategory = reader.GetFieldValue<string>(1),
            Name = reader.GetFieldValue<string>(2),
          });
        }
      }

      return products;
    }
    #endregion
    #region Single Record Inserts

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
    private void InsertProduct(SqlConnection connection, Product product)
    {
      var insertCommand = new SqlCommand(@"INSERT INTO Product (category, subcategory, name)
                                                        VALUES (@category, @subcategory, @name)", connection);

      SqlParameter[] parameterList =
      {
        new SqlParameter("@category", product.Category),
        new SqlParameter("@subcategory", product.Subcategory),
        new SqlParameter("@name", product.Name),

      };
      insertCommand.Parameters.AddRange(parameterList);

      using (SqlDataReader reader = insertCommand.ExecuteReader())
      {
        Console.WriteLine($"Records affected {reader.RecordsAffected}");
      }

    }
    private void InsertCustomer(SqlConnection connection, Customer customer)
    {
      var insertCommand = new SqlCommand(@"INSERT INTO Product (name, country, city, state, postalCode, region)
                                                        VALUES (@name, @country, @city, @state, @postalCode, @region)", connection);

      SqlParameter[] parameterList =
      {
        new SqlParameter("@name", customer.Name),
        new SqlParameter("@country", customer.Country),
        new SqlParameter("@city", customer.City),
        new SqlParameter("@state", customer.State),
        new SqlParameter("@region", customer.Region),

      };
      insertCommand.Parameters.AddRange(parameterList);

      using (SqlDataReader reader = insertCommand.ExecuteReader())
      {
        Console.WriteLine($"Records affected {reader.RecordsAffected}");
      }

    } 
    #endregion
  }
}
