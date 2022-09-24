using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DataLayer;

namespace ProjectUI.Controllers
{
    [Authorize]
    [ProjectAuthAction]
    public class DeveloperController : Controller
    {
        private SECHProjeEntities db = new SECHProjeEntities();

        //
        // GET: /Developer/

        public ActionResult Index()
        {
            return View(db.tblDevelopers.ToList());
        }

        //
        // GET: /Developer/Details/5

        public ActionResult Details(int id = 0)
        {
            tblDeveloper tbldeveloper = db.tblDevelopers.Find(id);
            if (tbldeveloper == null)
            {
                return HttpNotFound();
            }
            return View(tbldeveloper);
        }

        //
        // GET: /Developer/Create

        public ActionResult Create()
        {
            ViewBag.Units = new SelectList(db.tblUnits, "ID", "NAME");
            return View();
        }

        //
        // POST: /Developer/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(tblDeveloper tbldeveloper)
        {
            if (ModelState.IsValid)
            {
                db.tblDevelopers.Add(tbldeveloper);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Units = new SelectList(db.tblUnits, "ID", "NAME", tbldeveloper.UNIT_ID);
            return View(tbldeveloper);
        }

        //
        // GET: /Developer/Edit/5

        public ActionResult Edit(int id = 0)
        {
            tblDeveloper tbldeveloper = db.tblDevelopers.Find(id);
            if (tbldeveloper == null)
            {
                return HttpNotFound();
            }
            ViewBag.Units = new SelectList(db.tblUnits, "ID", "NAME", tbldeveloper.UNIT_ID);
            return View(tbldeveloper);
        }
    

        //
        // POST: /Developer/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(tblDeveloper tbldeveloper)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tbldeveloper).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tbldeveloper);
        }

        //
        // GET: /Developer/Delete/5

        public ActionResult Delete(int id = 0)
        {
            tblDeveloper tbldeveloper = db.tblDevelopers.Find(id);
            if (tbldeveloper == null)
            {
                return HttpNotFound();
            }
            return View(tbldeveloper);
        }

        //
        // POST: /Developer/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            tblDeveloper tbldeveloper = db.tblDevelopers.Find(id);
            db.tblDevelopers.Remove(tbldeveloper);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}