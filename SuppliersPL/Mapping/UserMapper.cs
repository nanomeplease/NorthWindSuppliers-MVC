namespace SuppliersPL.Mapping
{
    using DataLayer.Models;
    using SuppliersPL.Models;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;

    public class UserMapper
    {
        /// <summary>
        /// Maps the User list from the datalayer to the presentation layer
        /// </summary>
        /// <param name="from">Data Object from the DataLayer</param>
        /// <returns></returns>
        public static UserPO MapDoToPO(UserDO from)
        {
            UserPO to = new UserPO();
            try
            {
                to.UserId = from.UserId;
                to.Username = from.Username;
                to.Password = from.Password;
                to.FirstName = from.FirstName;
                to.LastName = from.LastName;
                to.Email = from.Email;
                to.UserRole = from.UserRole;

            }
            catch (Exception e)
            {
                throw e;
            }
            return to;
        }

        /// <summary>
        /// Mapping a List from the  Data Layer to the Presentation layer.
        /// </summary>
        /// <param name="from">List from the Data Layer</param>
        /// <returns></returns>
        public static List<UserPO> MapDoToPO(List<UserDO> from)
        {
            List<UserPO> to = new List<UserPO>();

            try
            {
                foreach (UserDO item in from)
                {
                    UserPO mappedItem = MapDoToPO(item);
                    to.Add(mappedItem);
                }

            }
            catch (Exception e)
            {
                throw e;
            }
            return to;
        }

        public static UserDO MapPoToDO(UserPO from)
        {
            UserDO to = new UserDO();
            try
            {
                to.UserId = from.UserId;
                to.Username = from.Username;
                to.Password = from.Password;
                to.FirstName = from.FirstName;
                to.LastName = from.LastName;
                to.Email = from.Email;
                to.UserRole = from.UserRole;
            }
            catch (Exception e)
            {
                throw e;
            }
            return to;
        }

        /// <summary>
        /// Mapping a List from the Presentation Layer
        /// </summary>
        /// <param name="from">List from the Presentation Layer</param>
        /// <returns></returns>
        public static List<UserDO> MapPoToDO(List<UserPO> from)
        {
            List<UserDO> to = new List<UserDO>();

            try
            {
                foreach (UserPO item in from)
                {
                    UserDO mappedItem = MapPoToDO(item);
                    to.Add(mappedItem);
                }

            }
            catch (Exception e)
            {
                throw e;
            }
            return to;
        }
    }
}