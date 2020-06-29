using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;

namespace Form2WebApp
{
    public static class Extensions
    {
        public static T SessionGet<T>(this UserControl userControl, string key, Func<T> create) where T : class
        {
            T item = null;

            if (!userControl.IsPostBack)
            {
                item = create();
                userControl.Session[key] = item;
            }
            else
            {
                if (userControl.Session[key] != null)
                    item = (T)userControl.Session[key];
            }

            return item;
        }
    }
}
