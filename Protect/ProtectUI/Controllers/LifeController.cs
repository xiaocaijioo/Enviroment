using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Model;

namespace ProtectUI.Controllers
{
    public class LifeController : Controller
    {
        private EnvironmentProtectEntities db = new EnvironmentProtectEntities();

        // GET: Life
        public ActionResult Index()
        {
            var life = db.Life.Include(l => l.Administrator).Include(l => l.LifeType);
            return View(life.ToList());
        }

        // GET: Life/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Life life = db.Life.Find(id);
            if (life == null)
            {
                return HttpNotFound();
            }
            return View(life);
        }

        // GET: Life/Create
        public ActionResult Create()
        {
            ViewBag.admid = new SelectList(db.Administrator, "Admid", "Password");
            ViewBag.typeid = new SelectList(db.LifeType, "typeid", "typename");
            return View();
        }

        // POST: Life/Create
        // 为了防止“过多发布”攻击，请启用要绑定到的特定属性；有关
        // 更多详细信息，请参阅 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "lifeid,typeid,title,lifecontent,addtime,admid,cover,state")] Life life)
        {
            if (ModelState.IsValid)
            {
                db.Life.Add(life);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.admid = new SelectList(db.Administrator, "Admid", "Password", life.admid);
            ViewBag.typeid = new SelectList(db.LifeType, "typeid", "typename", life.typeid);
            return View(life);
        }

        // GET: Life/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Life life = db.Life.Find(id);
            if (life == null)
            {
                return HttpNotFound();
            }
            ViewBag.admid = new SelectList(db.Administrator, "Admid", "Password", life.admid);
            ViewBag.typeid = new SelectList(db.LifeType, "typeid", "typename", life.typeid);
            return View(life);
        }

        // POST: Life/Edit/5
        // 为了防止“过多发布”攻击，请启用要绑定到的特定属性；有关
        // 更多详细信息，请参阅 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "lifeid,typeid,title,lifecontent,addtime,admid,cover,state")] Life life)
        {
            if (ModelState.IsValid)
            {
                db.Entry(life).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.admid = new SelectList(db.Administrator, "Admid", "Password", life.admid);
            ViewBag.typeid = new SelectList(db.LifeType, "typeid", "typename", life.typeid);
            return View(life);
        }

        // GET: Life/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Life life = db.Life.Find(id);
            if (life == null)
            {
                return HttpNotFound();
            }
            return View(life);
        }

        // POST: Life/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Life life = db.Life.Find(id);
            db.Life.Remove(life);
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
