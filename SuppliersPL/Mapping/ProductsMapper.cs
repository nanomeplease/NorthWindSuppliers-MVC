using DataLayer.Models;
using SuppliersPL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProductsPL.Mapping
{
    public class ProductsMapper
    {
        /// <summary>
        /// Maps the Product list from the datalayer to the presentation layer
        /// </summary>
        /// <param name="from">Data Object from the DataLayer</param>
        /// <returns></returns>
        public ProductsPO MapDoToPO(ProductsDO from)
        {
            try
            {
                ProductsPO to = new ProductsPO();
                to.ProductId = from.ProductId;
                to.ProductName = from.ProductName;
                to.QuantityPerUnit = from.QuantityPerUnit;
                to.UnitPrice = from.UnitPrice;
                to.UnitsInStock = from.UnitsInStock;
                to.UnitsOnOrder = from.UnitsOnOrder;
                to.SupplierId = from.SupplierId;
                return to;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public List<ProductsPO> MapDoToPO(List<ProductsDO> from)
        {
            List<ProductsPO> to = new List<ProductsPO>();

            try
            {
                foreach (ProductsDO item in from)
                {
                    ProductsPO mappedItem = new ProductsPO();
                    mappedItem.SupplierId = item.SupplierId;
                    mappedItem.ProductId = item.ProductId;
                    mappedItem.ProductName = item.ProductName;
                    mappedItem.QuantityPerUnit = item.QuantityPerUnit;
                    mappedItem.ReorderLevel = item.ReorderLevel;
                    mappedItem.UnitPrice = item.UnitPrice;
                    mappedItem.UnitsInStock = item.UnitsInStock;
                    mappedItem.UnitsOnOrder = item.UnitsOnOrder;
                    to.Add(mappedItem);
                }
                return to;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}