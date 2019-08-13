using Detention_facility.Data;
using Detention_facility.Models;
using System;
using System.Collections.Generic;

namespace Detention_facility.Business
{
    public class AccountService : IAccountService
    {
        private IUserDataAccessLayer _userDataProvider;

        public AccountService(IUserDataAccessLayer userDataProvider)
        {
            _userDataProvider = userDataProvider;
        }

        public void DeleteUser(int id)
        {
            _userDataProvider.DeleteUser(id);
        }

        public User GetUserByID(int id)
        {
           return _userDataProvider.GetUserByID(id);
        }

        public void RegisterUser(User user)
        {
            _userDataProvider.InsertUser(user);
        }

        public void UpdateUser(int id, User user)
        {
            _userDataProvider.UpdateUser(id, user);
        }

        public void UpdateUserPassword(int id, string password)
        {
            _userDataProvider.UpdateUserPassword(id, password);
        }
        public string GetRole(string login, string password)
        {
            string role = _userDataProvider.GetRole(login, password);
            return role;
        }

        public List<User> GetUsers()
        {
            return _userDataProvider.GetUsers();
        }
    }
}
