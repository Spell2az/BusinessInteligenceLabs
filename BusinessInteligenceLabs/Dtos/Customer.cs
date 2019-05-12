using System;
using System.Data.OleDb;
using System.Data.SqlClient;

namespace BusinessInteligenceLabs.Dtos
{
    class Customer : IEquatable<Customer>
    {

        #region Properties
        public int Id { get; set; }
        public string Name { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string PostalCode { get; set; }
        public string Region { get; set; }
        public string Reference { get; set; }
        #endregion

        public Customer(SqlDataReader reader)
        {
            Id = (int)reader["Id"];
            Name = (string)reader["name"];
            Country = (string)reader["country"];
            City = (string)reader["city"];
            State = (string)reader["state"];
            PostalCode = (string)reader["postalCode"];
            Region = (string)reader["region"];
            Reference = (string)reader["reference"];
        }

        public override string ToString()
        {
            return $"{nameof(Id)}: {Id}, {nameof(Name)}: {Name}, {nameof(Country)}: {Country}, {nameof(City)}: {City}, {nameof(State)}: {State}, {nameof(PostalCode)}: {PostalCode}, {nameof(Region)}: {Region}, {nameof(Reference)}: {Reference}";
        }

        public Customer(OleDbDataReader reader)
        {

            Name = (string)reader["Customer Name"];
            Country = (string)reader["Country"];
            City = (string)reader["City"];
            State = (string)reader["State"];
            PostalCode = Convert.ToString(reader["Postal Code"]);
            Region = (string)reader["Region"];
            Reference = (string)reader["Customer ID"];
        }

        public bool Equals(Customer other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return string.Equals(Name, other.Name) && string.Equals(Country, other.Country) && string.Equals(City, other.City) && string.Equals(State, other.State) && string.Equals(PostalCode, other.PostalCode) && string.Equals(Region, other.Region) && string.Equals(Reference, other.Reference);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((Customer)obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = 0;
                hashCode = (hashCode * 397) ^ (Name != null ? Name.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (Country != null ? Country.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (City != null ? City.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (State != null ? State.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (PostalCode != null ? PostalCode.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (Region != null ? Region.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (Reference != null ? Reference.GetHashCode() : 0);
                return hashCode;
            }
        }
    }
}
