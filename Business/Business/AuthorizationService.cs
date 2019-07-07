using Detention_facility.Data;
using Detention_facility.Models;

namespace Detention_facility.Business
{
    public class AuthorizationService : IAuthorizationService
    {
        private IUserDataAccessLayer _userDataProvider;

        public AuthorizationService(IUserDataAccessLayer userDataProvider)
        {
            _userDataProvider = userDataProvider;
        }
        
        public User CheckUser(string login, string password)
        {
            User user = _userDataProvider.CheckUser(login, password);
            return user;
        }


    }
}
