using DataLayer;
using ProjectUI.ViewModels;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.DirectoryServices;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace ProjectUI.Controllers
{
    [AllowAnonymous]
    public class AccountController : Controller
    {
        private SECHProjeEntities db = new SECHProjeEntities();
       
        public ActionResult Login(string returnUrl)
        {
            Session["UserName"] = "";
            Session["AUTH_LEVEL"] = "";



            var model = new Account
            {
                ReturnUrl = returnUrl
            };
            return View(model);
        }

        //
        // POST: /Account/Create

        [HttpPost]
        public ActionResult Login(Account model)
        {

            if (this.ModelState.IsValid && CheckUserPassword(model.Name, model.Password))
            {
                tblDeveloper tbldeveloper = db.tblDevelopers.Where(p => p.SICIL_NO == model.Name).FirstOrDefault();
                if (tbldeveloper != null)
                {
                    Session["activeUser"] = tbldeveloper;
                    FormsAuthentication.SetAuthCookie(model.Name, false);
                    if (this.Url.IsLocalUrl(model.ReturnUrl))
                    {
                        // AddLog(model.Name);
                        return Redirect(model.ReturnUrl);
                    }
                    else
                    {
                        return RedirectToAction("Index", "Project", new { UnitID = tbldeveloper.UNIT_ID });
                    }
                }
                else//First Login
                {
                    return RedirectToAction("SetUnit", "Account", model);
                }

               
            }

            // If we got this far, something failed, redisplay form
            this.ModelState.AddModelError("", "Hatalı kullanıcı adı veya şifre girdiniz.");
            return View(model);
            
        }

        public ActionResult LogOff()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Project");
        }

        public ActionResult setUnit(Account model)
        {
            ViewBag.Units = new SelectList(db.tblUnits, "ID", "NAME");
            return View(model);
        }

        [HttpPost]
        public ActionResult setUnit(Account model,int UnitID)
        {

            if (this.ModelState.IsValid && CheckUserPassword(model.Name, model.Password))
            {
                try
                {
                    model.LDAPName = Session["UserName"].ToString();
                    db.tblDevelopers.Add(new tblDeveloper { AD = model.LDAPName, SICIL_NO = model.Name, UNIT_ID = model.UnitId, YETKI = 2 });
                    db.SaveChanges();
                     return Redirect(model.ReturnUrl);
                }
                catch
                {
                    return HttpNotFound();
                }

            }
            return View(model);
          
                 
        }

        private bool CheckUserPassword(string UserName, string Password)
        {
            try
            {
                string domainAndUsername = ConfigurationManager.AppSettings["DirectoryDomain"] + @"\" + UserName;

                DirectoryEntry entry = new DirectoryEntry(ConfigurationManager.AppSettings["DirectoryPath"], domainAndUsername, Password);

                Object obj = entry.NativeObject;

                DirectorySearcher search = new DirectorySearcher(entry);

                search.Filter = "(SAMAccountName=" + UserName + ")";
                search.PropertiesToLoad.Add("cn");
                search.PropertiesToLoad.Add("name");
                search.PropertiesToLoad.Add("pager");
                search.PropertiesToLoad.Add("company");

                SearchResult result = search.FindOne();

                if (result != null)
                {
                    Session["UserName"] = (GetProperty(result, "name"));
                    Session["IpPhone"] = (GetProperty(result, "pager"));
                    Session["Company"] = (GetProperty(result, "company"));
                    Session["AUTH_LEVEL"] = "0";
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception)
            {
                return false;//false
            }
        }

        private string GetProperty(SearchResult searchResult, string PropertyName)
        {
            if (searchResult.Properties.Contains(PropertyName))
                return searchResult.Properties[PropertyName][0].ToString();
            else
                return string.Empty;
        }
    }
}
