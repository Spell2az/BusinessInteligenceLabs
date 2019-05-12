using System;
using System.Data.OleDb;
using System.Data.SqlClient;

namespace BusinessInteligenceLabs.Dtos
{
  public class Product: IEquatable<Product>
  {
    public int Id { get; set; }
    public string Category { get; set; }
    public string Subcategory { get; set; }
    public string Name { get; set; }
    public string Reference { get; set; }

      public Product(SqlDataReader reader)
      {
          Id = (int) reader["Id"];
          Category = (string)reader["category"];
          Subcategory = (string)reader["subcategory"];
          Name = (string)reader["name"];
          Reference = (string)reader["reference"];
        }

      public override string ToString()
      {
          return $"{nameof(Id)}: {Id}, {nameof(Category)}: {Category}, {nameof(Subcategory)}: {Subcategory}, {nameof(Name)}: {Name}, {nameof(Reference)}: {Reference}";
      }

      public Product(OleDbDataReader reader)
        {
            Category = (string)reader["Category"]; ;
            Subcategory = (string)reader["Sub-Category"]; ;
            Name = (string)reader["Product Name"];
            Reference = (string)reader["Product ID"];
        }

      public bool Equals(Product other)
      {
          if (ReferenceEquals(null, other)) return false;
          if (ReferenceEquals(this, other)) return true;
          return string.Equals(Category, other.Category) && string.Equals(Subcategory, other.Subcategory) && string.Equals(Name, other.Name) && string.Equals(Reference, other.Reference);
      }

      public override bool Equals(object obj)
      {
          if (ReferenceEquals(null, obj)) return false;
          if (ReferenceEquals(this, obj)) return true;
          if (obj.GetType() != this.GetType()) return false;
          return Equals((Product) obj);
      }

      public override int GetHashCode()
      {
          unchecked
          {
              var hashCode = (Category != null ? Category.GetHashCode() : 0);
              hashCode = (hashCode * 397) ^ (Subcategory != null ? Subcategory.GetHashCode() : 0);
              hashCode = (hashCode * 397) ^ (Name != null ? Name.GetHashCode() : 0);
              hashCode = (hashCode * 397) ^ (Reference != null ? Reference.GetHashCode() : 0);
              return hashCode;
          }
      }
  }
}