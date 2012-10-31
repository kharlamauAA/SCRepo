﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml.Xsl;
using System.Xml.XPath;
using System.Xml;
using System.IO;

namespace MVCApp.Helpers
{

    public static class XSLTConverter
    {
        public static string Transform(string model, string xstlModel)
        {
            XslTransform xslt = new XslTransform();
            xslt.Load(xstlModel);
            XPathDocument xpathdocument = new XPathDocument(model);

            StringWriter sr = new StringWriter();

            xslt.Transform(xpathdocument, null, sr);
            return sr.ToString();
        }

        public static string TransformFromMemory(XmlDocument xml, string xstlModel)
        {
            XslTransform xslt = new XslTransform();
            xslt.Load(xstlModel);

            StringWriter sr = new StringWriter();

            xslt.Transform(xml, null, sr);
            return sr.ToString();
        }

    }

}