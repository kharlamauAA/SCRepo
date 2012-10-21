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

namespace MVCApp.Controllers
{
    public class HomeController : Controller
    {
        private static int markOfSynchronization = 0;

        public ActionResult Index(int? page, string selectedUsers, string diselectedUsers)
        {
            markOfSynchronization = 0;
            return View();
          // // int pageNumber = page.HasValue ? page.Value : 1;
          ////  using (UserProvider provider = new UserProvider(new databaseDataContext(ConnectionProvider.ConnectionString)))
          //  {
          //    //  UnitOfWork uof = new UnitOfWork(provider);
          //      //var users = provider.GetUsers(); 
          //      //var users = uof.GetUsers();
          //      //if (users.Count() == 0)
          //      {
          //          
          //      }
          //      string url = "http://" + Request.Url.Authority + "/Home/Index";
          //     // Paging pagingModel = PagingHelper.GetPagingModel(pageNumber, users.Count(), this.usersPerPage, url);
          //      //ValueListIterator iterator = new ValueListIterator(provider, usersPerPage);
          //     // List<User> pagedUsers = iterator.GetCurrentElement(pageNumber);
          //      int[] selected = null;
          //      int[] diselected;
          //      diselected = ResolveSelections(selectedUsers, diselectedUsers, ref selected);
          //     // UrlOptions options = new UrlOptions()
          //      //{
          //      //    UpdateUrl = String.Format("http://{0}/Home/UpdateRecord", Request.Url.Authority)
          //      //};
          //     // SetChecked(pagedUsers, (int[])Session["selectedUsers"]);
          //     // XmlDocument xml = XMLUserWriter.WriteXML(pagedUsers, pagingModel, options);
          //     //// string html = XSLTConverter.TransformFromMemory(xml, Server.MapPath(Url.Content("~/Converters/paging.xsl")));
          //     // return Content(html);
          //  }
        }


        public ActionResult FirstPage(int? page, string selectedUsers, string diselectedUsers)
        {
            if (markOfSynchronization > 0)
                return Redirect("Error");
            else
            {
                markOfSynchronization = 1;
                return View();
            }
        }

        public ActionResult SecondPage(int? page, string selectedUsers, string diselectedUsers)
        {
            if (markOfSynchronization > 1)
                return Redirect("Error");
            else
            {
                markOfSynchronization = 2;
                return View();
            }
        }

        [HttpPost]
        [ActionName("SecondPage")]
        public ActionResult PostFirstPage(string name, string surname)
        {
            //using (UserProvider provider = new UserProvider(new databaseDataContext(ConnectionProvider.ConnectionString)))
            {

                return RedirectToAction("Index");
            }
        }

        public ActionResult Error()
        {
            return View();
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
        //[NonAction]
        //private void SetChecked(List<User> list, int[] selected)
        //{
        //    if (selected != null)
        //    {
        //        foreach (User user in list)
        //        {
        //            if (selected.Contains(user.UserID))
        //            {
        //                user.Checked = true;
        //            }
        //        }
        //    }
        //}

        //[HttpGet]
        //public ActionResult Step1()
        //{
        //    string html = XSLTConverter.Transform(Server.MapPath(Url.Content("~/App_Data/step1.xml")), Server.MapPath(Url.Content("~/Converters/index.xsl")));
        //    return Content(html);
        //}
        //[HttpPost]
        //[ActionName("Step1")]
        //public ActionResult PostStep1(string name, string surname)
        //{
        //    DomainModel.Models.User user = new DomainModel.Models.User()
        //    {
        //        Name = name,
        //        Surname = surname
        //    };
        //    TempData["Model"] = user;
        //    return RedirectToAction("Step2");
        //}

        //[HttpGet]
        //public ActionResult Step2()
        //{
        //    string html = XSLTConverter.Transform(Server.MapPath(Url.Content("~/App_Data/step2.xml")), Server.MapPath(Url.Content("~/Converters/index.xsl")));
        //    return Content(html);
        //}
        

        //[HttpPost]
        //public ActionResult DeleteRecord(int? page, string selectedUsers, string diselectedUsers)
        //{
        //    using (UserProvider provider = new UserProvider(new databaseDataContext(ConnectionProvider.ConnectionString)))
        //    {
        //        UnitOfWork uof = new UnitOfWork(provider);
        //        int[] selected = null;
        //        int[] diselected;
        //        if (!String.IsNullOrEmpty(selectedUsers) || !String.IsNullOrEmpty(diselectedUsers))
        //        {
        //            selected = selectedUsers.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries).Select(item => Int32.Parse(item)).ToArray();
        //            diselected = diselectedUsers.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries).Select(item => Int32.Parse(item)).ToArray();
        //            if (Session["selectedUsers"] != null)
        //            {
        //                int[] saved = (int[])Session["selectedUsers"];
        //                selected = saved.Union(selected).Except(diselected).ToArray();
        //            }
        //            foreach (var id in selected)
        //            {
        //                //provider.DeleteUser(id);
        //                uof.DeleteUser(id);
        //            }
        //        }
        //        //provider.Save();
        //        uof.Commit();
        //        return RedirectToAction("Index");
        //    }
        //}

        //public void UpdateRecord(User user)
        //{
        //    using (UserProvider provider = new UserProvider(new databaseDataContext(ConnectionProvider.ConnectionString)))
        //    {
        //        UnitOfWork uof = new UnitOfWork(provider);
        //        //provider.UpdateUser(user);
        //        //provider.Save();
        //        uof.UpdateUser(user);
        //        uof.Commit();
        //    }
        //}

        //public ActionResult Error()
        //{
        //    string html = XSLTConverter.Transform(Server.MapPath(Url.Content("~/App_Data/error.xml")), Server.MapPath(Url.Content("~/Converters/index.xsl")));
        //    return Content(html);
        //}

    }
}
