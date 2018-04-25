
namespace DataLayer.Models
{
    public class ProductsDO
    {
        //Declaring all object properties for products.
        public int SupplierId { get; set; }
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public string QuantityPerUnit { get; set; }
        public int UnitsInStock { get; set; }
        public int UnitsOnOrder { get; set; }
        public int ReorderLevel { get; set; }
        public decimal UnitPrice { get; set; }
    }
}
