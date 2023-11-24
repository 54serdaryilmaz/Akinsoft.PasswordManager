using Akinsoft.PasswordManager.Models;
using Akinsoft.PasswordManager.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Akinsoft.PasswordManager.Controllers
{
    public class HomeController : Controller
    {
        PasswordRepository ps=new PasswordRepository();
        public ActionResult Index()
        {
            //if (Session["UserName"]==null)
            //{
            //    return Redirect("/Account/Login");
            //}
            ViewBag.CategoryID = new SelectList(ps.GetListCategory(), "CategoryID", "CategoryName");
            ViewBag.UserName = new SelectList(ps.GetListUsers(), "UserName", "UserName");
            return View(ps.GetListPass());
        }

        public ActionResult PassAdd()
        {
            ViewBag.CategoryID = new SelectList(ps.GetListCategory(), "CategoryID", "CategoryName");
            return View(ps.GetListPass());
        }
        [HttpPost]
        public JsonResult AddPassword(PasswordRecord mdl)
        {
             
        return Json(new { data = ps.InsertPass(mdl) }, JsonRequestBehavior.AllowGet);
             

        }
    }
}