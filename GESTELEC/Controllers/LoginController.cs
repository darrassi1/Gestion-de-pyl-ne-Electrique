using GESTELEC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GESTELEC.Controllers
{
    public class LoginController : Controller
    {
        private readonly GestelecContext _context;

        public LoginController()
        {
            _context = new GestelecContext();
        }

        // GET: Login
        public ActionResult Index()
        {

            if (Session["UserId"] != null)
            {
                // Session is active, redirect to another controller/action
                return RedirectToAction("Index", "Work");
            }
            return View();
        }

        // POST: Login
        [HttpPost]
        public ActionResult Index(string username, string password)
        {
            // Check if the username and password are valid
            var user = _context.Users.FirstOrDefault(u => u.Username == username && u.Password == password);
            if (user != null)
            {
                // Authentication successful, create a session or set authentication cookie
                Session["UserId"] = user.Id;
                return RedirectToAction("Index", "Work");
            }

            // Invalid login, show error message
            ModelState.AddModelError("", "Invalid username or password");
            return View();
        }
    }

}
