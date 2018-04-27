namespace SuppliersPL.Controllers
{
    using DataLayer;
    using DataLayer.Models;
    using SuppliersPL.Custom;
    using SuppliersPL.Mapping;
    using SuppliersPL.Models;
    using System;
    using System.Collections.Generic;
    using System.Web.Mvc;

    public class SupplierController : Controller
    {
        public SupplierController()
        {
            dataAccess = new SupplierDAO();
        }

        //Dependencies
        private SupplierDAO dataAccess;


        //Create New Supplier
        [HttpGet]
        [MyFilter("/Account/Login", "RoleId", 1, 2)]

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [MyFilter("/Account/Login", "RoleId", 1, 2)]

        public ActionResult Create(SupplierPO form)
        {
            ActionResult oResponse = RedirectToAction("Index");
            if (ModelState.IsValid)
            {
                try
                {
                    SupplierDO to = SupplierMapper.MapPoToDO(form);
                    dataAccess.CreateNewSuppliers(to);
                }
                catch (Exception ex)
                {

                }
            }
            else
            {
                oResponse = View();
            }
            return oResponse;
        }

        // View Suppliers
        [MyFilter("/Account/Login", "RoleId", 1, 2, 3)]
        public ActionResult Index()
        {
            List<SupplierPO> mappedItems = new List<SupplierPO>();
            try
            {
                //Display all supppliers to the user and provide actions to authenticated users.                
                List<SupplierDO> dataObjects = dataAccess.ViewAllSuppliers();
                mappedItems = SupplierMapper.MapDoToPO(dataObjects);
            }
            catch (Exception ex)
            {
            }
            return View(mappedItems);
        }


        //Update Suppliers
        [HttpGet]
        [MyFilter("/Suppliers", "RoleId", 1, 2)]
        public ActionResult Update(int supplierId)
        {
            SupplierPO display = new SupplierPO();
            if (ModelState.IsValid)
            {               
                try
                {
                    SupplierDO data = dataAccess.ViewSupplierById(supplierId);
                    display = SupplierMapper.MapDoToPO(data);

                }
                catch (Exception ex)
                {
                } 
            }
            return View(display);
        }

        [HttpPost]
        [MyFilter("/Suppliers", "RoleId", 1, 2)]
        public ActionResult Update(SupplierPO form)
        {
            ActionResult oResponse = RedirectToAction("Index");
            if (ModelState.IsValid || form.SupplierId !=0)
            {
                try
                {
                    SupplierDO to = SupplierMapper.MapPoToDO(form);
                    dataAccess.UpdateSuppliers(to);
                }
                catch (Exception ex)
                {

                }
            }
            else
            {
                oResponse = View();
            }
            return oResponse;
        }

        //Delete Supplier by ID
        [HttpGet]
        [MyFilter("/Suppliers", "RoleId", 1, 2)]
        public ActionResult Delete(int supplierId)
        {
            try
            {
                dataAccess.DeleteSuppliers(supplierId);
            }
            catch (Exception)
            {
                //Log all exceptions and bury them.
                //The user never recieves the exception.
            }

            return RedirectToAction("Index");
        }
    }
}