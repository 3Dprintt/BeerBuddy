using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using BeerBuddy.Models;

namespace BeerBuddy.Controllers
{
    public class T_userController : Controller
    {
        private BeerBuddyEntities db = new BeerBuddyEntities();

        // GET: T_user
        public ActionResult Index()
        {
            return View(db.T_user.ToList());
        }

        // GET: T_user/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            T_user t_user = db.T_user.Find(id);
            if (t_user == null)
            {
                return HttpNotFound();
            }
            return View(t_user);
        }

        // GET: T_user/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: T_user/Create
        // Afin de déjouer les attaques par survalidation, activez les propriétés spécifiques auxquelles vous voulez établir une liaison. Pour 
        // plus de détails, consultez https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Nom,Prenom")] T_user t_user)
        {
            if (ModelState.IsValid)
            {
                db.T_user.Add(t_user);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(t_user);
        }

        // GET: T_user/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            T_user t_user = db.T_user.Find(id);
            if (t_user == null)
            {
                return HttpNotFound();
            }
            return View(t_user);
        }

        // POST: T_user/Edit/5
        // Afin de déjouer les attaques par survalidation, activez les propriétés spécifiques auxquelles vous voulez établir une liaison. Pour 
        // plus de détails, consultez https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Nom,Prenom")] T_user t_user)
        {
            if (ModelState.IsValid)
            {
                db.Entry(t_user).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(t_user);
        }

        // GET: T_user/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            T_user t_user = db.T_user.Find(id);
            if (t_user == null)
            {
                return HttpNotFound();
            }
            return View(t_user);
        }

        // POST: T_user/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            T_user t_user = db.T_user.Find(id);
            db.T_user.Remove(t_user);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
