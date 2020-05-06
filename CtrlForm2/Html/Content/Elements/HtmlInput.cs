using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using CtrlForm2.Html.Attributes.ReadOnly.String;
using CtrlForm2.Html.Attributes.Variable.Boolean;
using CtrlForm2.Html.Attributes.Variable.String;

namespace CtrlForm2.Html.Content.Elements
{
    public abstract class HtmlInput : HtmlElement
    {
        #region Fields

        private readonly AttrReadOnly attrReadOnly;

        private readonly AttrDisabled attrDisabled;

        private readonly AttrType attrType;

        private readonly AttrName attrName;

        private readonly AttrValue attrValue;

        #endregion


        #region Properties

        public override string Tag
        {
            get { return "input"; }
        }

        public AttrReadOnly ReadOnly
        {
            get { return attrReadOnly; }
        }

        public AttrDisabled Disabled
        {
            get { return attrDisabled; }
        }

        public AttrType Type
        {
            get { return attrType; }
        }

        public AttrName Name
        {
            get { return attrName; }
        }

        public AttrValue Value
        {
            get { return attrValue; }
        }

        #endregion


        #region Constructors

        public HtmlInput(string baseId, string type)
            : base(baseId)
        {
            attributes.Add(attrReadOnly = new AttrReadOnly());

            attributes.Add(attrDisabled = new AttrDisabled());

            attributes.Add(attrType = new AttrType(type));

            attributes.Add(attrName = new AttrName(baseId));

            attributes.Add(attrValue = new AttrValue());
        }

        #endregion


        #region Object

        public override string ToString()
        {
            return string.Format("{0}: {1}, Value: {2}", GetType().Name, Id.Value, Value.Value);
        }

        #endregion
    }
}
