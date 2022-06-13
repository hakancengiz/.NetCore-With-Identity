using DataAccess.Abstract;
using DataAccess.Concrete.EntityFrameworkCore.Context;
using Entity.Concrete;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Concrete.EntityFrameworkCore
{
    public class EfArticleDal : Core.DataAccess.EntityFramework.EfEntityRepositoryBase<Article, ProjectContext>, IArticleDal
    {

        public List<Article> GetListArticle(Expression<Func<Article, bool>> filter = null)
        {
            using (var context = new ProjectContext())
            {
                dynamic result;
                if (filter == null)
                {
                    result = context.Articles.Where(x => x.IsActive).Include(u => u.User).Include(t => t.Tags).ToList();
                }
                else
                {
                    result = context.Articles.Where(filter).Include(u => u.User).Include(t => t.Tags).ToList();
                }

                return result;
            }
        }

        public bool AddArticle(Article article)
        {
            using (var context = new ProjectContext())
            {
                try
                {
                    context.Articles.Add(article);
                    context.SaveChanges();
                    return true;
                }
                catch (Exception e)
                {
                    return false;
                }
            }
        }
    }
}
