using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Form2.Html.Events
{
    public class EventBlur : HtmlEvent
    {
        #region Properties

        public override string Name
        {
            get { return "onblur"; }
        }

        #endregion


        #region Constructors

        public EventBlur(string value)
            : base(!string.IsNullOrEmpty(value) ? value : null)
        {
        }

        public EventBlur()
            : base(null)
        {
        }

        #endregion
    }
}
