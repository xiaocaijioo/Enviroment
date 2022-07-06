using DAL;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class PostCommentBLL : BaseBLL<PostComment>
    {
        public override void SetDal()
        {
            dal = factory.CreatePostCommentDal();
            /*dal = factory.UsersDal; */     //工厂方法创建类的实例
            /* dal = new UsersDAL(); */       //每次都new一个很麻烦，所以使用工厂方法创建DAL实例
        }

        public PostComment LoadEntity(int id)
        {
            PostCommentDAL postcommentDAL = new PostCommentDAL();
            return postcommentDAL.LoadEntity(id);
        }
    }
}
