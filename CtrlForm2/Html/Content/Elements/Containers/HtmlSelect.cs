using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using CtrlForm2.Html.Attributes.ReadOnly.Boolean;
using CtrlForm2.Html.Attributes.ReadOnly.String;
using CtrlForm2.Html.Attributes.Variable.Boolean;
using CtrlForm2.Html.Attributes.Variable.Integer;

namespace CtrlForm2.Html.Content.Elements.Containers
{
    public class HtmlSelect : HtmlContainer
    {
        #region Fields

        private readonly AttrName attrName;

        private readonly AttrDisabled attrDisabled;

        private readonly AttrMultiple attrMultiple;

        private readonly AttrSize attrSize;

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

        #endregion


        #region Constructors

        public HtmlSelect(string baseId, int size)
            : base(baseId)
        {
            attributes.Add(attrName = new AttrName(baseId));

            attributes.Add(attrDisabled = new AttrDisabled());

            attributes.Add(attrMultiple = new AttrMultiple(true));

            if (size < 1)
                throw new ArgumentException();
            else
                attributes.Add(attrSize = new AttrSize(size));
        }

        public HtmlSelect(string baseId, bool multiple)
            : base(baseId)
        {
            attributes.Add(attrName = new AttrName(baseId));

            attributes.Add(attrDisabled = new AttrDisabled());

            attributes.Add(attrMultiple = new AttrMultiple(multiple));

            attributes.Add(attrSize = new AttrSize());
        }

        public HtmlSelect(string baseId)
            : this(baseId, false)
        {
        }

        #endregion
    }
}
