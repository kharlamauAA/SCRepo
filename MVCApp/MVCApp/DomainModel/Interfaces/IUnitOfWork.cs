using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Model;

namespace MVCApp.DomainModel.Interfaces
{
    interface IUnitOfWork
    {
        void RegisterNew(UserData o);
        void RegisterDeleted(UserData o);
        void RegisterChanged(UserData o);
        void Commit();
        void Rollback();
    }
}
