using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using DataLayer.Models;
using System.IO;
using System.Configuration;

namespace DataLayer
{
    public class ProductsDAO
    {

        //Limiting access to the SQL server.(private string)
        private static string connectionString = ConfigurationManager.ConnectionStrings["connectionString"].ConnectionString;

        /// <summary>
        /// View Products from selected supplier.
        /// </summary>
        public List<ProductsDO> ViewProducts(ProductsDO products)
        {
            string currentMethod = "ViewProducts";
            try
            {
                //Instatiating
                List<ProductsDO> allProducts = new List<ProductsDO>();

                //Opening SQL connection.
                using (SqlConnection northWndConn = new SqlConnection(connectionString))
                {
                    //Creating a new SqlCommand to use a stored procedure.
                    SqlCommand enterCommand = new SqlCommand("DISPLAY_PRODUCTS", northWndConn);
                    enterCommand.CommandType = CommandType.StoredProcedure;

                    enterCommand.Parameters.AddWithValue("@SupplierId", products.SupplierId);
                    northWndConn.Open();

                    //Using SqlDataAdapter to get SQL table.
                    DataTable ProductInfo = new DataTable();
                    using (SqlDataAdapter productAdapter = new SqlDataAdapter(enterCommand))
                    {
                        productAdapter.Fill(ProductInfo);
                        productAdapter.Dispose();
                    }

                    //Putting datarow into a List of the products object.
                    foreach (DataRow row in ProductInfo.Rows)
                    {
                        ProductsDO mappedRow = MapAllProducts(row);
                        allProducts.Add(mappedRow);
                    }
                }
                //Returning an updated list of the products object.
                return allProducts;
            }
            catch (Exception ex)
            {
                //Prints error to console and logs.
                //ProductsErrorHandler(ex, currentClass, currentMethod, ex.StackTrace);
                throw ex;
            }
        }

        /// <summary>
        /// Update Products based on selected supplier.
        /// </summary>
        public void UpdateProducts(ProductsDO products)
        {
            string currentMethod = "UpdateProducts";
            //Opening SQL connection.
            try
            {
                using (SqlConnection northWndConn = new SqlConnection(connectionString))
                {
                    //Creating a new SqlCommand to use a stored procedure.
                    SqlCommand enterCommand = new SqlCommand("UPDATE_PRODUCTS", northWndConn);

                    //Parameters that are being passed to the stored procedures.
                    enterCommand.CommandType = CommandType.StoredProcedure;
                    enterCommand.Parameters.AddWithValue("@ProductId", products.ProductId);
                    enterCommand.Parameters.AddWithValue("@ProductName", products.ProductName);
                    enterCommand.Parameters.AddWithValue("@QuantityPerUnit", products.QuantityPerUnit);
                    enterCommand.Parameters.AddWithValue("@UnitPrice", products.UnitPrice);
                    enterCommand.Parameters.AddWithValue("@UnitsInStock", products.UnitsInStock);
                    enterCommand.Parameters.AddWithValue("@UnitsOnOrder", products.UnitsOnOrder);
                    enterCommand.Parameters.AddWithValue("@ReorderLevel", products.ReorderLevel);

                    //Opening connection.
                    northWndConn.Open();
                    //Execute Non Query.
                    enterCommand.ExecuteNonQuery();
                }
            }
            catch (SqlException ex)
            {
                //Prints error to console and logs.
                //ProductsErrorHandler(ex, currentClass, currentMethod, ex.StackTrace);
                throw ex;
            }
        }

        /// <summary>
        /// Maps all products from the datarow and returns the ProductsDO object.
        /// </summary>
        /// <param name="dataRow"></param>
        /// <returns></returns>
        public ProductsDO MapAllProducts(DataRow dataRow)
        {
            string currentMethod = "MapAllProducts";
            try
            {
                ProductsDO products = new ProductsDO();

                //If the Product Id is not null then add values to the product object from the database.
                if (dataRow["ProductID"] != DBNull.Value)
                {
                    products.ProductId = (int)dataRow["ProductID"];
                }
                products.ProductName = dataRow["ProductName"].ToString();
                products.QuantityPerUnit = dataRow["QuantityPerUnit"].ToString();
                products.UnitPrice = (decimal)dataRow["UnitPrice"];
                products.UnitsInStock = (Int16)dataRow["UnitsInStock"];
                products.UnitsOnOrder = (Int16)dataRow["UnitsOnOrder"];
                products.ReorderLevel = (Int16)dataRow["ReorderLevel"];

                //Returning the object with a row updated from SQL.
                return products;
            }
            catch (Exception ex)
            {
                //Prints error to console and logs.
                //ProductsErrorHandler(ex, currentClass, currentMethod, ex.StackTrace);
                throw ex;
            }
        }

        /// <summary>
        /// Error Method to write error to a file
        /// </summary>
        /// <param name="ex">The exeption that needs to be written to file.</param>
        public void ProductsErrorHandler(Exception error, string currentClass, string currentMethod, string stackTrace = null)
        {
            if (error.Data["Logged"] == null)
            {
                error.Data["Logged"] = true;

                //using StreamWriter to write error message to a file.
                using (StreamWriter logWriter = new StreamWriter("ErrorLog.data", true))
                {
                    logWriter.WriteLine(new string('-', 120));
                    logWriter.WriteLine($"{DateTime.Now.ToString()} - {currentClass} - {currentMethod}");
                    logWriter.WriteLine(error);
                    if (!string.IsNullOrWhiteSpace(stackTrace))
                    {
                        logWriter.WriteLine(stackTrace);
                    }
                    logWriter.Dispose();
                    logWriter.Close();
                }
            }
        }
    }
}
