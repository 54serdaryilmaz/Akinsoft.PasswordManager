using Akinsoft.PasswordManager.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Akinsoft.PasswordManager.Repositories;

namespace Akinsoft.PasswordManager.Controllers
{
    public class AccountController : Controller
    {
        PasswordRepository ps = new PasswordRepository();
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Login()
        {
            return View();
        }
         
        [HttpPost]
        public ActionResult Login(Users mdl)
        {
            mdl.Password = ps.Sifrele(mdl.Password);

            var usr = ps.LoginCheck(mdl);
            if (usr == true)
            {
                Session["UserName"] = mdl.UserName.ToString();
                return Redirect("/Home/Index");
            }
            else
            {
                return View();
            }

        }
        public ActionResult LogOff()
        {
            Session.Remove("UserName");
            return RedirectToAction("Login");
        }
        public ActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Register(Users mdl)
        {

            mdl.Password = ps.Sifrele(mdl.Password);
            ps.InsertUser(mdl);

            Session["UserName"] = mdl.UserName.ToString();
            return Redirect("/Home/Index");
        }
    }
}