using DAL;
using IDAL;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Factory
{
    public class AbstractFactory : IAbstractFactory
    {
        public static string DalAssemblyPath = ConfigurationManager.AppSettings["DalAssemblyPath"];
        public static string DalNameSpace = ConfigurationManager.AppSettings["DalNameSpace"];
        public IUsersDAL CreateUsersDal()
        {
            string fullClassName = DalAssemblyPath + ".UsersDAL";
            UsersDAL usersDAL = (UsersDAL)Assembly.Load(DalAssemblyPath).CreateInstance(fullClassName);

            return usersDAL;
        }
        public IAdmDAL CreateAdmDal()
        {
            string fullClassName = DalAssemblyPath + ".AdmDAL";
            AdmDAL admDAL = (AdmDAL)Assembly.Load(DalAssemblyPath).CreateInstance(fullClassName);

            return admDAL;
        }
        public IPostDAL CreatePostDal()
        {
            string fullClassName = DalAssemblyPath + ".PostDAL";
            PostDAL postDAL = (PostDAL)Assembly.Load(DalAssemblyPath).CreateInstance(fullClassName);

            return postDAL;
        }
        public IKnowledgeDAL CreateKnowledgeDal()
        {
            string fullClassName = DalAssemblyPath + ".KnowledgeDAL";
            KnowledgeDAL knowledgeDAL = (KnowledgeDAL)Assembly.Load(DalAssemblyPath).CreateInstance(fullClassName);

            return knowledgeDAL;
        }
        public ILifeDAL CreateLifeDal()
        {
            string fullClassName = DalAssemblyPath + ".LifeDAL";
            LifeDAL lifeDAL = (LifeDAL)Assembly.Load(DalAssemblyPath).CreateInstance(fullClassName);

            return lifeDAL;
        }

        public ILifeTypeDAL CreateLifeTypeDal()
        {
            string fullClassName = DalAssemblyPath + ".LifeTypeDAL";
            LifeTypeDAL lifetypeDAL = (LifeTypeDAL)Assembly.Load(DalAssemblyPath).CreateInstance(fullClassName);

            return lifetypeDAL;
        }
        public IPostCommentDAL CreatePostCommentDal()
        {
            string fullClassName = DalAssemblyPath + ".PostCommentDAL";
            PostCommentDAL postcommentDAL = (PostCommentDAL)Assembly.Load(DalAssemblyPath).CreateInstance(fullClassName);

            return postcommentDAL;
        }
        public IPostlikeDAL CreatePostlikeDal()
        {
            string fullClassName = DalAssemblyPath + ".PostlikeDAL";
            PostlikeDAL postlikeDAL = (PostlikeDAL)Assembly.Load(DalAssemblyPath).CreateInstance(fullClassName);

            return postlikeDAL;
        }
    }

}
