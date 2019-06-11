using Detention_facility.Models;

namespace Detention_facility.Business
{
    public interface IAuthorizationService
    {
        void RegisterUser(User user);
        User FindUser(string login, string password);
    }
}
