using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using CtrlForm2.Html.Attributes.Variable.Boolean;
using CtrlForm2.Html.Attributes.Variable.String;

namespace CtrlForm2.Html.Elements.Containers
{
    public class HtmlOption : HtmlContainer
    {
        #region Fields

        private readonly AttrValue attrValue;

        private readonly AttrDisabled attrDisabled;

        private readonly AttrSelected attrSelected;

        #endregion


        #region Properties

        public override string Tag
        {
            get { return "option"; }
        }

        public AttrValue AttrValue
        {
            get { return attrValue; }
        }

        public AttrDisabled AttrDisabled
        {
            get { return attrDisabled; }
        }

        public AttrSelected AttrSelected
        {
            get { return attrSelected; }
        }

        #endregion


        #region Constructors

        public HtmlOption(string value)
            : base("")
        {
            attributes.Add(attrDisabled = new AttrDisabled());

            attributes.Add(attrValue = new AttrValue(value));

            attributes.Add(attrSelected = new AttrSelected());
        }

        #endregion
    }
}
