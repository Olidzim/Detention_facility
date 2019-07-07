using Detention_facility.Models;

namespace Detention_facility.Business
{
    public interface IAuthorizationService
    {        
        User CheckUser(string login, string password);

    }
}
