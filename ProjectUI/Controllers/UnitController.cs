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
    public class UnitController : Controller
    {
        private SECHProjeEntities db = new SECHProjeEntities();

        //
        // GET: /Unit/

       
        public ActionResult Index()
        {
         
            var res = db.tblUnits.ToList();
            
            return View(res);
        }

        //
        // GET: /Unit/Details/5

        public ActionResult Details(int id = 0)
        {
            tblUnit tblunit = db.tblUnits.Find(id);
            if (tblunit == null)
            {
                return HttpNotFound();
            }
            return View(tblunit);
        }

        //
        // GET: /Unit/Create

        public ActionResult Create()
        {

            var activeUser = ((DataLayer.tblDeveloper)Session["activeUser"]);
            ViewBag.UNIT_ID = new SelectList(db.tblUnits.Where(t => t.ID == activeUser.UNIT_ID || activeUser.YETKI == 0), "ID", "NAME");
            return View();
        }

        //
        // POST: /Unit/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(tblUnit tblunit)
        {
            if (ModelState.IsValid)
            {
                db.tblUnits.Add(tblunit);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            var activeUser = ((DataLayer.tblDeveloper)Session["activeUser"]);
            ViewBag.UNIT_ID = new SelectList(db.tblUnits.Where(t => t.ID == activeUser.UNIT_ID || activeUser.YETKI == 0), "ID", "NAME");
            return View(tblunit);
        }

        //
        // GET: /Unit/Edit/5

        public ActionResult Edit(int id = 0)
        {
            tblUnit tblunit = db.tblUnits.Find(id);
            if (tblunit == null)
            {
                return HttpNotFound();
            }
            return View(tblunit);
        }

        //
        // POST: /Unit/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(tblUnit tblunit)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tblunit).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tblunit);
        }

        //
        // GET: /Unit/Delete/5

        public ActionResult Delete(int id = 0)
        {
            tblUnit tblunit = db.tblUnits.Find(id);
            if (tblunit == null)
            {
                return HttpNotFound();
            }
            return View(tblunit);
        }

        //
        // POST: /Unit/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            tblUnit tblunit = db.tblUnits.Find(id);
            db.tblUnits.Remove(tblunit);
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