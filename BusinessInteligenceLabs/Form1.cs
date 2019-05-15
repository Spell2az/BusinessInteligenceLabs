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
        private string DestinationDbConnectionString => Properties.Settings.Default.DestinationDatabaseConnectionString;
        private string Source1DbConnectionString => Properties.Settings.Default.Data_set_1ConnectionString;
        private string Source2DbConnectionString => Properties.Settings.Default.DataSet2ConnectionString;

        #endregion

        private void FillListBox<T>(ListBox listBox, IEnumerable<T> listItems)
        { 
            listBox.DataSource = null;
            listBox.Items.Clear();
            listBox.DataSource = listItems;
        }

        #region Form Constructor & Load handler

        //Leave this alone
        public Form1()
        {
            InitializeComponent();
        }

        //Leave this alone
        private void Form1_Load(object sender, EventArgs e)
        {

        }
        #endregion


        #region Event Handlers


        //This needs replacing with whatever button we decide to make
        private void FillAllFromSource(object sender, EventArgs e)
        {
            FillListBox(lstCustomersSource, GetCustomerFromSource());
            FillListBox(lstTimeSource, GetDatesFromSource());
            FillListBox(lstProductsSource, GetProductFromSource());
            InsertCustomerDimension();
            InsertTimeDimension();
            InsertProductDimension();
            BuildFactTable();
            //FillListBox(lstProductSource, GetProductFromSource());
        }
        private void GetDataFromSource_Click(object sender, EventArgs e)
        {

            GetDatesFromSource();
            GetProductFromSource();
            GetCustomerFromSource();
        }


        //This needs replacing with whatever button we decide to make
        private void GetFromDestinationButton_Click(object sender, EventArgs e)
        {

            FillListBox(lstCustomersDimension, GetAllCustomersFromDimension());
            FillListBox(lstTimeDimension, GetAllDatesFromDimension());
            FillListBox(lstProductsDimension, GetAllProductsFromDimension());
            FillListBox(lstFacts, GetFactTableFromDestination());
            //GetAllDatesFromDimension();
            //GetAllProductsFromDimension();
            //GetAllCustomersFromDimension();
        }

        #endregion


        #region Customer

        private List<Customer> GetCustomerFromSource()
        {
            //Create a list

            //Connect to the source

            //Get the data

            //Add data to list
            var customers = new List<Customer>();

            using (OleDbConnection connection = new OleDbConnection(Source1DbConnectionString))
            {
                connection.Open();
                var query = @"SELECT [Customer Name], Country, City, State, [Postal Code], Region, [Customer ID]  from Sheet1";
                var command = new OleDbCommand(query, connection);

                using (OleDbDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        customers.Add(new Customer(reader));
                    }
                }
            }
            return customers.Distinct().ToList();
        }

        private void InsertCustomerDimension()
        {

            var customersPresent = GetAllCustomersFromDimension();
            var customersFromSource = GetCustomerFromSource();
            var customersToInsert = customersFromSource.Except(customersPresent);

            using (SqlConnection connection = new SqlConnection(DestinationDbConnectionString))
            {
                connection.Open();

                foreach (var customer in customersToInsert)
                {
                    InsertCustomer(connection, customer);
                }
            }
        }


        private void InsertCustomer(SqlConnection connection, Customer customer)
        {
            var query = @"INSERT INTO Customer (name, country, city, state, postalCode, region, reference)
                          VALUES (@name, @country, @city, @state, @postalCode, @region, @reference)";

            var insertCommand = new SqlCommand(query, connection);

            SqlParameter[] parameterList =
            {
                new SqlParameter("@name", customer.Name),
                new SqlParameter("@country", customer.Country),
                new SqlParameter("@city", customer.City),
                new SqlParameter("@state", customer.State),
                new SqlParameter("@postalCode", customer.PostalCode),
                new SqlParameter("@region", customer.Region),
                new SqlParameter("@reference", customer.Reference),
            };
            insertCommand.Parameters.AddRange(parameterList);

            using (SqlDataReader reader = insertCommand.ExecuteReader())
            {
                Console.WriteLine($"Records affected {reader.RecordsAffected}");
            }
        }
        private List<Customer> GetAllCustomersFromDimension()
        {
            //Create new list to store the named results in.

            //Create the database string

            //Run the query

            //Bind the listbox to the list.
            var customers = new List<Customer>();

            using (SqlConnection connection = new SqlConnection(DestinationDbConnectionString))
            {
                connection.Open();
                var query = @"SELECT Id, name, country, city, state, postalCode, region, reference  from Customer";
                var command = new SqlCommand(query, connection);

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        customers.Add(new Customer(reader));
                    }
                }
            }
            return customers;
        }


        private int GetCustomerId(string reference)
        {
            using (SqlConnection connection = new SqlConnection(DestinationDbConnectionString))
            {
                connection.Open();

                var query = @"SELECT id FROM Customer WHERE reference = @reference";
                var command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@reference", reference);

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
        #endregion

        #region Date

        private void InsertTimeDimension()
        {

            var datesPresent = GetAllDatesFromDimension();
            var datesFromSource = GetDatesFromSource();
            var datesToInsert = datesFromSource.Except(datesPresent);

            using (SqlConnection connection = new SqlConnection(DestinationDbConnectionString))
            {
                connection.Open();

                foreach (var date in datesToInsert)
                {
                    InsertDate(connection, date);
                }
            }
            //Create a connection to the MDF file

            //Build the query

            //Insert the data

        }

        private void InsertDate(SqlConnection connection, DateEntity date)
        {
            var query =
                @"INSERT INTO Time (dayName, dayNumber, monthName, monthNumber, weekNumber, year, weekend, date, dayOfYear)
                                                        VALUES (@dayName, @dayNumber, @monthName, @monthNumber, @weekNumber, @year, @weekend, @date, @dayOfYear)";
            var insertCommand = new SqlCommand(query, connection);

            SqlParameter[] parameterList =
            {
                new SqlParameter("@dayName", date.DayName),
                new SqlParameter("@dayNumber", date.DayNumber),
                new SqlParameter("@monthName", date.MonthName),
                new SqlParameter("@monthNumber", date.MonthNumber),
                new SqlParameter("@weekNumber", date.WeekNumber),
                new SqlParameter("@year", date.Year ),
                new SqlParameter("@weekend", date.Weekend),
                new SqlParameter("@date", date.DateTime),
                new SqlParameter("@dayOfYear", date.DayOfYear)
            };
            insertCommand.Parameters.AddRange(parameterList);

            using (SqlDataReader reader = insertCommand.ExecuteReader())
            {
                Console.WriteLine($"Records affected {reader.RecordsAffected}");
            }

        }

        private List<DateEntity> GetAllDatesFromDimension()
        {
            //Create new list to store the named results in.

            //Create the database string

            //Run the query

            //Bind the listbox to the list.
            var dates = new List<DateEntity>();

            using (SqlConnection connection = new SqlConnection(DestinationDbConnectionString))
            {
                connection.Open();
                var query = @"SELECT id, date  from Time";
                var command = new SqlCommand(query, connection);

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        dates.Add(new DateEntity(reader, 1));
                    }
                }
            }
            return dates;
        }

        private int GetDateId(DateEntity date)
        {
            //Remove the timestamps

            //Split the clean date down and assign it to variables for later use.

            //Create a connection to the MDF file

            //Run the command & read the results

            //return the details

            using (SqlConnection connection = new SqlConnection(DestinationDbConnectionString))
            {
                connection.Open();

                var query = @"SELECT id FROM Time WHERE date = @date";
                var command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@date", date.DateTime);

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

        private List<DateEntity> GetDatesFromSource()
        {
            //Create a list

            //Connect to the source

            //Get the data

            //Add data to temp list

            //Create a new list for the formatted data.

            //Format the data and add to new list
            var dates = new List<DateEntity>();

            using (OleDbConnection connection = new OleDbConnection(Source1DbConnectionString))
            {
                connection.Open();
                var query = @"SELECT [Order Date], [Ship Date] from Sheet1";
                var command = new OleDbCommand(query, connection);

                using (OleDbDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        dates.Add(new DateEntity(reader.GetDateTime(0)));
                        dates.Add(new DateEntity(reader.GetDateTime(1)));
                    }
                }
            }
            return dates.Distinct().ToList();

        }

        #endregion

        #region Product

        private void InsertProductDimension()
        {
            //Create a connection to the MDF file

            //Build the query

            //Insert the data
            var productsPresent = GetAllProductsFromDimension();
            var productsFromSource = GetProductFromSource();
            var productsToInsert = productsFromSource.Except(productsPresent);

            using (SqlConnection connection = new SqlConnection(DestinationDbConnectionString))
            {
                connection.Open();

                foreach (var product in productsToInsert)
                {
                    InsertProduct(connection, product);
                }
            }
        }

        private List<Product> GetAllProductsFromDimension()
        {
            //Create new list to store the named results in.

            //Create the database string

            //Run the query

            //Bind the listbox to the list.
            var products = new List<Product>();

            using (SqlConnection connection = new SqlConnection(DestinationDbConnectionString))
            {
                connection.Open();
                var query = @"SELECT Id, category, subcategory, name, reference  from Product";
                var command = new SqlCommand(query, connection);

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        products.Add(new Product(reader));
                    }
                }
            }
            return products;
        }

        private int GetProductId(string reference)
        {
            //Remove the timestamps

            //Split the clean date down and assign it to variables for later use.

            //Create a connection to the MDF file

            //Run the command & read the results

            //return the details
            using (SqlConnection connection = new SqlConnection(DestinationDbConnectionString))
            {
                connection.Open();

                var query = @"SELECT id FROM Product WHERE reference = @reference";
                var command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@reference", reference);

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

        private void InsertProduct(SqlConnection connection, Product product)
        {
            var query = @"INSERT INTO Product (category, subcategory, name, reference)
                          VALUES (@category, @subcategory, @name, @reference)";

            var insertCommand = new SqlCommand(query, connection);

            SqlParameter[] parameterList =
            {
                new SqlParameter("@category", product.Category),
                new SqlParameter("@subcategory", product.Subcategory),
                new SqlParameter("@name", product.Name),
                new SqlParameter("@reference", product.Reference),
            };
            insertCommand.Parameters.AddRange(parameterList);

            using (SqlDataReader reader = insertCommand.ExecuteReader())
            {
                Console.WriteLine($"Records affected {reader.RecordsAffected}");
            }
        }
        private List<Product> GetProductFromSource()
        {
            //Create a list

            //Connect to the source

            //Get the data

            //Add data to list
            var products = new List<Product>();

            using (OleDbConnection connection = new OleDbConnection(Source1DbConnectionString))
            {
                connection.Open();
                var query = @"SELECT Category, [Sub-Category], [Product Name], [Product ID] from Sheet1";
                var command = new OleDbCommand(query, connection);

                using (OleDbDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        products.Add(new Product(reader));
                    }
                }
            }
            return products.Distinct().ToList();
        }
        #endregion

        #region Fact Table

        private void InsertIntoFactTable(Fact fact)
        {
            //Create a connection to the MDF file

            //Build the query

            //Insert the data
            using (SqlConnection connection = new SqlConnection(DestinationDbConnectionString))
            {
                connection.Open();

                var query = @"INSERT INTO FactTable (productId, timeId, customerId, value, discount, profit, quantity)
                              VALUES (@productId, @timeId, @customerId, @value, @discount, @profit, @quantity)";

                var insertCommand = new SqlCommand(query, connection);

                SqlParameter[] parameterList =
                {
                    new SqlParameter("@productId", fact.ProductId),
                    new SqlParameter("@timeId", fact.TimeId),
                    new SqlParameter("@customerId", fact.CustomerId),
                    new SqlParameter("@value", fact.Value),
                    new SqlParameter("@discount", fact.Discount),
                    new SqlParameter("@profit", fact.Profit),
                    new SqlParameter("@quantity", fact.Quantity),
                };
                insertCommand.Parameters.AddRange(parameterList);

                using (SqlDataReader reader = insertCommand.ExecuteReader())
                {
                    Console.WriteLine($"Records affected {reader.RecordsAffected}");
                }

            }
        }

        private List<Fact> GetFactTableFromDestination()
        {
            //Create new list to store the named results in.

            //Create the database string

            //Run the query

            //Bind the listbox to the list.
            var facts = new List<Fact>();

            using (SqlConnection connection = new SqlConnection(DestinationDbConnectionString))
            {
                connection.Open();
                var query = @"SELECT productId, timeId, customerId, value, discount, profit, quantity  from FactTable";
                var command = new SqlCommand(query, connection);

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        facts.Add(new Fact(reader));
                    }
                }
            }
            return facts;
        }

        private void BuildFactTable()
        {
            var newFacts = new List<Fact>();
            var destinationFacts = GetFactTableFromDestination();

            using (OleDbConnection connection = new OleDbConnection(Source1DbConnectionString))
            {
                connection.Open();
                var query = @"SELECT [Order Date], [Customer ID], [Product ID], [Product Name], Sales, Quantity, Discount, Profit FROM Sheet1";
                var command = new OleDbCommand(query, connection);

                var reader = command.ExecuteReader();

                while (reader.Read())
                {
                    var productId = GetProductId(reader.GetString(2));
                    var timeId = GetDateId(new DateEntity(reader.GetDateTime(0)));
                    var customerId = GetCustomerId(reader.GetString(1));
                    newFacts.Add(new Fact(reader, productId, timeId, customerId));
                }
            }

            var factsToInsert = newFacts.Except(destinationFacts);

            foreach (var fact in factsToInsert)
            {
                InsertIntoFactTable(fact);
            }
        }

        #endregion


    }
}
