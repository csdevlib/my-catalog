using BeyondNet.Cqrs;
using BeyondNet.Ddd;
using BeyondNet.Ddd.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace MyPlanner.Catalog.Api.Models
{
    public class ProductProps : IProps
    {
        public const string TenantId = "TenantId";
        public const string Name = "Name";
        public const string Category = "Category";
        public const string Description = "Description";
        public const string ImageFile = "ImageFile";
        public const string Currency = "Currency";
        public const string Price = "Price";
        public const string StockQuantity = "StockQuantity";
        public const string DateAdded = "DateAdded";
        public const string Status = "Status";

        public object Clone()
        {
            return this.MemberwiseClone();
        }
    }
    public class Product : AggregateRoot
    {
        public string Id { get; set; } = default!;
        public string TenantId { get; set; } = default!;
        public string Name { get; set; } = default!;
        public int Category { get; set; } = default!;
        public string Description { get; set; } = default!;
        public int Currency { get; set; } = default!;
        public decimal Price { get; set; } = default!;
        public int StockQuantity { get; set; }
        public string ImageFile { get; set; } = default!;
        public DateTime DateAdded { get; set; } = DateTime.UtcNow;
        public int Status { get; set; } = default!;

        public Product()
        {
            Id = Guid.NewGuid().ToString();
        }

        public static Product Create(ProductProps props)
        {
            var product = Product.Create(props);

            return product;
        }
    }

    public class eProductCategory : Enumeration
    {
        public static eProductCategory Software = new eProductCategory(1, "Software");
        public static eProductCategory Hardware = new eProductCategory(2, "Hardware");
        public static eProductCategory Services = new eProductCategory(3, "Services");
        public static eProductCategory Food = new eProductCategory(4, "Food");
        public static eProductCategory Electronics = new eProductCategory(5, "Electronics");
        public static eProductCategory Clothing = new eProductCategory(6, "Clothing");
        public static eProductCategory Books = new eProductCategory(7, "Books");
        public static eProductCategory Furniture = new eProductCategory(8, "Furniture");
        public static eProductCategory Other = new eProductCategory(9, "Other");

        public eProductCategory(int id, string name) : base(id, name)
        {
        }
    }

    public class eCurrency : Enumeration
    {
        public static eCurrency USD = new eCurrency(1, "USD");
        public static eCurrency EUR = new eCurrency(2, "EUR");
        public static eCurrency GBP = new eCurrency(3, "GBP");
        public static eCurrency JPY = new eCurrency(4, "JPY");

        public eCurrency(int id, string name) : base(id, name)
        {
        }
    }
}
