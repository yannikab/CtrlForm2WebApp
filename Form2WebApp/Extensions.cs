using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;

namespace Form2WebApp
{
    public static class Extensions
    {
        public static T SessionGet<T>(this Page page, string key, Func<T> create) where T : class
        {
            T item = null;

            if (!page.IsPostBack)
            {
                item = create();
                page.Session[key] = item;
            }
            else
            {
                if (page.Session[key] != null)
                    item = (T)page.Session[key];
            }

            return item;
        }
    }
}
