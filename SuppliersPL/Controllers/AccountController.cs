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

        //Index
        public ActionResult Index()
        {
            return View();
        }

        #region Login
        //Login
        [HttpGet]
        public ActionResult Login()
        {

            return View();
        }

        [HttpPost]

        public ActionResult Login(UserPO user)
        {
            UserDO fromTable = dataAccess.ViewUserFromDb(user.Username);
            ActionResult oResponse = View();
            //If username is valid
            if (ModelState.IsValid && fromTable.UserId != 0 && fromTable.Password == user.Password)
            {
                //give session id and role
                Session.Add("UserId", fromTable.UserId);
                Session.Add("RoleId", fromTable.UserRole);
            }
            else
            {
                oResponse = View();
                //Incorect username or password
            }
            return oResponse;
        }
        #endregion

        [HttpGet]
        public ActionResult Logout()
        {
            return RedirectToAction("Login", "Account");
        }

        #region Register
        //Register new user
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
        #endregion
    }
}