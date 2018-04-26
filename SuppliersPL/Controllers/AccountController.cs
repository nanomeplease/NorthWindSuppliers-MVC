namespace SuppliersPL.Controllers
{
    using DataLayer;
    using DataLayer.Models;
    using SuppliersPL.Mapping;
    using SuppliersPL.Models;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using System.Web.Mvc;

    public class AccountController : Controller
    {
        public AccountController()
        {
            dataAccess = new UserDAO();
        }

        //Dependencies
        private UserDAO dataAccess;

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

        [HttpGet]
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Register(UserPO user)
        {
            if (ModelState.IsValid)
            {
                UserDO to = UserMapper.MapPoToDO(user);
                dataAccess.CreateNewUser(to);
                
            }
            else
            {
                //Some message here.
            }
            return RedirectToAction("Index");
        }
    }
}