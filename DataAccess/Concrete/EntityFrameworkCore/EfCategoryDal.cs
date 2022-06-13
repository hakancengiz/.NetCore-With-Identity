using DataAccess.Abstract;
using DataAccess.Concrete.EntityFrameworkCore.Context;
using Entity.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Concrete.EntityFrameworkCore
{
    public class EfCategoryDal : Core.DataAccess.EntityFramework.EfEntityRepositoryBase<Category, ProjectContext>, ICategoryDal
    {
    }
}
