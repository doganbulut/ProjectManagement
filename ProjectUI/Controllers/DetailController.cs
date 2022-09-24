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
   
    public class DetailController : Controller
    {
        private SECHProjeEntities db = new SECHProjeEntities();

        //
        // GET: /Detail/

        public ActionResult Index()
        {
            var tbldetails = db.tblDetails.Include(t => t.tblProject);
            return View(tbldetails.ToList());
        }

        [ChildActionOnly]
        public ActionResult IndexProjectDetail(int id)
        {
            var tbldetails = db.tblDetails.Where(p => p.PROJE_ID == id).Include(t => t.tblProject);
            return PartialView(tbldetails.ToList());
        }

        //
        // GET: /Detail/Details/5

        public ActionResult Details(int id = 0)
        {
            tblDetail tbldetail = db.tblDetails.Find(id);
            if (tbldetail == null)
            {
                return HttpNotFound();
            }
            return View(tbldetail);
        }

        //
        // GET: /Detail/Create

        public ActionResult Create(int id = 0)
        {
            tblDetail tbldetail = new tblDetail();
            tbldetail.PROJE_ID = id;
            tbldetail.CALISMA_TARIH = DateTime.Now.Date;
            tbldetail.tblProject = db.tblProjects.Where(p => p.ID == id).FirstOrDefault();
            return View(tbldetail);
        }

        //
        // POST: /Detail/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(tblDetail tbldetail)
        {

            if (ModelState.IsValid)
            {
                db.tblDetails.Add(tbldetail);
                db.SaveChanges();
                return RedirectToAction("Create",tbldetail.PROJE_ID);
            }

            ViewBag.PROJE_ID = new SelectList(db.tblProjects, "ID", "FIRSAT_ID", tbldetail.PROJE_ID);
            return View(tbldetail);
        }

        //
        // GET: /Detail/Edit/5

        public ActionResult Edit(int id = 0)
        {
            tblDetail tbldetail = db.tblDetails.Find(id);
            if (tbldetail == null)
            {
                return HttpNotFound();
            }
            ViewBag.PROJE_ID = new SelectList(db.tblProjects, "ID", "FIRSAT_ID", tbldetail.PROJE_ID);
            return View(tbldetail);
        }

        //
        // POST: /Detail/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(tblDetail tbldetail)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tbldetail).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.PROJE_ID = new SelectList(db.tblProjects, "ID", "FIRSAT_ID", tbldetail.PROJE_ID);
            return View(tbldetail);
        }

        //
        // GET: /Detail/Delete/5

        public ActionResult Delete(int id = 0)
        {
            tblDetail tbldetail = db.tblDetails.Find(id);
            if (tbldetail == null)
            {
                return HttpNotFound();
            }
            return View(tbldetail);
        }

        //
        // POST: /Detail/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            tblDetail tbldetail = db.tblDetails.Find(id);
            db.tblDetails.Remove(tbldetail);
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