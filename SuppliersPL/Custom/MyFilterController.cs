namespace SuppliersPL.Custom
{
    using System;
    using System.Linq;
    using System.Web;
    using System.Web.Mvc;

    public class MyFilter : ActionFilterAttribute
    {
        private readonly string _Key;
        private readonly string _redirectRoute;
        private readonly object[] _arguments;

        public MyFilter(string redirectRoute, string key, params object[] arguments)
        {
            _Key = key;
            _redirectRoute = redirectRoute;
            _arguments = arguments;
        }
        //Before method is ran
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            HttpSessionStateBase session = filterContext.HttpContext.Session;

            try
            {
                
                if (session[_Key] == null || (session[_Key] != null && !_arguments.Contains(session[_Key])))
                {
                    filterContext.Result = new RedirectResult(_redirectRoute, false);
                }
            }
            catch (Exception ex)
            {                
            }

            base.OnActionExecuting(filterContext);
        }

        //After the method is ran
        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            base.OnActionExecuted(filterContext);
        }


        //After the method has ran but before the result has started.
        public override void OnResultExecuting(ResultExecutingContext filterContext)
        {
            base.OnResultExecuting(filterContext);
        }
        //After the result has been decided
        public override void OnResultExecuted(ResultExecutedContext filterContext)
        {
            base.OnResultExecuted(filterContext);
        }


    }
}