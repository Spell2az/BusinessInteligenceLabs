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
using BusinessInteligenceLabs.Dtos;

namespace BusinessInteligenceLabs
{
  public partial class Form1 : Form
  {

    #region Get Connection strings
    private string DestinationDbConnectionString =>
        Properties.Settings.Default.DestinationDatabaseConnectionString;

    private string Source1DbConnectionString =>
        Properties.Settings.Default.Data_set_1ConnectionString;

    #endregion
    
    public Form1()
    {
      InitializeComponent();
      
    }


    private async void btnGetDates_ClickAsync(object sender, EventArgs e)
    {
      var times = await GetTimes();
      lstDates.DataSource = null;
      lstDates.Items.Clear();

     lstDates.DataSource = times.ToList();
      Debug.WriteLine("Unique Dates in a list " + times.Distinct().Count());
      //InsertTimeDimensionRecords(times);
    }


    #region Insert range
    private void InsertTimeDimensionRecords(IEnumerable<TimeDto> times)
    {
      var connectionString = Properties.Settings.Default.DestinationDatabaseConnectionString;

      using (SqlConnection connection = new SqlConnection(connectionString))
      {
        connection.Open();

        foreach (var time in times)
        {
          if (!IsDateIsPresentInDatabase(connection, time))
          {
            InsertDate(connection, time);
          }
        }
      }
    }

    private void InsertProductDimensionRecords(IEnumerable<ProductDto> products)
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

    private void InsertCustomerDimensionRecords(IEnumerable<CustomerDto> customers)
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
    private bool IsProductPresentInDatabase(SqlConnection connection, ProductDto productDto)
    {
      bool isPresent;

      var command = new SqlCommand("SELECT id from ProductDto where productDto.Name = @name", connection);
      command.Parameters.Add(new SqlParameter("@name", productDto.Name));

      using (SqlDataReader reader = command.ExecuteReader())
      {
        isPresent = reader.HasRows;
      }

      return isPresent;
    }
    private bool IsCustomerPresentInDatabase(SqlConnection connection, CustomerDto customerDto)
    {
      bool isPresent;

      var command = new SqlCommand("SELECT id from CustomerDto where productDto.Name = @name", connection);
      command.Parameters.Add(new SqlParameter("@name", customerDto.Name));

      using (SqlDataReader reader = command.ExecuteReader())
      {
        isPresent = reader.HasRows;
      }

      return isPresent;
    }
    private bool IsDateIsPresentInDatabase(SqlConnection connection, TimeDto time)
    {
      bool isPresent;

      var command = new SqlCommand("SELECT id from Time where date = @date", connection);
      command.Parameters.Add(new SqlParameter("@date", time.Date));

      using (SqlDataReader reader = command.ExecuteReader())
      {
        isPresent = reader.HasRows;
      }

      return isPresent;
    } 
    #endregion

    #region OLEDb Getters 
    private async Task<List<TimeDto>> GetTimes()
    {
      var dates = new List<TimeDto>();
    
      using (OleDbConnection connection = new OleDbConnection(Source1DbConnectionString))
      {
        connection.Open();
        var getDates = new OleDbCommand(@"SELECT [Order Date], [Ship Date]
                                                FROM Sheet1", connection);

        var reader = await getDates.ExecuteReaderAsync();
        while (await reader.ReadAsync())
        {
          dates.Add(new TimeDto(reader.GetDateTime(0)));
          dates.Add(new TimeDto(reader.GetDateTime(1)));
        }
      }

      return dates;
    }
    private async Task<List<CustomerDto>> GetCustomers()
    {
      var customers = new List<CustomerDto>();

      using (OleDbConnection connection = new OleDbConnection(Source1DbConnectionString))
      {
        connection.Open();
        var getDates = new OleDbCommand(@"
                SELECT [CustomerDto Name], Country, City, State, [Postal Code], Region
                FROM Sheet1", connection);

        var reader = await getDates.ExecuteReaderAsync();
        while (await reader.ReadAsync())
        {
          customers.Add(new CustomerDto()
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
    private async Task<List<ProductDto>> GetProducts()
    {
      var products = new List<ProductDto>();

      using (OleDbConnection connection = new OleDbConnection(Source1DbConnectionString))
      {
        connection.Open();
        var getDates = new OleDbCommand(@"
                SELECT Category, [Sub-Category], [ProductDto Name]
                FROM Sheet1", connection);

        var reader = await getDates.ExecuteReaderAsync();
        while (await reader.ReadAsync())
        {
          products.Add(new ProductDto()
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

    private void InsertDate(SqlConnection connection, TimeDto time)
    {
      var insertCommand = new SqlCommand(@"INSERT INTO Time (dayName, dayNumber, monthName, monthNumber, weekNumber, year, weekend, date, dayOfYear)
                                                        VALUES (@dayName, @dayNumber, @monthName, @monthNumber, @weekNumber, @year, @weekend, @date, @dayOfYear)", connection);

      SqlParameter[] parameterList =
      {
                    new SqlParameter("@dayName", time.DayName),
                    new SqlParameter("@dayNumber", time.DayNumber),
                    new SqlParameter("@monthName", time.MonthName),
                    new SqlParameter("@monthNumber", time.MonthNumber),
                    new SqlParameter("@weekNumber", time.WeekNumber),
                    new SqlParameter("@year", time.Year ),
                    new SqlParameter("@weekend", time.Weekend),
                    new SqlParameter("@date", time.Date),
                    new SqlParameter("@dayOfYear", time.DayOfYear)
                };
      insertCommand.Parameters.AddRange(parameterList);

      using (SqlDataReader reader = insertCommand.ExecuteReader())
      {
        Console.WriteLine($"Records affected {reader.RecordsAffected}");
      }

    }
    private void InsertProduct(SqlConnection connection, ProductDto productDto)
    {
      var insertCommand = new SqlCommand(@"INSERT INTO ProductDto (category, subcategory, name)
                                                        VALUES (@category, @subcategory, @name)", connection);

      SqlParameter[] parameterList =
      {
        new SqlParameter("@category", productDto.Category),
        new SqlParameter("@subcategory", productDto.Subcategory),
        new SqlParameter("@name", productDto.Name),

      };
      insertCommand.Parameters.AddRange(parameterList);

      using (SqlDataReader reader = insertCommand.ExecuteReader())
      {
        Console.WriteLine($"Records affected {reader.RecordsAffected}");
      }

    }
    private void InsertCustomer(SqlConnection connection, CustomerDto customerDto)
    {
      var insertCommand = new SqlCommand(@"INSERT INTO ProductDto (name, country, city, state, postalCode, region)
                                                        VALUES (@name, @country, @city, @state, @postalCode, @region)", connection);

      SqlParameter[] parameterList =
      {
        new SqlParameter("@name", customerDto.Name),
        new SqlParameter("@country", customerDto.Country),
        new SqlParameter("@city", customerDto.City),
        new SqlParameter("@state", customerDto.State),
        new SqlParameter("@region", customerDto.Region),

      };
      insertCommand.Parameters.AddRange(parameterList);

      using (SqlDataReader reader = insertCommand.ExecuteReader())
      {
        Console.WriteLine($"Records affected {reader.RecordsAffected}");
      }

    } 
    #endregion

    private int GetTimeId(TimeDto time)
    {
      using (SqlConnection connection = new SqlConnection(DestinationDbConnectionString))
      {
        connection.Open();

        var query = @"SELECT id FROM Time WHERE date = @date";
        var command = new SqlCommand(query, connection);
        command.Parameters.AddWithValue("@date", time.Date);

        using (SqlDataReader reader = command.ExecuteReader(CommandBehavior.SingleRow))
        {
          if (reader.HasRows)
          {
            while (reader.Read())
            {
              return Convert.ToInt32(reader["id"]);
            }
            
          }
        }
      }
      return -1;
    }

    

    private IEnumerable<TimeDto> GetFromDimensionTime()
    {
      var result = new List<TimeDto>();
      using (SqlConnection connection = new SqlConnection(DestinationDbConnectionString))
      {
        connection.Open();
        var query = @"SELECT id, date  from Time";
        var command = new SqlCommand(query, connection);

        using (SqlDataReader reader = command.ExecuteReader())
        {
          while (reader.Read())
          {
            result.Add(new TimeDto(reader, 1));
          }
        }
      }
      return result;
    }
    private void btnGetTimeFromDestination_Click(object sender, EventArgs e)
    {
      var times = GetFromDimensionTime();
      lstDestinationTimes.DataSource = times;

      Debug.WriteLine("Id is " +GetTimeId(new TimeDto(new DateTime(2014, 1, 3))));
    }
  }
}
