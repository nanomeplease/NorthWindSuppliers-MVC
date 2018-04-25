
namespace SuppliersPL.Models
{
    public class ProductsPO
    {
        //Declaring all object properties for products.
        public int SupplierId;
        public int ProductId;
        public string ProductName;
        public string QuantityPerUnit;
        public int UnitsInStock;
        public int UnitsOnOrder;
        public int ReorderLevel;
        public decimal UnitPrice;
    }
}