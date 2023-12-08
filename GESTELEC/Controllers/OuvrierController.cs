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
    public class OuvrierController : Controller
    {
        private IUnitOfWork<Ouvrier> unitOfWork;

        public OuvrierController()
        {
            this.unitOfWork = new UnitOfWork<Ouvrier>();
        }

        // GET: Ouvrier
        public ActionResult Index()
        {
            if (Session["UserId"] == null)
            {
                // Session is active, redirect to another controller/action
                return RedirectToAction("Index", "Login");
            }
            var ouvriers = unitOfWork.Entity.GetAll();
            return View(ouvriers);
        }

        // GET: Ouvrier/Details/5
        public ActionResult Details(string id)
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
            Ouvrier ouvrier = unitOfWork.Entity.GetById(id);
            if (ouvrier == null)
            {
                return HttpNotFound();
            }
            return View(ouvrier);
        }

        // GET: Ouvrier/Create
        public ActionResult Create()
        {

            if (Session["UserId"] == null)
            {
                // Session is active, redirect to another controller/action
                return RedirectToAction("Index", "Login");
            }

            return View();
        }

        // POST: Ouvrier/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "CIN,NomComplet,Ville,Telephone,DateNaissance,DateDebutActivite,Poste")] Ouvrier ouvrier)
        {
            if (ModelState.IsValid)
            {
                unitOfWork.Entity.Insert(ouvrier);
                unitOfWork.Save();
                return RedirectToAction("Index");
            }

            return View(ouvrier);
        }

        // GET: Ouvrier/Edit/5
        public ActionResult Edit(string id)
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
            Ouvrier ouvrier = unitOfWork.Entity.GetById(id);
            if (ouvrier == null)
            {
                return HttpNotFound();
            }
            return View(ouvrier);
        }

        // POST: Ouvrier/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "CIN,NomComplet,Ville,Telephone,DateNaissance,DateDebutActivite,Poste")] Ouvrier ouvrier)
        {
            if (ModelState.IsValid)
            {
                unitOfWork.Entity.Update(ouvrier);
                unitOfWork.Save();
                return RedirectToAction("Index");
            }
            return View(ouvrier);
        }

        // GET: Ouvrier/Delete/5
        public ActionResult Delete(string id)
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
            Ouvrier ouvrier = unitOfWork.Entity.GetById(id);
            if (ouvrier == null)
            {
                return HttpNotFound();
            }
            return View(ouvrier);
        }

        // POST: Ouvrier/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            Ouvrier ouvrier = unitOfWork.Entity.GetById(id);
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
