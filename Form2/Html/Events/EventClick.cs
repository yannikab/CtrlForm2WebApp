using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Form2.Html.Events
{
    public class EventClick : HtmlEvent
    {
        #region Properties

        public override string Name
        {
            get { return "onclick"; }
        }

        #endregion


        #region Constructors

        public EventClick(string value)
            : base(value)
        {
        }

        public EventClick()
            : base(null)
        {
        }

        #endregion
    }
}
