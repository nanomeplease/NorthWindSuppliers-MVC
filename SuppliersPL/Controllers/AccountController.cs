namespace SuppliersPL.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using System.Web.Mvc;

    public class AccountController : Controller
    {
        // GET: Account
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(FormCollection form)
        {
            return View();
        }

        [HttpGet]
        public ActionResult Logout()
        {
            return RedirectToAction("Login", "Account");
        }
    }
}