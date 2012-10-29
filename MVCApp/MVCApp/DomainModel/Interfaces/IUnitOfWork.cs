using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Model;

namespace MVCApp.DomainModel.Interfaces
{
    interface IUnitOfWork
    {
        void Insert(UserData o);
        void Delete(int userId);
        void Commit();
        void Rollback();
    }
}
