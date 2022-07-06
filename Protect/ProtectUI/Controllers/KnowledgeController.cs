using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using BLL;
using Model;

namespace ProtectUI.Controllers
{
    public class KnowledgeController : Controller
    {
        private EnvironmentProtectEntities db = new EnvironmentProtectEntities();
        KnowledgeBLL knobll = new KnowledgeBLL();
        // GET: Knowledge
        public ActionResult Index()
        {
            var knowledge = db.Knowledge.Include(k => k.Administrator);
            return View(knowledge.ToList());
        }

        // GET: Knowledge/Details/5
        [ValidateInput(false)]
        public ActionResult Details(int? id)
        {
            Knowledge knowledge = db.Knowledge.Find(id);
            if (knowledge == null)
            {
                return HttpNotFound();
            }
            return View(knowledge);

        }

        // GET: Knowledge/Create
        public ActionResult Create()
        {
            ViewBag.admid = new SelectList(db.Administrator, "Admid", "Password");
            return View();
        }

        // POST: Knowledge/Create
        // 为了防止“过多发布”攻击，请启用要绑定到的特定属性；有关
        // 更多详细信息，请参阅 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "knoid,title,knowledgecontent,addtime,sorce,admid,knopicture,state")] Knowledge knowledge)
        {
            if (ModelState.IsValid)
            {
                db.Knowledge.Add(knowledge);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.admid = new SelectList(db.Administrator, "Admid", "Password", knowledge.admid);
            return View(knowledge);
        }

        // GET: Knowledge/Edit/
        [ValidateInput(false)]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Knowledge knowledge = db.Knowledge.Find(id);
            if (knowledge == null)
            {
                return HttpNotFound();
            }
            ViewBag.admid = new SelectList(db.Administrator, "Admid", "Password", knowledge.admid);
            return View(knowledge);
        }

        // POST: Knowledge/Edit/5
        // 为了防止“过多发布”攻击，请启用要绑定到的特定属性；有关
        // 更多详细信息，请参阅 https://go.microsoft.com/fwlink/?LinkId=317598。
        
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Edit([Bind(Include = "knoid,title,knowledgecontent,addtime,sorce,admid,knopicture,state")] Knowledge knowledge)
        {
            if (ModelState.IsValid)
            {
                db.Entry(knowledge).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.admid = new SelectList(db.Administrator, "Admid", "Password", knowledge.admid);
            return View(knowledge);
        }

        // GET: Knowledge/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Knowledge knowledge = db.Knowledge.Find(id);
            if (knowledge == null)
            {
                return HttpNotFound();
            }
            return View(knowledge);
        }

        // POST: Knowledge/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Knowledge knowledge = db.Knowledge.Find(id);
            db.Knowledge.Remove(knowledge);
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
