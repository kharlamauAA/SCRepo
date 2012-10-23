using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Model;

namespace MVCApp.DomainModel.Extentions
{
    public static class UserDataExtensions
    {
        public static UserData ConvertToUser(this User data)
        {
            UserData result = new UserData()
            {
                UserID = data.UserID,
                UserName = data.UserName,
                UserSurname = data.UserSurname,
                SecWord = data.SecWord,
                UserPhone = data.UserPhone
            };
            return result;
        }
    }
}