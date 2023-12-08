using GESTELEC.Interfaces;
using GESTELEC.Models;
using GESTELEC.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace GESTELEC.Controllers
{
    public class PyloneController : Controller
    {
        private IUnitOfWork<Pylone> unitOfWork;

        public PyloneController()
        {
            this.unitOfWork = new UnitOfWork<Pylone>();
        }

        // GET: Pylone
        public ActionResult Index()
        {
            if (Session["UserId"] == null)
            {
                // Session is active, redirect to another controller/action
                return RedirectToAction("Index", "Login");
            }
            var pylones = unitOfWork.Entity.GetAll();
            return View(pylones);
        }

        // GET: Pylone/Details/5
        public ActionResult Details(int? id)
        {
            if (Session["UserId"] == null)
            {
                // Session is active, redirect to another controller/action
                return RedirectToAction("Index", "Login");
            }
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Pylone pylone = unitOfWork.Entity.GetById(id.Value);
            if (pylone == null)
            {
                return HttpNotFound();
            }
            return View(pylone);
        }

        // GET: Pylone/Create
        public ActionResult Create()
        {

            if (Session["UserId"] == null)
            {
                // Session is active, redirect to another controller/action
                return RedirectToAction("Index", "Login");
            }
            return View();
        }

        // POST: Pylone/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Numero,LigneElectrique,Ville,Longitude,Latitude,EtatDegradation")] Pylone pylone)
        {
            if (ModelState.IsValid)
            {
                unitOfWork.Entity.Insert(pylone);
                unitOfWork.Save();
                return RedirectToAction("Index");
            }

            return View(pylone);
        }

        // GET: Pylone/Edit/5
        public ActionResult Edit(int? id)
        {
            if (Session["UserId"] == null)
            {
                // Session is active, redirect to another controller/action
                return RedirectToAction("Index", "Login");
            }
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Pylone pylone = unitOfWork.Entity.GetById(id.Value);
            if (pylone == null)
            {
                return HttpNotFound();
            }
            return View(pylone);
        }

        // POST: Pylone/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "PyloneId,Numero,LigneElectrique,Ville,Longitude,Latitude,EtatDegradation")] Pylone pylone)
        {
            if (ModelState.IsValid)
            {
                unitOfWork.Entity.Update(pylone);
                unitOfWork.Save();
                return RedirectToAction("Index");
            }
            return View(pylone);
        }

        // GET: Pylone/Delete/5
        public ActionResult Delete(int? id)
        {
            if (Session["UserId"] == null)
            {
                // Session is active, redirect to another controller/action
                return RedirectToAction("Index", "Login");
            }
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Pylone pylone = unitOfWork.Entity.GetById(id.Value);
            if (pylone == null)
            {
                return HttpNotFound();
            }
            return View(pylone);
        }

        // POST: Pylone/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Pylone pylone = unitOfWork.Entity.GetById(id);
            unitOfWork.Entity.Delete(id);
            unitOfWork.Save();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                unitOfWork.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
