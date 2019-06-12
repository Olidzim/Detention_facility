using Detention_facility.Models;

namespace Detention_facility.Data
{
    public interface IUserDataAccessLayer
    {
        void InsertUser(User user);
        User CheckUser(string Login, string password);
        void UpdateUser(int id, User user);
        void UpdateUserPassword(int id, string password);
        void DeleteUser(int id);
        User GetUserByID (int id);
    }
}
