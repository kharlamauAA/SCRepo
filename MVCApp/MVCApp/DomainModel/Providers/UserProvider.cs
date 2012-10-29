using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;
using MVCApp.DomainModel.Interfaces;
using System.Data.Linq;
using System.Configuration;
using MVCApp.DomainModel.Extentions;
using Model;


namespace MVCApp.DomainModel.Providers
{
    public class UserProvider : IUserRepository
    {
        private static readonly string ConnectionString = ConnectionProvider.ConnectionString;
        private databaseDataContext context;

        public UserProvider(databaseDataContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException();
            }
            this.context = context;
        }

        public int GetCount()
        {
            return context.Users.Count();
        }

        public List<UserData> GetRange(int skip, int items)
        {
            return context.Users.Select(row => row.ConvertToUser()).Skip(skip).Take(items).ToList();
        }

        public IEnumerable<UserData> GetUsers()
        {
            return context.Users.Select(row => row.ConvertToUser()).ToList();
        }

        public UserData GetUserById(int userId)
        {
            return context.Users.First(x => x.UserID == userId).ConvertToUser();
        }

        public void Save()
        {
            this.context.SubmitChanges();
        }

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    context.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public void Insert(UserData user)
        {
            User data = new User()
            {
                UserID = user.UserID,
                UserName = user.UserName,
                UserSurname = user.UserSurname,
                SecWord = user.SecWord,
                UserPhone = user.UserPhone
            };
            context.Users.InsertOnSubmit(data);
        }

        public void Update(UserData user)
        {
            User data = context.Users.First(x => x.UserID == user.UserID);
            data.SecWord = user.SecWord;
            data.UserPhone = user.UserPhone;
            data.UserName = user.UserName;
            data.UserSurname = user.UserSurname;
        }

        public void Delete(int userId)
        {
            var userToDelete = context.Users.First(x => x.UserID == userId);
            context.Users.DeleteOnSubmit(userToDelete);
        }
    }
}