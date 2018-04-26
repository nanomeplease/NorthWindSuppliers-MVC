namespace DataLayer
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;
    using DataLayer.Models;
    using System.IO;
    using System.Configuration;

    public class UserDAO
    {

        private static string connectionString = ConfigurationManager.ConnectionStrings["connectionString"].ConnectionString;

        public void CreateNewUser(UserDO user)
        {
            try
            {
                //Creates a new connections.
                using (SqlConnection northWndConn = new SqlConnection(connectionString))
                {
                    //Creating a new SqlCommand to use a stored procedure.
                    SqlCommand enterCommand = new SqlCommand("CREATE_USER", northWndConn);
                    enterCommand.CommandType = CommandType.StoredProcedure;

                    //Parameters that are being passed to the stored procedures.
                    enterCommand.Parameters.AddWithValue("@Username", user.Username);
                    enterCommand.Parameters.AddWithValue("@Password", user.Password);
                    enterCommand.Parameters.AddWithValue("@FirstName", user.FirstName);
                    enterCommand.Parameters.AddWithValue("@LastName", user.LastName);
                    enterCommand.Parameters.AddWithValue("@Email", user.Email);

                    //Opening connection.
                    northWndConn.Open();
                    //Execute Non Query command.
                    enterCommand.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                //Prints error to console.
                //ErrorHandler(ex);
                throw ex;
            }
        }
        
        public List<UserDO> ViewAllUsers()
        {
            List<UserDO> user = new List<UserDO>();
            using (SqlConnection northWndConn = new SqlConnection(connectionString))
            {
                //Creating a new SqlCommand to use a stored procedure.
                SqlCommand enterCommand = new SqlCommand("READ_USERS", northWndConn);
                enterCommand.CommandType = CommandType.StoredProcedure;
                northWndConn.Open();

                //Using SqlDataAdapter to get SQL table.
                DataTable userInfo = new DataTable();
                using (SqlDataAdapter userAdapter = new SqlDataAdapter(enterCommand))
                {
                    userAdapter.Fill(userInfo);
                    userAdapter.Dispose();
                }

                //Putting datarow into a List of the users object.
                foreach (DataRow row in userInfo.Rows)
                {
                    UserDO mappedRow = MapAllUsers(row);
                    user.Add(mappedRow);
                }
                return user;
            }

        }

        public UserDO MapAllUsers(DataRow dataRow)
        {
            try
            {
                UserDO user = new UserDO();

                //If the user Id is not null then add values to the user object from the database.
                if (dataRow["UserID"] != DBNull.Value)
                {
                    user.UserId = (int)dataRow["UserID"];
                }
                user.Username = dataRow["Username"].ToString();
                user.Password = dataRow["Password"].ToString();
                user.FirstName = dataRow["FirstName"].ToString();
                user.LastName = dataRow["LastName"].ToString();
                user.Email = dataRow["Email"].ToString();
                user.UserRole = (int)dataRow["UserRole"];

                //Returning the object with a row updated from SQL.
                return user;
            }
            catch (Exception ex)
            {
                //Prints error to console and logs.
                //ErrorHandler(ex);
                throw ex;
            }
        }

    }
}
