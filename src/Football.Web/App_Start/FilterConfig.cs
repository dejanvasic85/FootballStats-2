﻿using System.Web;
using System.Web.Mvc;
using Microsoft.Ajax.Utilities;

namespace Football.Web
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}