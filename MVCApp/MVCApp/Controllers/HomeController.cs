﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
//using WebApp.Filters;
//using WebApp.Helper;
//using DomainModel.Providers;
//using DomainModel.Models;
using System.Xml.XPath;
using System.Xml;
//using DomainModel
using System.Web.Services;
using System.Web.Script.Services;
using MVCApp.Filters;
using Model;
using MVCApp.DomainModel.Providers;
using MVCApp.DomainModel;
using MVCApp.Helpers;
using MVCApp.Helpers;

namespace MVCApp.Controllers
{
    public class HomeController : Controller
    {
        private static int markOfSynchronization = 0;
        private static FilterChain ftChain = FilterManager.instance().CreateChain();
        private static UserData userData = null;

        public ActionResult Index(int? page, string selectedUsers, string diselectedUsers)
        {
            markOfSynchronization = 0;
            UserProvider provider = new UserProvider(new databaseDataContext(ConnectionProvider.ConnectionString));
            userData = new UserData();
            ftChain.addFilter(new UserCheckerFilter());
            string html = XSLTConverter.Transform(Server.MapPath(Url.Content("~/App_Data/Index.xml")), Server.MapPath(Url.Content("~/XSLTConverters/Pages.xsl")));
            return Content(html);
        }


        public ActionResult FirstPage()
        {
            string html = XSLTConverter.Transform(Server.MapPath(Url.Content("~/App_Data/FirstPage.xml")), Server.MapPath(Url.Content("~/XSLTConverters/Pages.xsl")));
            return Content(html);
        }

        public ActionResult SecondPage()
        {
            string html = XSLTConverter.Transform(Server.MapPath(Url.Content("~/App_Data/SecondPage.xml")), Server.MapPath(Url.Content("~/XSLTConverters/Pages.xsl")));
            return Content(html);
        }

        public ActionResult Congratulations(string result)
        {
            string html = XSLTConverter.Transform(Server.MapPath(Url.Content("~/App_Data/Congrat.xml")), Server.MapPath(Url.Content("~/XSLTConverters/Pages.xsl")));
            return Content(html);
        }

        [HttpPost]
        [ActionName("SecondPage")]
        public ActionResult PostFirstPage(string name, string surname)
        {
            if ( ++markOfSynchronization > 1)
                return RedirectToAction("Error");
            userData.UserName = name;
            userData.UserSurname = surname;
            return RedirectToAction("SecondPage");
        }

        [HttpPost]
        [ActionName("Congratulations")]
        public ActionResult PostSecondPage(string FavoriteWord, string Phone)
        {
            if ( ++markOfSynchronization > 2)
                return RedirectToAction("Index");
            userData.SecWord = FavoriteWord;
            userData.UserPhone = Phone;
            userData.UserID = -1;
            int res = -1;
            ftChain.StartFiltering(userData, ref res);
            string result = "";
            if (res == -1)
                result = "Everything is OK";
            else
                result = "Such User Exists";
            return RedirectToAction("Congratulations");
        }

        public ActionResult Error()
        {
            userData = null;
            string html = XSLTConverter.Transform(Server.MapPath(Url.Content("~/App_Data/Error.xml")), Server.MapPath(Url.Content("~/XSLTConverters/Pages.xsl")));
            return Content(html);
        }


        [NonAction]
        private int[] ResolveSelections(string selectedUsers, string diselectedUsers, ref int[] selected)
        {
            int[] diselected = null;
            if (!String.IsNullOrEmpty(selectedUsers) || !String.IsNullOrEmpty(diselectedUsers))
            {
                selected = selectedUsers.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries).Select(item => Int32.Parse(item)).ToArray();
                diselected = diselectedUsers.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries).Select(item => Int32.Parse(item)).ToArray();
                if (Session["selectedUsers"] != null)
                {
                    int[] saved = (int[])Session["selectedUsers"];
                    selected = saved.Union(selected).Except(diselected).ToArray();
                }
                Session["selectedUsers"] = selected;
            }
            return diselected;
        }

    }
}
