using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.XPath;
using System.Xml;
//using DomainModel
using System.Web;
using System.Web.Mvc;
using System.Web.Services;
using System.Web.Script.Services;
using MVCApp.Filters;
using Model;
using MVCApp.DomainModel.Providers;
using MVCApp.DomainModel;
using MVCApp.Helpers;
using MVCApp.Helpers;

namespace MVCApp.Controllers.Commands
{
    interface ICommand
    {
        Object ExecuteCommand(Object o);
    }

    public class GenerateViewCommand : ICommand
    {

        private string pathXsltConveter;

        public GenerateViewCommand(string xsltConverter)
        {
            pathXsltConveter = xsltConverter;
        }

        public object ExecuteCommand(object o)
        {
            return XSLTConverter.Transform((string)o, pathXsltConveter);
        }
    }
 
}
