using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Form2.Html.Attributes.Variable.String;
using Form2.Html.Events;

namespace Form2.Html.Content.Elements.Input
{
    public class HtmlTextBox : HtmlInput
    {
        #region Fields

        private readonly AttrPlaceholder attrPlaceholder;

        private readonly EventChange eventChange;

        #endregion


        #region Properties

        protected override string Prefix
        {
            get { return "txt"; }
        }

        public AttrPlaceholder Placeholder
        {
            get { return attrPlaceholder; }
        }

        public EventChange Change
        {
            get { return eventChange; }
        }

        #endregion


        #region Constructors

        public HtmlTextBox(string name)
            : base(name, "text")
        {
            attributes.Add(attrPlaceholder = new AttrPlaceholder());

            events.Add(eventChange = new EventChange(null));
        }

        #endregion
    }
}
