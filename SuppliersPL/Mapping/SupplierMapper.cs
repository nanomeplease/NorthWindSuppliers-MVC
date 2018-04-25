namespace SuppliersPL.Mapping
{
    #region Using Statements
    using DataLayer.Models;
    using SuppliersPL.Models;
    using System;
    using System.Collections.Generic;
    #endregion

    public class SupplierMapper
    {

        /// <summary>
        /// Maps the supplier list from the datalayer to the presentation layer
        /// </summary>
        /// <param name="from">Data Object from the DataLayer</param>
        /// <returns></returns>
        public static SupplierPO MapDoToPO(SupplierDO from)
        {
            SupplierPO to = new SupplierPO();
            try
            {
                to.SupplierId = from.SupplierId;
                to.ContactName = from.ContactName;
                to.ContactTitle = from.ContactTitle;
                to.PostalCode = from.PostalCode;
                to.Country = from.Country;
                to.PhoneNumber = from.PhoneNumber;

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
        public static List<SupplierPO> MapDoToPO(List<SupplierDO> from)
        {
            List<SupplierPO> to = new List<SupplierPO>();

            try
            {
                foreach (SupplierDO item in from)
                {
                    SupplierPO mappedItem = MapDoToPO(item);
                    to.Add(mappedItem);
                }

            }
            catch (Exception e)
            {
                throw e;
            }
            return to;
        }

        public static SupplierDO MapPoToDO(SupplierPO from)
        {
            SupplierDO to = new SupplierDO();
            try
            {
                to.SupplierId = from.SupplierId;
                to.ContactName = from.ContactName;
                to.ContactTitle = from.ContactTitle;
                to.PostalCode = from.PostalCode;
                to.Country = from.Country;
                to.PhoneNumber = from.PhoneNumber;
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
        public static List<SupplierDO> MapPoToDO(List<SupplierPO> from)
        {
            List<SupplierDO> to = new List<SupplierDO>();

            try
            {
                foreach (SupplierPO item in from)
                {
                    SupplierDO mappedItem = MapPoToDO(item);
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