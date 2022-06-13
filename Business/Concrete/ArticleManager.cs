using Business.Abstract;
using Business.Constants;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entity.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class ArticleManager : IArticleService
    {
        private IArticleDal _articleDal;

        public ArticleManager(IArticleDal articleDal)
        {
            _articleDal = articleDal;
        }

        public IDataResult<List<Article>> GetListArticle()
        {
            try
            {
                var result = _articleDal.GetListArticle();
                return new SuccessDataResult<List<Article>>(result, Messages.SuccessfulListing);
            }
            catch (Exception e)
            {
                return new ErrorDataResult<List<Article>>(e.Message);
            }
        }
        public IDataResult<bool> AddArticle(Article article)
        {
            try
            {
                var result = _articleDal.AddArticle(article);
                return new SuccessDataResult<bool>(result, Messages.SuccessfulAdding);
            }
            catch (Exception e)
            {
                return new ErrorDataResult<bool>(e.Message);
            }
        }

    }
}
