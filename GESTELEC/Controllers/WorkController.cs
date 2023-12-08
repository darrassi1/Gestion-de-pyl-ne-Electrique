using System.Collections.Generic;
using System;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using GESTELEC.Interfaces;
using GESTELEC.Models;
using GESTELEC.UnitOfWork;

namespace GESTELEC.Controllers
{
    public class WorkController : Controller
    {
        private readonly IUnitOfWork<Work> unitOfWork;
        private readonly IUnitOfWork<Ouvrier> unitOfWork1;
        private readonly IUnitOfWork<Pylone> unitOfWork2;

        public WorkController()
        {
            unitOfWork = new UnitOfWork<Work>();
            unitOfWork1 = new UnitOfWork<Ouvrier>();
            unitOfWork2 = new UnitOfWork<Pylone>();
        }

        // GET: Work
        public ActionResult Index(string filterOption, string searchValue)
        {
            if (Session["UserId"] == null)
            {
                // Session is active, redirect to another controller/action
                return RedirectToAction("Index", "Login");
            }
            var works = unitOfWork.Entity.GetAll();

            if (!string.IsNullOrEmpty(filterOption) && !string.IsNullOrEmpty(searchValue))
            {
                if (filterOption == "ouvrierCIN")
                {
                    works = works.Where(w => w.OuvrierCIN == searchValue);
                }
                else if (filterOption == "pyloneNumero")
                {
                    works = works.Where(w => w.PyloneNumero == searchValue);
                }
            }

            return View(works);
        }

        [HttpPost]
        public ActionResult SearchByFilter(string filterOption, string searchValue)
        {
            return RedirectToAction("Index", new { filterOption, searchValue });
        }

        [HttpPost]
        public ActionResult SearchByOuvrier(string ouvrierCIN)
        {
            return RedirectToAction("Index", new { ouvrierCIN });
        }

        [HttpPost]
        public ActionResult SearchByPylone(string pyloneNumero)
        {
            return RedirectToAction("Index", new { pyloneNumero });
        }



        // GET: Work/Details/5
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

            Work work = unitOfWork.Entity.GetById(id.Value);

            if (work == null)
            {
                return HttpNotFound();
            }

            return View(work);
        }

        // GET: Work/Create
        public ActionResult Create()
        {
            if (Session["UserId"] == null)
            {
                // Session is active, redirect to another controller/action
                return RedirectToAction("Index", "Login");
            }
            ViewBag.OuvrierCIN = new SelectList(unitOfWork1.Entity.GetAll(), "CIN", "NomComplet");
            ViewBag.PyloneNumero = new SelectList(unitOfWork2.Entity.GetAll(), "Numero", "Numero");

            return View();
        }

        // POST: Work/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "OuvrierCIN,PyloneNumero,Date,Description")] Work work)
        {
            if (ModelState.IsValid)
            {
                unitOfWork.Entity.Insert(work);
                unitOfWork.Save();

                return RedirectToAction("Index");
            }

            ViewBag.OuvrierCIN = new SelectList(unitOfWork1.Entity.GetAll(), "CIN", "NomComplet", work.OuvrierCIN);
            ViewBag.PyloneNumero = new SelectList(unitOfWork2.Entity.GetAll(), "Numero", "Numero", work.PyloneNumero);

            return View(work);
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

            int workId = id.Value;
            Work work = unitOfWork.Entity.GetById(workId);

            if (work == null)
            {
                return HttpNotFound();
            }

            return View(work);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Work work = unitOfWork.Entity.GetById(id);

            if (work == null)
            {
                return HttpNotFound();
            }

            unitOfWork.Entity.Delete(work);
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

            Work work = unitOfWork.Entity.GetById(id.Value);

            if (work == null)
            {
                return HttpNotFound();
            }

            ViewBag.OuvrierCIN = new SelectList(unitOfWork1.Entity.GetAll(), "CIN", "NomComplet", work.OuvrierCIN);
            ViewBag.PyloneNumero = new SelectList(unitOfWork2.Entity.GetAll(), "Numero", "Numero", work.PyloneNumero);

            return View(work);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "WorkId,OuvrierCIN,PyloneNumero,Date,Description")] Work work)
        {
            if (ModelState.IsValid)
            {
                unitOfWork.Entity.Update(work);
                unitOfWork.Save();

                return RedirectToAction("Details", new { id = work.WorkId });
            }

            ViewBag.OuvrierCIN = new SelectList(unitOfWork1.Entity.GetAll(), "CIN", "NomComplet", work.OuvrierCIN);
            ViewBag.PyloneNumero = new SelectList(unitOfWork2.Entity.GetAll(), "Numero", "Numero", work.PyloneNumero);

            return View(work);
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
