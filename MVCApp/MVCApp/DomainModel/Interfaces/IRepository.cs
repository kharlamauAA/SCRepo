using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Model;

namespace MVCApp.DomainModel.Interfaces
{
    public interface IUserRepository : IDisposable
    {
        IEnumerable<UserData> GetUsers();
        UserData GetUserById(int userId);
        void CreateUser(UserData user);
        void DeleteUser(int userId);
        void UpdateUser(UserData user);
        void Save();
    }
}
