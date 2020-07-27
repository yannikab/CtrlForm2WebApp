using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Form2.Html.Attributes.ReadOnly.String;
using Form2.Html.Attributes.Variable.Boolean;
using Form2.Html.Events;

namespace Form2.Html.Content.Elements.Containers
{
    public class HtmlRadioGroup : HtmlContainer
    {
        #region Fields

        private readonly AttrName attrName;

        private readonly AttrDisabled attrDisabled;

        private readonly EventChange eventChange;

        #endregion


        #region Properties

        public override string Tag
        {
            get { return "div"; }
        }

        protected override string Prefix
        {
            get { return "rbg"; }
        }

        public AttrName Name
        {
            get { return attrName; }
        }

        public AttrDisabled Disabled
        {
            get { return attrDisabled; }
        }

        public EventChange Change
        {
            get { return eventChange; }
        }

        #endregion


        #region Constructors

        public HtmlRadioGroup(string name, bool verbose, bool isPostBack)
            : base(verbose ? name : "")
        {
            attrName = new AttrName(name);

            attributes.Add(attrDisabled = new AttrDisabled());

            if (isPostBack)
                events.Add(eventChange = new EventChange(string.Format("__doPostBack('{0}', '');", name)));
        }

        public HtmlRadioGroup(string name)
            : this(name, false, false)
        {
        }

        #endregion
    }
}
