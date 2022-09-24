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
    public class TeamController : Controller
    {
        private SECHProjeEntities db = new SECHProjeEntities();

        //
        // GET: /Team/

        public ActionResult Index()
        {
            var tblteams = db.tblTeams.Include(t => t.tblDeveloper).Include(t => t.tblProject);
            return View(tblteams.ToList());
        }

        //
        // GET: /Team/Details/5

        public ActionResult Details(int id = 0)
        {
            tblTeam tblteam = db.tblTeams.Find(id);
            if (tblteam == null)
            {
                return HttpNotFound();
            }
            return View(tblteam);
        }

        //
        // GET: /Team/Create

        public ActionResult Create()
        {
            ViewBag.DEVELOPER_ID = new SelectList(db.tblDevelopers, "ID", "AD");
            ViewBag.PROJECT_ID = new SelectList(db.tblProjects, "ID", "FIRSAT_ID");
            return View();
        }

        //
        // POST: /Team/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(tblTeam tblteam)
        {
            if (ModelState.IsValid)
            {
                db.tblTeams.Add(tblteam);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.DEVELOPER_ID = new SelectList(db.tblDevelopers, "ID", "AD", tblteam.DEVELOPER_ID);
            ViewBag.PROJECT_ID = new SelectList(db.tblProjects, "ID", "FIRSAT_ID", tblteam.PROJECT_ID);
            return View(tblteam);
        }

        //
        // GET: /Team/Edit/5

        public ActionResult Edit(int id = 0)
        {
            tblTeam tblteam = db.tblTeams.Find(id);
            if (tblteam == null)
            {
                return HttpNotFound();
            }
            ViewBag.DEVELOPER_ID = new SelectList(db.tblDevelopers, "ID", "AD", tblteam.DEVELOPER_ID);
            ViewBag.PROJECT_ID = new SelectList(db.tblProjects, "ID", "FIRSAT_ID", tblteam.PROJECT_ID);
            return View(tblteam);
        }

        //
        // POST: /Team/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(tblTeam tblteam)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tblteam).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.DEVELOPER_ID = new SelectList(db.tblDevelopers, "ID", "AD", tblteam.DEVELOPER_ID);
            ViewBag.PROJECT_ID = new SelectList(db.tblProjects, "ID", "FIRSAT_ID", tblteam.PROJECT_ID);
            return View(tblteam);
        }

        //
        // GET: /Team/Delete/5

        public ActionResult Delete(int id = 0)
        {
            tblTeam tblteam = db.tblTeams.Find(id);
            if (tblteam == null)
            {
                return HttpNotFound();
            }
            return View(tblteam);
        }

        //
        // POST: /Team/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            tblTeam tblteam = db.tblTeams.Find(id);
            db.tblTeams.Remove(tblteam);
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