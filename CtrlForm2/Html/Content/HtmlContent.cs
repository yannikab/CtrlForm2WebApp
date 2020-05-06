using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using CtrlForm2.Html.Content.Elements;

namespace CtrlForm2.Html.Content
{
    public abstract class HtmlContent
    {
        #region Fields

        protected HtmlContainer container;

        #endregion


        #region Properties

        public HtmlContainer Container
        {
            get { return container; }
            set { container = value; }
        }

        #endregion
    }
}
