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
    public class ReposController : Controller
    {
        private readonly IUnitOfWork<Repos> unitOfWork;
        private readonly IUnitOfWork<Ouvrier> unitOfWork1;

        public ReposController()
        {
            unitOfWork = new UnitOfWork<Repos>();
            unitOfWork1 = new UnitOfWork<Ouvrier>();
        }

        // GET: Repos
        public ActionResult Index()
        {
            if (Session["UserId"] == null)
            {
                // Session is active, redirect to another controller/action
                return RedirectToAction("Index", "Login");
            }
            var reposList = unitOfWork.Entity.GetAll().ToList();
            return View(reposList);
        }

        [HttpPost]
        public ActionResult SearchByOuvrier(string ouvrierCIN)
        {
            var repos = unitOfWork.Entity.GetAll();

            if (!string.IsNullOrEmpty(ouvrierCIN))
            {
             
                repos = repos.Where(r => r.CIN == ouvrierCIN);
            }

            return View("Index", repos);
        }


        // GET: Repos/Details/5
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

            Repos repos = unitOfWork.Entity.GetById(id.Value);

            if (repos == null)
            {
                return HttpNotFound();
            }

            return View(repos);
        }

        // GET: Repos/Create
        public ActionResult Create()
        {
            if (Session["UserId"] == null)
            {
                // Session is active, redirect to another controller/action
                return RedirectToAction("Index", "Login");
            }
            ViewBag.CIN = new SelectList(unitOfWork1.Entity.GetAll(), "CIN", "NomComplet");
            return View();
        }

        // POST: Repos/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ReposId,DateRepos,MotifRepos,CIN")] Repos repos)
        {
            if (ModelState.IsValid)
            {
                unitOfWork.Entity.Insert(repos);
                unitOfWork.Save();
                return RedirectToAction("Index");
            }

            ViewBag.CIN = new SelectList(unitOfWork1.Entity.GetAll(), "CIN", "NomComplet", repos.CIN);
            return View(repos);
        }

        // GET: Repos/Edit/5
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

            Repos repos = unitOfWork.Entity.GetById(id.Value);

            if (repos == null)
            {
                return HttpNotFound();
            }

            ViewBag.CIN = new SelectList(unitOfWork1.Entity.GetAll(), "CIN", "NomComplet", repos.CIN);
            return View(repos);
        }

        // POST: Repos/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ReposId,DateRepos,MotifRepos,CIN")] Repos repos)
        {
            if (ModelState.IsValid)
            {
                unitOfWork.Entity.Update(repos);
                unitOfWork.Save();
                return RedirectToAction("Index");
            }

            ViewBag.CIN = new SelectList(unitOfWork1.Entity.GetAll(), "CIN", "NomComplet", repos.CIN);
            return View(repos);
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
