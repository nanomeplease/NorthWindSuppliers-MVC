using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using DataLayer.Models;
using System.IO;
using System.Configuration;

namespace DataLayer
{
    public class SupplierDAO
    {
        //Limiting access to the SQL server.(private string)
        private static string connectionString = ConfigurationManager.ConnectionStrings["connectionString"].ConnectionString;


        /// <summary>
        /// This will CREATE a new supplier.
        /// </summary>
        /// <param name="contactName">The name of the person that owns the company.</param>
        /// <param name="contactTitle"></param>
        /// <param name="postalCode"></param>
        /// <param name="country"></param>
        /// <param name="phoneNumber"></param>
        public void CreateNewSuppliers(SupplierDO supplier)
        {
            try
            {
                //Creates a new connections.
                using (SqlConnection northWndConn = new SqlConnection(connectionString))
                {
                    //Creating a new SqlCommand to use a stored procedure.
                    SqlCommand enterCommand = new SqlCommand("CREATE_SUPPLIER", northWndConn);
                    enterCommand.CommandType = CommandType.StoredProcedure;

                    //Parameters that are being passed to the stored procedures.
                    enterCommand.Parameters.AddWithValue("@ContactName", supplier.ContactName);
                    enterCommand.Parameters.AddWithValue("@ContactTitle", supplier.ContactTitle);
                    enterCommand.Parameters.AddWithValue("@PostalCode", supplier.PostalCode);
                    enterCommand.Parameters.AddWithValue("@Country", supplier.Country);
                    enterCommand.Parameters.AddWithValue("@PhoneNumber", supplier.PhoneNumber);

                    //Opening connection.
                    northWndConn.Open();
                    //Execute Non Query command.
                    enterCommand.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                //Prints error to console.
                ErrorHandler(ex);
                throw ex;
            }
        }

        /// <summary>
        /// This will DISPLAY a new supplier.
        /// </summary>
        /// <returns></returns>
        public List<SupplierDO> ViewAllSuppliers()
        {
            try
            {
                List<SupplierDO> allSuppliers = new List<SupplierDO>();
                //Opening SQL connection.
                using (SqlConnection northWndConn = new SqlConnection(connectionString))
                {
                    //Creating a new SqlCommand to use a stored procedure.
                    SqlCommand enterCommand = new SqlCommand("DISPLAY_SUPPLIER", northWndConn);
                    enterCommand.CommandType = CommandType.StoredProcedure;
                    northWndConn.Open();

                    //Using SqlDataAdapter to get SQL table.
                    DataTable supplyInfo = new DataTable();
                    using (SqlDataAdapter supplierAdapter = new SqlDataAdapter(enterCommand))
                    {
                        supplierAdapter.Fill(supplyInfo);
                        supplierAdapter.Dispose();
                    }

                    //Putting datarow into a List of the supplier object.
                    foreach (DataRow row in supplyInfo.Rows)
                    {
                        SupplierDO mappedRow = MapAllSuppliers(row);
                        allSuppliers.Add(mappedRow);
                    }
                }
                //Returning an updated list of the supplier object.
                return allSuppliers;
            }
            catch (Exception ex)
            {
                //Prints error to console and logs.
                ErrorHandler(ex);
                throw ex;
            }


        }

        public SupplierDO ViewSupplierById(int supplierId)
        {
            SupplierDO singleSupplier = new SupplierDO();
            try
            {

                //Opening SQL connection.
                using (SqlConnection northWndConn = new SqlConnection(connectionString))
                {
                    //Creating a new SqlCommand to use a stored procedure.
                    SqlCommand enterCommand = new SqlCommand("DISPLAY_SUPPLIER_BY_ID", northWndConn);
                    enterCommand.CommandType = CommandType.StoredProcedure;
                    enterCommand.Parameters.AddWithValue("@SupplierID", supplierId);
                    northWndConn.Open();

                    //Using SqlDataAdapter to get SQL table.
                    DataTable supplyInfo = new DataTable();
                    using (SqlDataAdapter supplierAdapter = new SqlDataAdapter(enterCommand))
                    {
                        supplierAdapter.Fill(supplyInfo);
                        supplierAdapter.Dispose();
                    }

                    //Putting datarow into a List of the supplier object.
                    foreach (DataRow row in supplyInfo.Rows)
                    {
                        singleSupplier = MapAllSuppliers(row);
                    }
                }
                //Returning an updated list of the supplier object.

            }
            catch (Exception ex)
            {
                //Prints error to console and logs.
                ErrorHandler(ex);
                throw ex;
            }

            return singleSupplier;
        }

        /// <summary>
        /// This will UPDATE a new supplier.
        /// </summary>
        /// <param name="supplierId"></param>
        /// <param name="contactName"></param>
        /// <param name="contactTitle"></param>
        /// <param name="postalCode"></param>
        /// <param name="country"></param>
        /// <param name="phoneNumber"></param>
        public void UpdateSuppliers(SupplierDO supplier)
        {
            //Opening SQL connection.
            try
            {
                using (SqlConnection northWndConn = new SqlConnection(connectionString))
                {
                    //Creating a new SqlCommand to use a stored procedure.
                    SqlCommand enterCommand = new SqlCommand("UPDATE_SUPPLIER", northWndConn);

                    //Parameters that are being passed to the stored procedures.
                    enterCommand.CommandType = CommandType.StoredProcedure;
                    enterCommand.Parameters.AddWithValue("@ContactName", supplier.ContactName);
                    enterCommand.Parameters.AddWithValue("@ContactTitle", supplier.ContactTitle);
                    enterCommand.Parameters.AddWithValue("@PostalCode", supplier.PostalCode);
                    enterCommand.Parameters.AddWithValue("@Country", supplier.Country);
                    enterCommand.Parameters.AddWithValue("@PhoneNumber", supplier.PhoneNumber);
                    enterCommand.Parameters.AddWithValue("@SupplierID", supplier.SupplierId);

                    //Opening connection.
                    northWndConn.Open();
                    //Execute Non Query.
                    enterCommand.ExecuteNonQuery();
                }
            }
            catch (SqlException ex)
            {
                //Prints error to console and logs.
                ErrorHandler(ex);
                throw ex;
            }
        }

        /// <summary>
        /// This will DELETE a new supplier by thier Id
        /// </summary>
        /// <param name="supplierId">This is the supplierId</param>
        public void DeleteSuppliers(int contactId)
        {

            try
            {
                //Opening SQL connection to modify table using a stored procedure for deleting a row.
                using (SqlConnection northWndConn = new SqlConnection(connectionString))
                {
                    //Creating a new SqlCommand to use a stored procedure.
                    SqlCommand enterCommand = new SqlCommand("DELETE_SUPPLIER", northWndConn);

                    //Parameters that are being passed to the stored procedures.
                    enterCommand.CommandType = CommandType.StoredProcedure;
                    enterCommand.Parameters.AddWithValue("@SupplierID", contactId);

                    //Opening connection.
                    northWndConn.Open();
                    //Execute Non Query.
                    enterCommand.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                //Prints error to console and logs.
                ErrorHandler(ex);
                throw ex;
            }
        }


        /// <summary>
        /// Maps all suppliers from the datarow and returns the SupplierDO object.
        /// </summary>
        /// <param name="dataRow"></param>
        /// <returns></returns>
        public SupplierDO MapAllSuppliers(DataRow dataRow)
        {
            try
            {
                SupplierDO supplier = new SupplierDO();

                //If the supplier Id is not null then add values to the supplier object from the database.
                if (dataRow["SupplierID"] != DBNull.Value)
                {
                    supplier.SupplierId = (int)dataRow["SupplierID"];
                }
                supplier.ContactName = dataRow["ContactName"].ToString();
                supplier.ContactTitle = dataRow["ContactTitle"].ToString();
                supplier.PostalCode = dataRow["PostalCode"].ToString();
                supplier.Country = dataRow["Country"].ToString();
                supplier.PhoneNumber = dataRow["Phone"].ToString();

                //Returning the object with a row updated from SQL.
                return supplier;
            }
            catch (Exception ex)
            {
                //Prints error to console and logs.
                ErrorHandler(ex);
                throw ex;
            }
        }


        /// <summary>
        /// Error Method to write error to a file
        /// </summary>
        /// <param name="ex">The exeption that needs to be written to file.</param>
        public void ErrorHandler(Exception ex)
        {
            //using StreamWriter to write error message to a file.
            using (StreamWriter writer = new StreamWriter("ErrorLog.data", true))
            {
                writer.WriteLine(DateTime.Now.ToString() + ex);
                writer.Dispose();
                writer.Close();
            }
            //Prints the error message to the console.
            Console.WriteLine(ex.Message);
            Console.ReadLine();
        }



    }

}

