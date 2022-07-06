using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using BLL;
using Model;

namespace ProtectUI.Controllers
{
    public class AdmController : Controller
    {

        private EnvironmentProtectEntities db = new EnvironmentProtectEntities();
        private PostBLL postbll = new PostBLL();
        private UsersBLL userbll = new UsersBLL();
        private AdmBLL admbll = new AdmBLL();
        private KnowledgeBLL knobll = new KnowledgeBLL();
        private LifeBLL lifebll = new LifeBLL();
        private LifeTypeBLL typebll = new LifeTypeBLL();
        private PostCommentBLL postcommentbll = new PostCommentBLL();

        #region 管理员登录
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(Administrator administrator)
        {
            SqlDataReader reader = admbll.AdmLogin(administrator);

            string result = reader.HasRows ? "成功" : "失败";
            if (result == "成功")
            {
                Session["admid"] = administrator.Admid;
                Session["password"] = administrator.Password;
                return RedirectToAction("Index", "Adm");
            }
            if (result == "失败")
            {
                Console.WriteLine("<script language='javascript'>alert(登录失败！)</script>");
            }
            return View();
        }
        #endregion

        #region 管理员注销登录
        public ActionResult Loginout()
        {
            Session["adminid"] = null;
            Session["adminname"] = null;
            return Redirect("/Home/Index");
        }
        #endregion

        #region 管理员发布环保知识
        [ValidateInput(false)]
        public ActionResult AddKnowledge()
        {

            if (Request.IsAjaxRequest())
            {
                return PartialView();
            }
            else return View();
        }

        #endregion

        #region 提交环保知识
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult AddNewKnowledge(FormCollection form)
        {
            try
            {
                Knowledge kno = new Knowledge();
                kno.title = form["title"];
                //kno.admid = Convert.ToInt32(Session["admid"]);
                kno.admid = Convert.ToInt32(Session["admid"]);
                kno.knopicture = form["cover"];
                kno.knowledgecontent = form["knowledgecontent"];
                kno.addtime = DateTime.Now;
                kno.state = Convert.ToInt32(form["workstate"]);
                string msg = knobll.AddEntity(kno);
                if (msg == "success")
                {
                    return Content("suc");
                }
                else
                {
                    return Content("fail");
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                return Content(dbEx.Message);
            }
        }
        #endregion

        #region 上传环保知识图片图片
        public JsonResult UpWorkImg()
        {
            string src = "";
            HttpPostedFileBase file = Request.Files[0];
            string savefilePath = "";
            //判断文件是否为空
            if (file != null)
            {
                //获取文件类型
                string fileExtension = Path.GetExtension(file.FileName);
                //自定义文件名（时间+唯一标识符+后缀）
                string fileName = DateTime.Now.ToString("yyyy-MM-dd") + Guid.NewGuid() + fileExtension;
                //判断是否存在需要的目录，不存在则创建 
                if (!Directory.Exists(Server.MapPath("~/images/works")))
                {
                    Directory.CreateDirectory(Server.MapPath("~/images/works"));
                }
                //拼接保存文件的详细路径
                savefilePath = Server.MapPath("~/images/works/") + fileName;
                //若扩展名不为空则判断文件是否是指定图片类型
                if (fileExtension != null)
                {
                    if ("(.bmp)|(.png)|(.jpg)|(.gif)|(.jpeg)|(.BMP)|(.PNG)|(.JPG)|(.GIF)|(.JPEG)".Contains(fileExtension))
                    {
                        //拼接返回的Img标签
                        src = "../../images/works/" + fileName;
                    }
                    file.SaveAs(savefilePath);
                    return Json(new { code = 200, url = src }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new { code = 2000, url = src }, JsonRequestBehavior.AllowGet);
                }
            }
            else
            {
                src = null;
                return Json(new { code = 2000, url = src }, JsonRequestBehavior.AllowGet);
            }

        }
        #endregion

        #region 管理员发布生活方式
        public ActionResult AddLife()
        {
            IEnumerable<LifeType> lifetypes = typebll.LoadEntities(u => true);
            ViewBag.typename = new SelectList(lifetypes, "typeid", "typename");
            if (Request.IsAjaxRequest())
            {
                return PartialView();
            }
            else return View();

        }
        #endregion

        #region 提交生活方式
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult AddNewLife(FormCollection form)
        {
            try
            {
                Life l = new Life();
                l.title = form["title"];
                //kno.admid = Convert.ToInt32(Session["admid"]);
                l.admid = Convert.ToInt32(Session["admid"]);
                l.cover = form["cover"];
                l.lifecontent = form["lifecontent"];
                l.addtime = DateTime.Now;
                l.state = Convert.ToInt32(form["state"]);
                l.typeid = Convert.ToInt32(form["typename"]);
                string msg = lifebll.AddEntity(l);
                if (msg == "success")
                {
                    return Content("suc");
                }
                else
                {
                    return Content("fail");
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                return Content(dbEx.Message);
            }
        }

        #endregion

        #region 上传环保生活方式文章封面
        public JsonResult UpLifeImg()
        {
            string src = "";
            HttpPostedFileBase file = Request.Files[0];
            string savefilePath = "";
            //判断文件是否为空
            if (file != null)
            {
                //获取文件类型
                string fileExtension = Path.GetExtension(file.FileName);
                //自定义文件名（时间+唯一标识符+后缀）
                string fileName = DateTime.Now.ToString("yyyy-MM-dd") + Guid.NewGuid() + fileExtension;
                //判断是否存在需要的目录，不存在则创建 
                if (!Directory.Exists(Server.MapPath("~/images/lifes")))
                {
                    Directory.CreateDirectory(Server.MapPath("~/images/lifes"));
                }
                //拼接保存文件的详细路径
                savefilePath = Server.MapPath("~/images/lifes/") + fileName;
                //若扩展名不为空则判断文件是否是指定图片类型
                if (fileExtension != null)
                {
                    if ("(.bmp)|(.png)|(.jpg)|(.gif)|(.jpeg)|(.BMP)|(.PNG)|(.JPG)|(.GIF)|(.JPEG)".Contains(fileExtension))
                    {
                        //拼接返回的Img标签
                        src = "../../images/lifes/" + fileName;
                    }
                    file.SaveAs(savefilePath);
                    return Json(new { code = 200, url = src }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new { code = 1000, url = src }, JsonRequestBehavior.AllowGet);
                }
            }
            else
            {
                src = null;
                return Json(new { code = 1000, url = src }, JsonRequestBehavior.AllowGet);
            }

        }
        #endregion

        #region 系统自带
        // GET: Adm
        public ActionResult Index()
        {
            return View(db.Administrator.ToList());
        }

        // GET: Adm/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Administrator administrator = db.Administrator.Find(id);
            if (administrator == null)
            {
                return HttpNotFound();
            }
            return View(administrator);
        }

        // GET: Adm/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Adm/Create
        // 为了防止“过多发布”攻击，请启用要绑定到的特定属性；有关
        // 更多详细信息，请参阅 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Admid,Password")] Administrator administrator)
        {
            if (ModelState.IsValid)
            {
                db.Administrator.Add(administrator);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(administrator);
        }

        // GET: Adm/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Administrator administrator = db.Administrator.Find(id);
            if (administrator == null)
            {
                return HttpNotFound();
            }
            return View(administrator);
        }

        // POST: Adm/Edit/5
        // 为了防止“过多发布”攻击，请启用要绑定到的特定属性；有关
        // 更多详细信息，请参阅 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Admid,Password")] Administrator administrator)
        {
            if (ModelState.IsValid)
            {
                db.Entry(administrator).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(administrator);
        }

        // GET: Adm/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Administrator administrator = db.Administrator.Find(id);
            if (administrator == null)
            {
                return HttpNotFound();
            }
            return View(administrator);
        }

        // POST: Adm/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Administrator administrator = db.Administrator.Find(id);
            db.Administrator.Remove(administrator);
            db.SaveChanges();
            return RedirectToAction("Index");
        }


        #endregion

        #region 获取Json用户集
        public JsonResult GetUsersJson()
        {
            List<Users> users = db.Users.ToList();
            int Count = users.Count;
            return Json(new { code = 0, count = Count, data = users, msg = "" }, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region 系统用户信息分页数据接口
        public JsonResult SystemUsers(int page, int limit)
        {
            db.Configuration.ProxyCreationEnabled = false;
            List<Users> users = db.Users.ToList();
            var list = users.Where(o => true).Skip((page - 1) * limit).Take(limit).ToList();
            int Count = users.Count;
            return Json(new { code = 0, count = Count, data = list, msg = "" }, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region 查询注册用户
        public JsonResult SearchUser(int page, int limit, int id)
        {
            db.Configuration.ProxyCreationEnabled = false;
            Users user = userbll.LoadEntity(id);
            List<Users> list = new List<Users>();
            list.Add(user);
            int Count = list.Count();
            return Json(new { code = 0, count = Count, data = list, msg = "" }, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region JSon获取帖子
        public JsonResult GetPostsJson()
        {
            db.Configuration.ProxyCreationEnabled = false;
            List<Post> posts = db.Post.ToList();
            int Count = posts.Count;
            return Json(new { code = 0, count = Count, data = posts, msg = "" }, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region 系统在线帖子库
        public ActionResult Posts()
        {
            if (Request.IsAjaxRequest())
            {
                return PartialView();
            }
            else
            {
                return View();
            }
        }
        #endregion  

        #region 系统用户帖子库数据接口
        public JsonResult SystemPosts(int page, int limit)
        {
            db.Configuration.ProxyCreationEnabled = false;
            List<Post> post = db.Post.ToList();
            var list = post.Where(o => o.poststate == 1).Skip((page - 1) * limit).Take(limit).ToList();
            int Count = post.Count;
            return Json(new { code = 0, count = Count, data = list, msg = "" }, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region 系统帖子评论库
        public ActionResult PostComments()
        {
                if (Request.IsAjaxRequest())
                {
                    return PartialView();
                }
                else
                {
                    return View();
                }
        }
        #endregion

        #region 系统帖子评论库数据接口
        public JsonResult SystemPostComments(int page, int limit)
        {
            db.Configuration.ProxyCreationEnabled = false;
            List<PostComment> postcomment = db.PostComment.ToList();
            var list = postcomment.Skip((page - 1) * limit).Take(limit).ToList();
            int Count = postcomment.Count;
            return Json(new { code = 0, count = Count, data = list, msg = "" }, JsonRequestBehavior.AllowGet);
        }
        #endregion
    }
}
