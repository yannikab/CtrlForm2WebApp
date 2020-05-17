using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Form2.Html.Attributes.ReadOnly.Boolean;
using Form2.Html.Attributes.ReadOnly.String;
using Form2.Html.Attributes.Variable.Boolean;
using Form2.Html.Attributes.Variable.Integer;
using Form2.Html.Events;

namespace Form2.Html.Content.Elements.Containers
{
    public class HtmlSelect : HtmlContainer
    {
        #region Fields

        private readonly AttrName attrName;

        private readonly AttrDisabled attrDisabled;

        private readonly AttrMultiple attrMultiple;

        private readonly AttrSize attrSize;

        private readonly EventChange eventChange;

        #endregion


        #region Properties

        public override string Tag
        {
            get { return "select"; }
        }

        protected override string Prefix
        {
            get { return "sel"; }
        }

        public AttrName Name
        {
            get { return attrName; }
        }

        public AttrDisabled Disabled
        {
            get { return attrDisabled; }
        }

        public AttrMultiple Multiple
        {
            get { return attrMultiple; }
        }

        public AttrSize Size
        {
            get { return attrSize; }
        }

        public EventChange Change
        {
            get { return eventChange; }
        }

        #endregion


        #region Constructors

        public HtmlSelect(string baseId, int size, bool isPostBack)
            : base(baseId)
        {
            attributes.Add(attrName = new AttrName(baseId));

            attributes.Add(attrDisabled = new AttrDisabled());

            attributes.Add(attrMultiple = new AttrMultiple(true));

            if (size < 1)
                throw new ArgumentException();
            else
                attributes.Add(attrSize = new AttrSize(size));

            if (isPostBack)
                events.Add(eventChange = new EventChange(string.Format("javascript:__doPostBack('{0}','');", baseId)));
        }

        public HtmlSelect(string baseId, bool multiple, bool isPostBack)
            : base(baseId)
        {
            attributes.Add(attrName = new AttrName(baseId));

            attributes.Add(attrDisabled = new AttrDisabled());

            attributes.Add(attrMultiple = new AttrMultiple(multiple));

            attributes.Add(attrSize = new AttrSize());

            if (isPostBack)
                events.Add(eventChange = new EventChange(string.Format("javascript:__doPostBack('{0}','');", baseId)));
        }

        public HtmlSelect(string baseId, bool isPostBack)
            : this(baseId, false, isPostBack)
        {
        }

        public HtmlSelect(string baseId)
            : this(baseId, false)
        {
        }

        #endregion
    }
}
