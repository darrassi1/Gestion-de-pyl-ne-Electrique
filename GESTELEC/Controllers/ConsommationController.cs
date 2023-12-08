using System;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using GESTELEC.Interfaces;
using GESTELEC.Models;
using GESTELEC.UnitOfWork;

namespace GESTELEC.Controllers
{
    public class ConsommationController : Controller
    {
        private readonly IUnitOfWork<Consommation> unitOfWork;
        private readonly IUnitOfWork<Vehicule> unitOfWorkVehicule;

        public ConsommationController()
        {
            unitOfWork = new UnitOfWork<Consommation>();
            unitOfWorkVehicule = new UnitOfWork<Vehicule>();
        }

        // GET: Consommation
        public ActionResult Index()
        {
            if (Session["UserId"] == null)
            {
                // Session is active, redirect to another controller/action
                return RedirectToAction("Index", "Login");
            }
            var consommations = unitOfWork.Entity.GetAll();
            return View(consommations);
        }

        // GET: Consommation/Details/5
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

            Consommation consommation = unitOfWork.Entity.GetById(id.Value);

            if (consommation == null)
            {
                return HttpNotFound();
            }

            return View(consommation);
        }

        // GET: Consommation/Create
        public ActionResult Create()
        {
            if (Session["UserId"] == null)
            {
                // Session is active, redirect to another controller/action
                return RedirectToAction("Index", "Login");
            }
            ViewBag.Immatricule = new SelectList(unitOfWorkVehicule.Entity.GetAll(), "Immatricule", "Immatricule");

            return View();
        }

        // POST: Consommation/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ConsommationId,VolumeGasoil,PrixBon,DateRemplissage,Kilometrage,Immatricule")] Consommation consommation)
        {
            if (ModelState.IsValid)
            {
                unitOfWork.Entity.Insert(consommation);
                unitOfWork.Save();

                return RedirectToAction("Index");
            }

            ViewBag.Immatricule = new SelectList(unitOfWorkVehicule.Entity.GetAll(), "Immatricule", "Immatricule", consommation.Immatricule);

            return View(consommation);
        }

        // GET: Consommation/Delete/5
        public ActionResult Delete(int? id)
        {
           
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            int consommationId = id.Value;
            Consommation consommation = unitOfWork.Entity.GetById(consommationId);

            if (consommation == null)
            {
                return HttpNotFound();
            }

            return View(consommation);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Consommation consommation = unitOfWork.Entity.GetById(id);

            if (consommation == null)
            {
                return HttpNotFound();
            }

            unitOfWork.Entity.Delete(consommation);
            unitOfWork.Save();

            return RedirectToAction("Index");
        }

        // GET: Consommation/Edit/5
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

            Consommation consommation = unitOfWork.Entity.GetById(id.Value);

            if (consommation == null)
            {
                return HttpNotFound();
            }

            ViewBag.Immatricule = new SelectList(unitOfWorkVehicule.Entity.GetAll(), "Immatricule", "Immatricule", consommation.Immatricule);

            return View(consommation);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ConsommationId, VolumeGasoil, PrixBon, DateRemplissage, Kilometrage, Immatricule")] Consommation consommation)
        {
            if (ModelState.IsValid)
            {
                unitOfWork.Entity.Update(consommation);
                unitOfWork.Save();

                return RedirectToAction("Details", new { id = consommation.ConsommationId });
            }

            ViewBag.Immatricule = new SelectList(unitOfWorkVehicule.Entity.GetAll(), "Immatricule", "Immatricule", consommation.Immatricule);

            return View(consommation);
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
