using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using BLL;
using Model;

namespace ProtectUI.Controllers
{
    public class UsersController : Controller
    {
        private UsersBLL bll = new UsersBLL();
        private PostlikeBLL postlikebll = new PostlikeBLL();

        // GET: Users
        public ActionResult Index()
        {
            return View(bll.LoadEntities(u => true));
        }

        #region 用户详情
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Users users = bll.LoadEntity((int)id);
            if (users == null)
            {
                return HttpNotFound();
            }
            return View(users);
        }
        #endregion

        #region 用户注册
        public ActionResult Create()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Account,Password,Username")] Users users)
        {
            if (ModelState.IsValid)
            {
                string a = bll.AddEntity(users);
                if (a == "success")
                {
                    return RedirectToAction("Index");
                }

            }

            return View(users);
        }
        #endregion

        #region 编辑
        // GET: Users/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Users users = bll.LoadEntity((int)id);
            if (users == null)
            {
                return HttpNotFound();
            }
            return View(users);
        }

        // POST: Users/Edit/5
        // 为了防止“过多发布”攻击，请启用要绑定到的特定属性；有关
        // 更多详细信息，请参阅 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Account,Password,Username")] Users users)
        {
            if (ModelState.IsValid)
            {
                bll.EditEntity(users);

                return RedirectToAction("Index");
            }
            return View(users);
        }
        #endregion

        #region 注销登录
        public string LogOut()
        {
            Session["userid"] = null;
            Session["username"] = null;
            string data = "已注销登录！";
            return data;
        }

        #endregion

        #region 删除
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Users users = bll.LoadEntity((int)id);
            if (users == null)
            {
                return HttpNotFound();
            }
            return View(users);
        }

        // POST: Users/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Users users = bll.LoadEntity((int)id);
            bll.DeleteEntity(users);
            return RedirectToAction("Index");
        }
        #endregion

        #region 调用验证码类
        //调用验证码生成
        public ActionResult GetRandomCode()
        {
            Captcha captcha = new Captcha();
            string code = captcha.CreateRandomCode(4);
            Session["validateCode"] = code;
            return File(captcha.CreateValidGraphic(code), "image/jpeg");
        }

        #endregion

        #region 用户登录
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]

        public ActionResult Login(Users users, string validationCode)
        {
            SqlDataReader reader = bll.UsersLogin(users);

            string result = reader.HasRows ? "成功" : "失败";
            if (Session["validateCode"].ToString() == validationCode)
            {
                if (result == "成功")
                {
                    Users user = bll.LoadEntities(b => b.Account == users.Account).FirstOrDefault();
                    Session["userid"] = user.Id;
                    Session["username"] = user.Username;
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    return Content("<script>alert('账户或密码输入错误！', { icon: 0, title: '提示' });history.go(-1);</script>");
                }
                
            }
            else
            {
                return Content("<script>alert('验证输入错错误', { icon: 0, title: '提示' });history.go(-1);</script>");
            }
            #endregion




            //#region 点赞帖子

            //public ActionResult Praise(int postid)
            //{
            //    if (Session["userid"]!=null)
            //    {
            //        var userid = Convert.ToInt32(Session["userid"]);
            //        Postlike like = new Postlike();
            //        like.userid = userid;
            //        like.postid = postid;
            //        string addlike = postlikebll.AddEntity(like);
            //        if (addlike == "success")
            //        {
            //            return Content("success");
            //        }
            //        else
            //            return Content("fail");
            //    }
            //    else
            //    {
            //        return Content("<script>alert('您还没有登录哦！');history.go(-1);</script>");
            //    }
            //}
            //#endregion

            //[HttpPost]
            //public ActionResult Register(FormCollection form)
            //{
            //    if (ModelState.IsValid)
            //    {
            //        string account = form["account"];
            //        string password = form["password"];
            //        string email = form["email"];

            //        string code = Guid.NewGuid().ToString();

            //        string body="<a href='https://>"+Request.Url.Host+":"+Request.Url.Port+
            //            ""
            //    }
            //    return View();
            //}

        }
    }
}
