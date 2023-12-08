using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GESTELEC.Models;

namespace GESTELEC.Controllers
{
    public class RegisterController : Controller
    {
        // GET: Register
        [HttpGet]
        public ActionResult Index()
        {
            if (Session["UserId"] != null)
            {
                // Session is active, redirect to another controller/action
                return RedirectToAction("Index", "Work");
            }
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(User user)
        {
            if (ModelState.IsValid)
            {
                // Save the user to the database using Entity Framework or any other data access method
                // Example using Entity Framework:
                using (var db = new GestelecContext())
                {
                    db.Users.Add(user);
                    db.SaveChanges();
                }
                return RedirectToAction("Index", "Login");
            }

            return View(user);
        }
    }
}
