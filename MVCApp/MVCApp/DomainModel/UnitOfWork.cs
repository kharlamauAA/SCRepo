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
        private List<UserData> newObjects;
        private List<UserData> dirtyObjects;
        private List<int> removedObjects;

        private IUserRepository repository;
        private Dictionary<int, UserData> originals;


        public UnitOfWork(IUserRepository repository)
        {
            newObjects = new List<UserData>();
            dirtyObjects = new List<UserData>();
            removedObjects = new List<int>();
            originals = new Dictionary<int, UserData>();
            this.repository = repository;
        }

        public void RegisterNew(UserData user)
        {
            if (user != null && !newObjects.Contains(user))
            {
                newObjects.Add(user);
            }
        }

        public void RegisterDeleted(UserData user)
        {
            if (user != null && !removedObjects.Contains(user.UserID))
            {
                if (newObjects.Contains(user))
                {
                    newObjects.Remove(user);
                }
                else if (dirtyObjects.Contains(user))
                {
                    dirtyObjects.Remove(user);
                    removedObjects.Add(user.UserID);
                }
                else
                {
                    removedObjects.Add(user.UserID);
                }
            }
        }

        public void RegisterChanged(UserData user)
        {
            if (user != null)
            {
                UserData newUser = newObjects.Single(x => x.UserID == user.UserID);
                if (newUser != null)
                {
                    newObjects.Remove(newUser);
                    newObjects.Add(user);
                }
                else
                {
                    dirtyObjects.Add(user);
                }
            }
        }

        public void Commit()
        {
            foreach (UserData user in newObjects)
            {
                repository.CreateUser(user);
            }
            foreach (int userId in removedObjects)
            {
                repository.DeleteUser(userId);
            }
            foreach (UserData user in dirtyObjects)
            {
                repository.UpdateUser(user);
            }
            repository.Save();
            Rollback();
        }

        public void Rollback()
        {
            newObjects.Clear();
            dirtyObjects.Clear();
            removedObjects.Clear();
        }

        public IEnumerable<UserData> GetUsers()
        {
            IEnumerable<UserData> users = repository.GetUsers();
            foreach (UserData user in users)
            {
                if (!originals.ContainsKey(user.UserID))
                {
                    originals.Add(user.UserID, user);
                }
            }
            return users;
        }

        public UserData GetUserById(int userId)
        {
            UserData user = repository.GetUserById(userId);
            if (!originals.ContainsKey(user.UserID))
            {
                originals.Add(user.UserID, user);
            }
            return user;
        }

        public void CreateUser(UserData user)
        {
            newObjects.Add(user);
        }

        public void DeleteUser(int userId)
        {
            removedObjects.Add(userId);
        }

        public void UpdateUser(UserData user)
        {
            dirtyObjects.Add(user);
        }

        public void Save()
        {
            Commit();
        }

        public void Dispose()
        {

        }
    }
}