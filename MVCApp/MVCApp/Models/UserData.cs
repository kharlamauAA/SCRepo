﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Xml.XPath;
using System.Xml;
using System.Web.Services;
using System.Web.Script.Services;


namespace Model
{

    public class UserData
    {
        public int UserID { get; set; }
        public string UserName { get; set; }
        public bool Checked { get; set; }
        public string UserSurname { get; set; }
        public string SecWord { get; set; }
        public string UserPhone { get; set; }

    }

}