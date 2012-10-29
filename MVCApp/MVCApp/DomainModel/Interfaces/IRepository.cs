using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Model;

namespace MVCApp.DomainModel.Interfaces
{
    public interface IUserRepository : IDisposable
    {
        UserData GetUserById(int userId);
        void Insert(UserData user);
        void Update(UserData user);
        void Delete(int userID);
        void Save();

        IEnumerable<UserData> GetUsers();
    }
}
