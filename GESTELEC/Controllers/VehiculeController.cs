using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using GESTELEC.Interfaces;
using GESTELEC.Models;
using GESTELEC.UnitOfWork;

namespace GESTELEC.Controllers
{
    public class VehiculeController : Controller
    {
        private readonly IUnitOfWork<Vehicule> unitOfWork;

        public VehiculeController()
        {
            unitOfWork = new UnitOfWork<Vehicule>();
        }

        // GET: Vehicule
        public ActionResult Index()
        {
            if (Session["UserId"] == null)
            {
                // Session is active, redirect to another controller/action
                return RedirectToAction("Index", "Login");
            }
            var vehicules = unitOfWork.Entity.GetAll().ToList();
            return View(vehicules);
        }

        // GET: Vehicule/Details/Immatricule
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

            Vehicule vehicule = unitOfWork.Entity.GetById(id);

            if (vehicule == null)
            {
                return HttpNotFound();
            }

            return View(vehicule);
        }

        // GET: Vehicule/Create
        public ActionResult Create()
        {
            if (Session["UserId"] == null)
            {
                // Session is active, redirect to another controller/action
                return RedirectToAction("Index", "Login");
            }
            return View();
        }

        // POST: Vehicule/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Immatricule,Model,TypeCarburant,KilometrageInitial")] Vehicule vehicule)
        {
            if (ModelState.IsValid)
            {
                unitOfWork.Entity.Insert(vehicule);
                unitOfWork.Save();
                return RedirectToAction("Index");
            }

            return View(vehicule);
        }

        // GET: Vehicule/Edit/Immatricule
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

            Vehicule vehicule = unitOfWork.Entity.GetById(id);

            if (vehicule == null)
            {
                return HttpNotFound();
            }

            return View(vehicule);
        }

        // POST: Vehicule/Edit/Immatricule
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Immatricule,Model,TypeCarburant,KilometrageInitial")] Vehicule vehicule)
        {
            if (ModelState.IsValid)
            {
                unitOfWork.Entity.Update(vehicule);
                unitOfWork.Save();
                return RedirectToAction("Index");
            }

            return View(vehicule);
        }

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

            int nonNullableId = id.Value;

            Vehicule vehicule = unitOfWork.Entity.GetById(nonNullableId);

            if (vehicule == null)
            {
                return HttpNotFound();
            }

            return View(vehicule);
        }

        // POST: Vehicule/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Vehicule vehicule = unitOfWork.Entity.GetById(id);

            if (vehicule == null)
            {
                return HttpNotFound();
            }

            unitOfWork.Entity.Delete(vehicule);
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
