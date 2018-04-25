using DataLayer;
using DataLayer.Models;
using SuppliersPL.Mapping;
using SuppliersPL.Models;
using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace SuppliersPL.Controllers
{
    public class SupplierController : Controller
    {
        public SupplierController()
        {
            dataAccess = new SupplierDAO();
        }

        //Dependencies
        private SupplierDAO dataAccess;


        // GET: Suppliers
        public ActionResult Index()
        {
            try
            {              
                //Display all supppliers to the user and provide actions to authenticated users.                
                List<SupplierDO> dataObjects = dataAccess.ViewAllSuppliers();
                List<SupplierPO> mappedItems = SupplierMapper.MapDoToPO(dataObjects);

                return View(mappedItems);
            }
            catch (Exception e)
            {

                throw e;
            }
        }



        [HttpGet]
        public ActionResult Update(int supplierId)
        {
            SupplierPO display = new SupplierPO();
            try
            {
                SupplierDO data = dataAccess.ViewSupplierById(supplierId);
                //Attempt to go to the database and pull a dataobject for this supplier
                display= SupplierMapper.MapDoToPO(data);                
                //Map that dataObject to a displayObject and send it to the user.
            }
            catch (Exception ex)
            {


            }
            return View(display);
        }

        [HttpPost]
        public ActionResult Update(SupplierPO form)
        {
            //Check model state for errors.
            //Attempt to post object to database
            //Redirect the user back to the index view
            //No
            //Give the view back.
            return View();
        }


        [HttpGet]
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