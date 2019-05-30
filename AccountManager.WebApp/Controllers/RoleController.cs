using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using AccountManager.Entities;
using AccountManager.WebApp.Models;

namespace AccountManager.WebApp.Controllers
{
    public class RoleController : Controller
    {
        private AccountDbContext db = CurrentDbContext.GetDbContext();

        // GET: Role
        public ActionResult Index()
        {
            return View(db.Roles.ToList());
        }

        // GET: Role/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Role role = db.Roles.Find(id);
            if (role == null)
            {
                return HttpNotFound();
            }
            return View(role);
        }
        public ActionResult Authorize(int? id)
        {
            if (id==null)
            {
                return View("Error");
            }

            Role role = db.Roles.Find(id);

            var groups = db.Permissions.GroupBy(p => p.Category);
            List<SelectListItem> selectListItems = new List<SelectListItem>();
            foreach (var group in groups)
            {
                SelectListGroup selectListGroup = new SelectListGroup { Name = group.Key };
                selectListItems.AddRange(group.Select(g => new SelectListItem
                {
                    Group = selectListGroup,
                    Selected = role.Permissions.Any(rp=>rp.Id==g.Id),
                    Value=g.Id.ToString(),
                    Text=g.Description
                }));
            }   
            return View(new SelectList(selectListItems));
        }
        // GET: Role/Create
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<ActionResult> Authorize(int? id,IEnumerable<int> permissionIds)
        {
            if (id==null)
            {
                return View("Error");
            }
            permissionIds = permissionIds ?? Enumerable.Empty<int>();
            
            Role role = db.Roles.Find(id);
            if (role==null)
            {
                return View("Error");
            }
            try {

                permissionIds.ToList().ForEach(pId =>
                {
                    if (!role.Permissions.Any(r => r.Id == pId))
                    {
                        role.Permissions.Add(db.Permissions.Find(pId));
                    }
                });

                //role.Permissions.ToList().ForEach(p => role.Permissions.Remove(p));//remove all exist permissions
                //permissionIds.ToList().ForEach(pId => role.Permissions.Add(db.Permissions.Find(pId)));//add permissions
                await db.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View("Error");
            }
        }

        // POST: Role/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,Active")] Role role)
        {
            if (ModelState.IsValid)
            {
                db.Roles.Add(role);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(role);
        }

        // GET: Role/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Role role = db.Roles.Find(id);
            if (role == null)
            {
                return HttpNotFound();
            }
            return View(role);
        }

        // POST: Role/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,Active")] Role role)
        {
            if (ModelState.IsValid)
            {
                db.Entry(role).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(role);
        }

        // GET: Role/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Role role = db.Roles.Find(id);
            if (role == null)
            {
                return HttpNotFound();
            }
            return View(role);
        }

        // POST: Role/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Role role = db.Roles.Find(id);
            db.Roles.Remove(role);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
