using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MVCApp.DomainModel.Interfaces;
using Model;

namespace MVCApp.DomainModel
{
    public class UnitOfWork : IUnitOfWork, IUserRepository
    {
        private Dictionary< UserData, int > userObjects;

        private IUserRepository repository;

        private bool getFromRepFlag = false;

        private void checkUser(UserData userNew)
        {
            int i = 0;
            foreach (UserData user in userObjects.Keys)
            {                
                if (user.UserID == userNew.UserID)
                {
                    userObjects.Remove(user);
                    i = 1;
                    break;
                }
            }
            userObjects.Add(userNew, i);
        }

        private void deleteUser(int userId)
        {
            foreach (var user in userObjects)
            {
                if (((UserData)user.Key).UserID == userId)
                {
                    userObjects.Add(user.Key, 2);
                    break;
                }
            }
        }


        public UnitOfWork(IUserRepository repository)
        {
            userObjects = new Dictionary<UserData, int>();
            this.repository = repository;
            GetUsersFromRep();
        }

        public void Insert(UserData user)
        {
            if (user != null)
                checkUser(user);
        }

        public void Update(UserData user)
        {
            if (user != null)
                checkUser(user);
        }

        public void Delete(int userId)
        {
            deleteUser(userId);
        }

        public void Commit()
        {
            foreach (var user in userObjects)
            {
                switch(user.Value)
                {
                    case 0:
                        repository.Insert((UserData)user.Key);
                        break;
                    case 1:
                        repository.Update((UserData)user.Key);
                        break;
                    case 2:
                        repository.Delete(((UserData)user.Key).UserID);
                        break;
                    default: break;
                }

            }
            repository.Save();
            Rollback();
        }

        public void Rollback()
        {
            userObjects.Clear();
        }

        private void GetUsersFromRep()
        {
            if (!getFromRepFlag)
            {
                getFromRepFlag = true;
                IEnumerable<UserData> users = repository.GetUsers();
                foreach (UserData user in users)
                {
                    userObjects.Add(user, -1);
                }
            }
        }

        public int getNumberOfUsers()
        {
            return userObjects.Count;
        }

        public UserData GetUserById(int userId)
        {
            foreach (var user in userObjects)
            {
                if (((UserData)user.Key).UserID == userId)
                    return (UserData)user.Key;
            }
            return null;
        }

        public void Save()
        {
            Commit();
        }

        public void Dispose()
        {

        }


        public IEnumerable<UserData> GetUsers()
        {
            GetUsersFromRep();
            return userObjects.Keys;
        }
    }
}