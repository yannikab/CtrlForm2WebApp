using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using CtrlForm2.Html.Attributes.ReadOnly.String;
using CtrlForm2.Html.Attributes.Variable.Boolean;

namespace CtrlForm2.Html.Content.Elements.Containers
{
    public class HtmlRadioGroup : HtmlContainer
    {
        #region Fields

        private readonly AttrName attrName;

        private readonly AttrDisabled attrDisabled;

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

        #endregion


        #region Constructors

        public HtmlRadioGroup(string baseId)
            : base(baseId)
        {
            attrName = new AttrName(baseId);

            attributes.Add(attrDisabled = new AttrDisabled());
        }

        #endregion
    }
}
