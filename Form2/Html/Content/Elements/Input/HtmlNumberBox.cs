using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Form2.Html.Attributes.Variable.Decimal;
using Form2.Html.Attributes.Variable.String;

namespace Form2.Html.Content.Elements.Input
{
    public class HtmlNumberBox : HtmlInput
    {
        #region Fields

        private readonly AttrPlaceholder attrPlaceholder;

        private readonly AttrMin attrMin;

        private readonly AttrMax attrMax;

        private readonly AttrStep attrStep;

        #endregion


        #region Properties

        public override string Tag
        {
            get { return "input"; }
        }

        protected override string Prefix
        {
            get { return "nbx"; }
        }

        public AttrPlaceholder Placeholder
        {
            get { return attrPlaceholder; }
        }

        public AttrMin Min
        {
            get { return attrMin; }
        }

        public AttrMax Max
        {
            get { return attrMax; }
        }

        public AttrStep Step
        {
            get { return attrStep; }
        }

        #endregion


        #region Constructors

        public HtmlNumberBox(string name)
            : base(name, "number")
        {
            attributes.Add(attrPlaceholder = new AttrPlaceholder());

            attributes.Add(attrMin = new AttrMin());

            attributes.Add(attrMax = new AttrMax());

            attributes.Add(attrStep = new AttrStep());
        }

        #endregion
    }
}
