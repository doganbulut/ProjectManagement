using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DataLayer;
using ProjectUI.ViewModels;
using System.Web.Helpers;
using System.Web.Security;
using System.Collections;


namespace ProjectUI.Controllers
{
    [Authorize]
    public class ProjectController : Controller

    {
        private SECHProjeEntities db = new SECHProjeEntities();

        //
        // GET: /Project/

        public ActionResult Index(int? UnitID,int? DurumID)
        {
            var activeUser = ((DataLayer.tblDeveloper)Session["activeUser"]);

            if (activeUser == null)
                return Redirect("Account\\Login");


            if (UnitID == null && activeUser != null)
                UnitID = activeUser.UNIT_ID;

            if (DurumID == null && activeUser != null)
                DurumID = 99;


            List<tblProject> tblprojects;

            if (DurumID==99)
                tblprojects = db.tblProjects.Where(p => p.UNIT_ID == UnitID).Include(t => t.tblUnit).ToList();
            else
                tblprojects = db.tblProjects.Where(p => p.UNIT_ID == UnitID && p.DURUM_ID == DurumID).Include(t => t.tblUnit).ToList();
              
  

            ViewBag.ShowSpecialColumn = activeUser.UNIT_ID == 6 ? true : false;
            ViewBag.UNIT_ID = new SelectList(db.tblUnits, "ID", "NAME");

            Dictionary<int, string> durumlist = new Dictionary<int, string>();
            durumlist.Add(0, "Planlandı");
            durumlist.Add(1, "Devam Ediyor");
            durumlist.Add(2, "Tamamlandı");
            durumlist.Add(99,"Hepsi");
            ViewBag.DURUM_ID = new SelectList(durumlist, "Key", "Value");


         

        
            return View(tblprojects);

        }

        //
        // GET: /Project/Details/5

        public ActionResult Details(int id = 0)
        {
            var activeUser = ((DataLayer.tblDeveloper)Session["activeUser"]);
            ViewBag.ShowSpecialColumn = activeUser.UNIT_ID == 6 ? true : false;

            tblProject tblproject = db.tblProjects.Find(id);
            if (tblproject == null)
            {
                return HttpNotFound();
            }
            return View(tblproject);
        }

        //
        // GET: /Project/Create

        public ActionResult Create()
        {
            var activeUser = ((DataLayer.tblDeveloper)Session["activeUser"]);
            ViewBag.UNIT_ID = new SelectList(db.tblUnits.Where(t => t.ID == activeUser.UNIT_ID || activeUser.YETKI==0), "ID", "NAME");
            ViewBag.ShowSpecialColumn = activeUser.UNIT_ID == 6 ? true : false;
            //ViewBag.UNIT_ID = new SelectList(db.tblUnits, "ID", "NAME");
            ViewBag.PARA_BIRIMI = new SelectList(Helper.GetCurrency(), "Key", "Value");
            if (ViewBag.FirsatIDRequired == null)
                ViewBag.FirsatIDRequired = true;
            return View();
            
        }

        //
        // POST: /Project/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(tblProject tblproject)
        {
            ViewBag.FirsatIDRequired = (tblproject.SIRKET_ICI);

            if (ModelState.IsValid && (tblproject.SIRKET_ICI || (!tblproject.SIRKET_ICI && tblproject.FIRSAT_ID != null)))
            {
                tblproject.DURUM_ID = Helper.GetProjectStateID(tblproject.YUZDE_DURUM);
                db.tblProjects.Add(tblproject);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            var activeUser = ((DataLayer.tblDeveloper)Session["activeUser"]);
            ViewBag.UNIT_ID = new SelectList(db.tblUnits.Where(t=>t.ID==activeUser.UNIT_ID), "ID", "NAME", tblproject.UNIT_ID);
            ViewBag.ShowSpecialColumn = activeUser.UNIT_ID == 6 ? true : false;
            ViewBag.PARA_BIRIMI = new SelectList(Helper.GetCurrency(), "Key", "Value");
            return View(tblproject);
        }

        //
        // GET: /Project/Edit/5

        public ActionResult Edit(int id = 0)
        {
            tblProject tblproject = db.tblProjects.Find(id);
            ViewBag.PARA_BIRIMI = new SelectList(Helper.GetCurrency(), "Key", "Value");
            
            ViewBag.UNIT_ID = new SelectList(db.tblUnits, "ID", "NAME", tblproject.UNIT_ID);
            if (ViewBag.ProjeFinishValid == null)
                ViewBag.ProjeFinishValid = false;

            var activeUser = ((DataLayer.tblDeveloper)Session["activeUser"]);
            if (activeUser == null)
                return Redirect("Account\\Login");
            ViewBag.ShowSpecialColumn = activeUser.UNIT_ID == 6 ? true : false;
            if (ViewBag.FirsatIDRequired == null)
                ViewBag.FirsatIDRequired = true;
           
            return View(tblproject);
            //if (tblproject.KURULUS_UC_SAYISI==id)
        }

        //
        // POST: /Project/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(tblProject tblproject)
        {
            ViewBag.PARA_BIRIMI = new SelectList(Helper.GetCurrency(), "Key", "Value");
            ViewBag.UNIT_ID = new SelectList(db.tblUnits, "ID", "NAME", tblproject.UNIT_ID);

            var activeUser = ((DataLayer.tblDeveloper)Session["activeUser"]);
            if (activeUser == null)
                return Redirect("Account\\Login");
            ViewBag.ShowSpecialColumn = activeUser.UNIT_ID == 6 ? true : false;

            ViewBag.FirsatIDRequired = (tblproject.SIRKET_ICI);

            if (ModelState.IsValid && (tblproject.SIRKET_ICI || (!tblproject.SIRKET_ICI && tblproject.FIRSAT_ID != null)))
            {
                ViewBag.ProjeFinishValid = false;

                if (tblproject.UNIT_ID == 6)
                {
                    float toplamUcSayisi = tblproject.TOPLAM_UC_SAYISI.HasValue ? tblproject.TOPLAM_UC_SAYISI.Value : 0;
                    float kurulusUcSayisi = tblproject.KURULUS_UC_SAYISI.HasValue ? tblproject.KURULUS_UC_SAYISI.Value : 0;
                    if (toplamUcSayisi != 0)
                        tblproject.YUZDE_DURUM = (int)Math.Floor((kurulusUcSayisi / toplamUcSayisi) * 100);
                }

                tblproject.DURUM_ID = Helper.GetProjectStateID(tblproject.YUZDE_DURUM);
                db.Entry(tblproject).State = EntityState.Modified;
                db.SaveChanges();

                if (tblproject.YUZDE_DURUM == 100 && tblproject.GERCEKLESEN_BITIS_TARIH == null)
                {
                    ViewBag.ProjeFinishValid = true;
                    return View(tblproject);
                }

                ViewBag.ProjeFinishValid = false;
                return RedirectToAction("Index");
            }

            ViewBag.ProjeFinishValid = false;
            return View(tblproject);
        }

        //
        // GET: /Project/Delete/5

        public ActionResult Delete(int id = 0)
        {
            tblProject tblproject = db.tblProjects.Find(id);
            if (tblproject == null)
            {
                return HttpNotFound();
            }
            return View(tblproject);
        }

        //
        // POST: /Project/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            tblProject tblproject = db.tblProjects.Find(id);
            db.tblProjects.Remove(tblproject);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Exception(string Message = "")
        {
            ViewBag.Exception = Message;
            return View();
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}