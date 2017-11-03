using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using BillPayApplication.Models;

namespace BillPayApplication.Controllers
{
    public class UsersController: Controller
    {
        // GET: Account

        public ActionResult Index()
        {
            using (BillPayDBEntities db = new BillPayDBEntities())
            {
                return View(db.Users.ToList());
            }
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(User user)
        {
            if (ModelState.IsValid)
            {
                using (BillPayDBEntities db = new BillPayDBEntities())
                {
                    db.Users.Add(user);
                    db.SaveChanges();
                }

                ModelState.Clear();
                ViewBag.Message = user.FirstName + " " + user.LastName + " Registration successful.";
            }
            return View();
        }

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(User user)
        {
            using (BillPayDBEntities db = new BillPayDBEntities())
            {
                var usr = db.Users.Single(u => u.Email == user.Email && u.Password == user.Password);

                if (usr != null)
                {
                    Session["UserID"] = usr.UserID.ToString();
                    Session["Email"] = usr.Email.ToString();
                    return RedirectToAction("LoggedIn");
                }
                else
                {
                    ModelState.AddModelError("", "Invalid Email or password.");
                }

                return View();
            }
        }

        public ActionResult LoggedIn()
        {
            if (Session["UserID"] != null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("LogIn");
            }
        }
    }
}
