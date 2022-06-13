using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Abstract
{
    public interface ILoginDal
    {
        bool CheckLogin(string email, string password);
    }
}
