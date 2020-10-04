using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Form2.Html.Events
{
    public class EventChange : HtmlEvent
    {
        #region Properties

        public override string Name
        {
            get { return "onchange"; }
        }

        #endregion


        #region Constructors

        public EventChange(string value)
            : base(!string.IsNullOrEmpty(value) ? value : null)
        {
        }

        public EventChange()
            : base(null)
        {
        }

        #endregion
    }
}
