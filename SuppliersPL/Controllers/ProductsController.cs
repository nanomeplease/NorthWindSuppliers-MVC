using DataLayer;
using DataLayer.Models;
using ProductsPL.Mapping;
using SuppliersPL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SuppliersPL.Controllers
{
    public class ProductsController : Controller
    {
        //Dependencies
        private ProductsDAO dataAccess;
        private ProductsDO productsDO;

        // GET: Products
        public ActionResult Index()
        {
            try
            {
                dataAccess = new ProductsDAO();
                ProductsMapper productsMapper = new ProductsMapper();

                //Display all supppliers to the user and provide actions to authenticated users.                
                List<ProductsDO> dataObjects = dataAccess.ViewProducts(productsDO);
                List<ProductsPO> mappedItems = productsMapper.MapDoToPO(dataObjects);

                return View(mappedItems);
            }
            catch (Exception e)
            {

                throw e;
            }
        }
    }
}