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

        public void RegisterUser(User user)
        {
            _userDataProvider.InsertDetention(user);
        }

        public User FindUser(string login, string password)
        {
            User user = _userDataProvider.FindUser(login, password);
            return user;
        }
    }
}
