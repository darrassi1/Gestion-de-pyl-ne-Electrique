using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using GESTELEC.Interfaces;
using GESTELEC.Models;
using GESTELEC.UnitOfWork;

namespace GESTELEC.Controllers
{
    public class PaiementController : Controller
    {
        private readonly IUnitOfWork<Paiement> unitOfWork;
        private readonly IUnitOfWork<Ouvrier> unitOfWork1;
        private readonly IUnitOfWork<Pylone> unitOfWork2;

        public PaiementController()
        {
            unitOfWork = new UnitOfWork<Paiement>();
            unitOfWork1 = new UnitOfWork<Ouvrier>();
            unitOfWork2 = new UnitOfWork<Pylone>();
        }

        // GET: Paiement
        // GET: Paiement
        public ActionResult Index(string filterOption, string searchValue)
        {
            if (Session["UserId"] == null)
            {
                // Session is active, redirect to another controller/action
                return RedirectToAction("Index", "Login");
            }
            var paiements = unitOfWork.Entity.GetAll();

            if (!string.IsNullOrEmpty(filterOption) && !string.IsNullOrEmpty(searchValue))
            {
                if (filterOption == "ouvrierCIN")
                {
                    paiements = paiements.Where(p => p.CIN == searchValue);
                }
                else if (filterOption == "pyloneId" && int.TryParse(searchValue, out int pyloneId))
                {
                    paiements = paiements.Where(p => p.PyloneId == pyloneId);
                }
            }

            return View(paiements);
        }

        [HttpPost]
        public ActionResult SearchByFilter(string filterOption, string searchValue)
        {
            if (filterOption == "ouvrierCIN")
            {
                return RedirectToAction("Index", new { filterOption, searchValue });
            }
            else if (filterOption == "pyloneId")
            {
                if (int.TryParse(searchValue, out int pyloneId))
                {
                    return RedirectToAction("Index", new { filterOption, searchValue = pyloneId });
                }
            }

            // If the filter option or search value is invalid, redirect to the Index without any parameters
            return RedirectToAction("Index");
        }

       


        // GET: Paiement/Details/5
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

            Paiement paiement = unitOfWork.Entity.GetById(id.Value);

            if (paiement == null)
            {
                return HttpNotFound();
            }

            return View(paiement);
        }

        // GET: Paiement/Create
        public ActionResult Create()
        {
            if (Session["UserId"] == null)
            {
                // Session is active, redirect to another controller/action
                return RedirectToAction("Index", "Login");
            }
            ViewBag.CIN = new SelectList(unitOfWork1.Entity.GetAll(), "CIN", "NomComplet");
            ViewBag.PyloneId = new SelectList(unitOfWork2.Entity.GetAll(), "PyloneId", "Numero");

            return View();
        }

        // POST: Paiement/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Montant,DatePaiement,CIN,PyloneId")] Paiement paiement)
        {
            if (ModelState.IsValid)
            {
                unitOfWork.Entity.Insert(paiement);
                unitOfWork.Save();

                return RedirectToAction("Index");
            }

            ViewBag.CIN = new SelectList(unitOfWork1.Entity.GetAll(), "CIN", "NomComplet", paiement.CIN);
            ViewBag.PyloneId = new SelectList(unitOfWork2.Entity.GetAll(), "PyloneId", "Numero", paiement.PyloneId);

            return View(paiement);
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

            int paiementId = id.Value;
            Paiement paiement = unitOfWork.Entity.GetById(paiementId);

            if (paiement == null)
            {
                return HttpNotFound();
            }

            return View(paiement);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Paiement paiement = unitOfWork.Entity.GetById(id);

            if (paiement == null)
            {
                return HttpNotFound();
            }

            unitOfWork.Entity.Delete(paiement);
            unitOfWork.Save();

            return RedirectToAction("Index");
        }

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

            Paiement paiement = unitOfWork.Entity.GetById(id.Value);

            if (paiement == null)
            {
                return HttpNotFound();
            }

            ViewBag.CIN = new SelectList(unitOfWork1.Entity.GetAll(), "CIN", "NomComplet", paiement.CIN);
            ViewBag.PyloneId = new SelectList(unitOfWork2.Entity.GetAll(), "PyloneId", "Numero", paiement.PyloneId);

            return View(paiement);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "PaiementId,Montant,DatePaiement,CIN,PyloneId")] Paiement paiement)
        {
            if (ModelState.IsValid)
            {
                unitOfWork.Entity.Update(paiement);
                unitOfWork.Save();

                return RedirectToAction("Details", new { id = paiement.PaiementId });
            }

            ViewBag.CIN = new SelectList(unitOfWork1.Entity.GetAll(), "CIN", "NomComplet", paiement.CIN);
            ViewBag.PyloneId = new SelectList(unitOfWork2.Entity.GetAll(), "PyloneId", "Numero", paiement.PyloneId);

            return View(paiement);
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

