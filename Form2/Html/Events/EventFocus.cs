using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Form2.Html.Events
{
    public class EventFocus : HtmlEvent
    {
        #region Properties

        public override string Name
        {
            get { return "onfocus"; }
        }

        #endregion


        #region Constructors

        public EventFocus(string value)
            : base(!string.IsNullOrEmpty(value) ? value : null)
        {
        }

        public EventFocus()
            : base(null)
        {
        }

        #endregion
    }
}
