using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GESTELEC.Controllers
{
    public class LogoutController : Controller
    {
        // GET: Lougout
        public ActionResult Index()
        {
            Session.Clear();
            return RedirectToAction("Index", "Home");
        }
    }
}