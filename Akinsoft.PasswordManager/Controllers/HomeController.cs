using Akinsoft.PasswordManager.Models;
using Akinsoft.PasswordManager.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Web;
using System.Web.Mvc;

namespace Akinsoft.PasswordManager.Controllers
{
    public class HomeController : Controller
    {
        PasswordRepository ps=new PasswordRepository();

        
        public ActionResult Index()
        {
            if (Session["UserName"] == null)
            {
                return Redirect("/Account/Login");
            }
            ViewBag.CategoryID = new SelectList(ps.GetListCategory(), "CategoryID", "CategoryName");
          
            return View(ps.GetListPass(Session["UserName"].ToString()));
        }

        public ActionResult PassAdd()
        {
            ViewBag.CategoryID = new SelectList(ps.GetListCategory(), "CategoryID", "CategoryName");
            return View(ps.GetListPass(Session["UserName"].ToString()));
        }
        [HttpPost]
        public JsonResult AddPassword(PasswordRecord mdl)
        {
             
        return Json(new { data = ps.InsertPass(mdl) }, JsonRequestBehavior.AllowGet);
             

        }



        public JsonResult GetPasswords()
        {
            if (Session["UserName"] == null)
            {
                return Json(new { data = "Error" }, JsonRequestBehavior.AllowGet);
            }

            return Json(new { data = ps.GetListPass(Session["UserName"].ToString()) }, JsonRequestBehavior.AllowGet);
        }





    }
}