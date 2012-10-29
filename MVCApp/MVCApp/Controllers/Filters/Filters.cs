using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Model;
using System.Data.SqlClient;
using MVCApp.DomainModel.Providers;
using MVCApp.DomainModel;


namespace MVCApp.Filters
{

    public interface Filter
    {
        bool execute(Object data);
    }

    public class UserCheckerFilter : Filter
    {
        private int getUserID(Object data)
        {
            int id = 0;
            using (UserProvider provider = new UserProvider(new databaseDataContext(ConnectionProvider.ConnectionString)))
            {
                UnitOfWork uof = new UnitOfWork(provider);
                //var users = provider.GetUsers(); 
                var users = uof.getNumberOfUsers() + 1;
                id = uof.getNumberOfUsers() + 1;
            }
            return id;
        }

        public bool execute(Object data)
        {
            UserData user = (UserData)data;

            using (UserProvider provider = new UserProvider(new databaseDataContext(ConnectionProvider.ConnectionString)))
            {
                UnitOfWork uof = new UnitOfWork(provider);
                if (user.UserID == -1)
                    user.UserID = getUserID(user);
                uof.Insert(user);
                uof.Commit();
            }

            return true;
        }
    }

    public class FilterChain
    {

        private List<Filter> chain = new List<Filter>();

        public void addFilter(Filter ft)
        {
            chain.Add(ft);
        }

        public void StartFiltering(Object userData, ref int result)
        {
            int index = 0;
            bool flag = true;
            foreach (Filter ft in chain)
            {
                if (!ft.execute(userData))
                {
                    flag = false;
                    break;
                }
                else ++index;
            }
            if (!flag)
                result = ++index;
        }
    }


    public class FilterManager
    {

        private static FilterManager ftManager = null;

        private FilterManager()
        {
        }

        public static FilterManager instance()
        {
            if (ftManager == null)
                ftManager = new FilterManager();
            return ftManager;
        }

        public FilterChain CreateChain()
        {
            return new FilterChain();
        }

    }
}