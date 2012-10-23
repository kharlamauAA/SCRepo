using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;

namespace MVCApp.DomainModel.Providers
{
    public class ConnectionProvider
    {
        private static readonly string connectionString = ConfigurationManager.ConnectionStrings["userEntities"].ConnectionString;

        public static string ConnectionString
        {
            get
            {
                return connectionString;
            }
        }
    }
}