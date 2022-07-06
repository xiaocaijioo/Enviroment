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
using System.Web.Helpers;
using System.Web.Mvc;
using BLL;
using Model;
using X.PagedList;

namespace ProtectUI.Controllers
{
    public class PostController : Controller
    {
        private EnvironmentProtectEntities db = new EnvironmentProtectEntities();
        PostBLL postbll = new PostBLL();
        UsersBLL usersbll = new UsersBLL();
        PostCommentBLL postcommentbll = new PostCommentBLL();
        
        // GET: Posts,使用MvcPager控件分页显示
        public ActionResult Index(int pageIndex=1)
        {
            const int pageSize = 15;
            var post = db.Post.OrderByDescending(u => u.addtime).ToPagedList(pageIndex, pageSize);
            //var post = db.Post.Include(p => p.Users);
            //return View(post.ToList());
            return View(post);
        }
        #region 发帖 
        [HttpGet]

        public ActionResult PublishPost()
        {
            
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult PublishPost(FormCollection forms, Post post)
        {
            if (Session["userid"] != null)
            {
                if (ModelState.IsValid)
                {
                    try
                    {
                        post.userid =Convert.ToInt32(Session["userid"]);
                        post.addtime = DateTime.Now;
                        post.likenum = 0;
                        post.commentnum = 0;
                        post.poststate = 1;
                        string ap = postbll.AddEntity(post);
                        if (ap == "success")
                        {
                            return Content("success");
                            //return View();
                        }
                        else
                        {
                            return Content("fail");
                        }
                        //return View();
                    }
                    catch (DbEntityValidationException dbEx)
                    {
                        return Content(dbEx.Message);
                    }
                }
                else
                {
                    return View();
                }
            }
            else
            {
                return Content("<script>alert('您还未登录');location='../../Users/Login'</script>");
            }
        }

        #endregion

        #region 帖子详情
        public ActionResult Details(int id)
        {
            var postdetail = postbll.LoadEntity(id);
            return View(postdetail);
        }
        #endregion

        #region 评论展示

        public ActionResult PostComment(int? id)
        {
            if (Session["userid"] != null)
            {
                IEnumerable<PostComment> comment = null;
                comment = db.PostComment.Where(u => u.postid == id).OrderByDescending(u => u.replytime).ToList();
                return PartialView(comment);
            }
            else
            {
                return Content("<script>alert('您还未登录');location='../../Users/Login'</script>");
             }

}
        #endregion

        #region 发布帖子评论
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult AddPostComment(int postid, string replycontent)
        {
            if (ModelState.IsValid)
            {
                try { 
                    PostComment c = new PostComment();
                    //c.userid = Convert.ToInt32(Session["userid"]);
                    c.postid = postid;
                    c.userid = Convert.ToInt32(Session["userid"]);
                    c.replycontent = replycontent;
                    c.replytime = DateTime.Now;
                    c.praisenum = 0;
                    string pc=postcommentbll.AddEntity(c);
                    if (pc == "success")
                    {
                        var com=db.PostComment.Where(u => u.postid == postid).OrderByDescending(u => u.replytime).ToList();
                        return PartialView("PostComment",com);
                    }
                    else { 
                        return Content("Fail");
                    }
                }
                catch (DbEntityValidationException dbEx)
                {
                    return Content(dbEx.Message);
                }
            }
            else return Content("Login");
        }
        #endregion

        #region 富文本编辑器器中上传图片
        public ActionResult UploadPic()
        {
            HttpPostedFileBase imageName = Request.Files[0];
            try
            {
                //返回后缀。GetExtension    imageName.FileName获取文件的全名  判断是否为图片
                string fileExtension = Path.GetExtension(imageName.FileName);

                if ("(.gif)|(.jpg)|(.bmp)|(.jpeg)|(.png)".Contains(fileExtension))
                {
                    //检查目录是否存在,不存在就创建文件夹Server.MapPath
                    if (!Directory.Exists(Server.MapPath("~/Document/Img/")))
                    {
                        //在项目所在的位置添加它的子目录
                        Directory.CreateDirectory(Server.MapPath("~/Document/Img/"));
                    }

                    //拼接文件名====当前日期+全球唯一标识符+后缀（目的是区分上传的图片）+Guid. NewGuid随机数 + 文件后缀
                    string fileName = DateTime.Now.ToString("yyyy-MM-dd") + "-" + Guid.NewGuid() + fileExtension;
                    //获取物理路径===也就是绝对路径
                    string filePath = Server.MapPath("~/Document/Img/") + fileName;
                    //把文件保存到物理路径当中

                    imageName.SaveAs(filePath);

                    var Url = "/Document/Img/" + fileName;// 设置数据库保存的路径

                    var CkeditorFuncNum = System.Web.HttpContext.Current.Request["CKEditorFuncNum"];
                    // 上传成功后，我们还需要通过以下的一个脚本把图片返回到ckeditor第一个 tab(图像信息) 选项页
                    return Content("<script>window.parent.CKEDITOR.tools.callFunction(" + CkeditorFuncNum + ", \""
                        + Url + "\");</script>");
                }
                else
                {
                    //判断后缀发现不是图片，提示用户
                    return Content("<script>alert('请上传正确格式的图片', { icon: 0, title: '提示' });history.go(-1);</script>");
                }
            }
            catch (Exception)
            {
                return Content("<script>alert('图片未能正常上传！');history.go(-1);</script>");
            }

        }
        #endregion
    }
}
