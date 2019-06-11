using Detention_facility.Models;

namespace Detention_facility.Data
{
    public interface IUserDataAccessLayer
    {
        void InsertDetention(User user);
        User FindUser(string Login, string password);
    }
}
