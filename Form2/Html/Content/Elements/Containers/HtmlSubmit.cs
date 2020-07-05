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
    public class HtmlSubmit : HtmlContainer
    {
        #region fields

        private readonly AttrType attrType;

        private readonly AttrDisabled attrDisabled;

        private readonly EventClick eventClick;

        #endregion


        #region Properties

        public override string Tag
        {
            get { return "button"; }
        }

        protected override string Prefix
        {
            get { return "btn"; }
        }

        public AttrType Type
        {
            get { return attrType; }
        }

        public AttrDisabled Disabled
        {
            get { return attrDisabled; }
        }

        public EventClick Click
        {
            get { return eventClick; }
        }

        #endregion


        #region Constructors

        public HtmlSubmit(string baseId)
            : base(baseId)
        {
            attributes.Add(attrType = new AttrType("button"));

            attributes.Add(attrDisabled = new AttrDisabled());

            events.Add(eventClick = new EventClick(string.Format("__doPostBack('{0}', '');", baseId)));
        }

        #endregion
    }
}
