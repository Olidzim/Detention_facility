using Detention_facility.Models;
using System.Collections.Generic;
using Detention_facility.Custom;

namespace Detention_facility.Business
{
    public interface IAccountService
    {
        void RegisterUser(User user);
        void UpdateUser(int id, User user);
        void UpdateUserPassword(int id, string password);
        User GetUserByID(int id);
        void DeleteUser(int id);
        string GetRole(string login, string password);
        List<User> GetUsers();
    }
}
