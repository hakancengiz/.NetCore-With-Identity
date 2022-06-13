using Core.Utilities.Results;
using Entity.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface IArticleService
    {
        IDataResult<List<Article>> GetListArticle();
        IDataResult<bool> AddArticle(Article article);
    }
}
