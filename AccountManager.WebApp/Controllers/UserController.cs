using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using AccountManager.Entities;
using AccountManager.WebApp.Attribute;
using AccountManager.WebApp.Models;
using AccountManager.WebApp.Security;

namespace AccountManager.WebApp.Controllers
{
    //[CustomAuthorize(PermissionNames = "UserIndex,UserDetails,UserCreate,UserEdit,UserDelete,UserDeleteConfirmed")]
    [CustomAuthorize]
    public class UserController : Controller
    {
        private AccountDbContext db = CurrentDbContext.GetDbContext();
        IAuthenticationProvider authorizeProvider = new AuthenticationProvider();
        // GET: User
        public ActionResult Index()
        {
            return View(db.Users.ToList());
        }

        [HttpGet]
        [AllowAnonymous]
        public ActionResult Login(string ReturnUrl)
        {
            ViewBag.ReturnUrl = ReturnUrl;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [AllowAnonymous]
        public ActionResult Login(LoginModel model,string ReturnUrl)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError(string.Empty, "User Name Or Password not Currect!");
                return View(model);
            }
            var user = db.Users.FirstOrDefault(u => u.Name == model.UserName && u.Password == model.UserPassword);
            if (user == null)
            {
                ModelState.AddModelError(string.Empty, "User Name Or Password not Currect!");
                return View(model);
            }
            authorizeProvider.SignIn(user,model.RememberMe);
            if (!string.IsNullOrEmpty(ReturnUrl))
            {
                return Redirect(ReturnUrl);
            }
            return RedirectToAction("Index","Home");
        }

        // GET: User/Details/5
        public ActionResult Details(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.Users.FirstOrDefault(u=>u.Name==name);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        [AllowAnonymous]
        public ActionResult LogOut()
        {
            authorizeProvider.SignOut();
            return RedirectToAction("Index", "Home");
        }
        // GET: User/Create
        [AllowAnonymous]
        public ActionResult Create()
        {
            return View();
        }

        // POST: User/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Password,Name,Active")] User user)
        {
            if (ModelState.IsValid)
            {
                db.Users.Add(user);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(user);
        }

        // GET: User/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // POST: User/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Password,Name,Active")] User user)
        {
            if (ModelState.IsValid)
            {
                db.Entry(user).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(user);
        }

        // GET: User/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // POST: User/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            User user = db.Users.Find(id);
            db.Users.Remove(user);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Search()
        {
            return View();
        }
    }
}
