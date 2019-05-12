using System;
using System.Data.OleDb;
using System.Data.SqlClient;

namespace BusinessInteligenceLabs.Dtos
{
    public class Fact : IEquatable<Fact>
    {
        public Fact(SqlDataReader reader)
        {
            ProductId = reader.GetInt32(0);
            TimeId = reader.GetInt32(1);
            CustomerId = reader.GetInt32(2);
            Value = reader.GetDecimal(3);
            Discount = reader.GetDouble(4);
            Profit = reader.GetDecimal(5);
            Quantity = reader.GetInt32(6);
        }

        public Fact(OleDbDataReader reader, int productId, int timeId, int customerId)
        {
            ProductId = productId;
            TimeId = timeId;
            CustomerId = customerId;
            Value = Convert.ToDecimal(reader["Sales"]);
            Discount = Convert.ToDouble(reader["Discount"]);
            Profit = Convert.ToDecimal(reader["Profit"]);
            Quantity = Convert.ToInt32(reader["quantity"]);
        }

        public int ProductId { get; set; }
        public int TimeId { get; set; }
        public int CustomerId { get; set; }
        public decimal Value { get; set; }
        public double Discount { get; set; }

        public override string ToString()
        {
            return $"{nameof(ProductId)}: {ProductId}, {nameof(TimeId)}: {TimeId}, {nameof(CustomerId)}: {CustomerId}, {nameof(Value)}: {Value}, {nameof(Discount)}: {Discount}, {nameof(Profit)}: {Profit}, {nameof(Quantity)}: {Quantity}";
        }

        public decimal Profit { get; set; }
        public int Quantity { get; set; }

        public bool Equals(Fact other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return ProductId == other.ProductId && TimeId == other.TimeId && CustomerId == other.CustomerId && Value == other.Value && Discount.Equals(other.Discount) && Profit == other.Profit && Quantity == other.Quantity;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((Fact)obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = ProductId;
                hashCode = (hashCode * 397) ^ TimeId;
                hashCode = (hashCode * 397) ^ CustomerId;
                hashCode = (hashCode * 397) ^ Value.GetHashCode();
                hashCode = (hashCode * 397) ^ Discount.GetHashCode();
                hashCode = (hashCode * 397) ^ Profit.GetHashCode();
                hashCode = (hashCode * 397) ^ Quantity;
                return hashCode;
            }
        }
    }
}