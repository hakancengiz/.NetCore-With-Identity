using Business.Abstract;
using Business.Constants;
using Core.Utilities.Results;
using DataAccess.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class LoginManager : ILoginService
    {
        private ILoginDal _loginDal;

        public LoginManager(ILoginDal loginDal)
        {
            _loginDal = loginDal;
        }

        public IDataResult<bool> CheckLogin(string email, string password)
        {
            try
            {
                var result = _loginDal.CheckLogin(email, password);
                if (result == true)
                {
                    return new SuccessDataResult<bool>(result, Messages.SuccessfulLogin);
                }
                else
                {
                    return new ErrorDataResult<bool>(result, Messages.UserNotFound);
                }

                
            }
            catch (Exception e)
            {
                return new ErrorDataResult<bool>(e.Message);
            }
        }
    }
}
