namespace SuppliersPL.Controllers
{
    using DataLayer;
    using DataLayer.Models;
    using SuppliersPL.Mapping;
    using SuppliersPL.Models;
    using SuppliersPL.Custom;
    using System.Web.Mvc;
    using System;

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
            ActionResult oResponse = RedirectToAction("Index", "Home");
            //If username is valid
            if (ModelState.IsValid && fromTable.UserId != 0 && fromTable.Password == user.Password)
            {
                try
                {
                    //give session id and role
                    Session["UserId"] = fromTable.UserId;
                    Session["Username"] = fromTable.Username;
                    Session["RoleId"] = fromTable.UserRole;
                }
                catch (Exception ex)
                {
                }
            }
            else
            {
                //If to check if it was modelstate.
                oResponse = View();
                //Incorect username or password
            }
            return oResponse;
        }
        #endregion

        public ActionResult Logout()
        {
            Session.Abandon(); // it will clear the session at the end of request
            return RedirectToAction("Index", "Home");
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
                try
                {
                    UserDO to = UserMapper.MapPoToDO(user);
                    dataAccess.CreateNewUser(to);
                }
                catch (Exception ex) 
                {
                    
                }

            }
            else
            {
                //Some message here.
            }
            return RedirectToAction("Index" , "Home" );
        }
        #endregion
    }
}