using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Form2.Html.Attributes.ReadOnly.String;
using Form2.Html.Attributes.Variable.Boolean;
using Form2.Html.Attributes.Variable.String;
using Form2.Html.Events;

namespace Form2.Html.Content.Elements.Containers
{
    public class HtmlButton : HtmlElement
    {
        #region fields

        private readonly AttrType attrType;

        private readonly AttrDisabled attrDisabled;

        private readonly AttrValue attrValue;

        private readonly EventClick eventClick;

        #endregion


        #region Properties

        public override string Tag
        {
            get { return "input"; }
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

        public AttrValue Value
        {
            get { return attrValue; }
        }

        public EventClick Click
        {
            get { return eventClick; }
        }

        #endregion


        #region Constructors

        public HtmlButton(string baseId, string onClick)
            : base(baseId)
        {
            attributes.Add(attrType = new AttrType("button"));

            attributes.Add(attrDisabled = new AttrDisabled());

            attributes.Add(attrValue = new AttrValue());

            events.Add(eventClick = new EventClick(onClick));
        }

        public HtmlButton(string baseId)
            : this(baseId, "")
        {
        }

        #endregion
    }
}
