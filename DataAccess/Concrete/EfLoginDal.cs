using DataAccess.Abstract;
using DataAccess.Concrete.EntityFrameworkCore.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Concrete
{
    public class EfLoginDal : ILoginDal
    {
        public bool CheckLogin(string email, string password)
        {
            bool result = false;
            using (var context = new ProjectContext())
            {
                //result = context.Users.Any(u => u.Email == email && u.Password == password && u.IsActive);
            }

            return result;
        }
    }
}
