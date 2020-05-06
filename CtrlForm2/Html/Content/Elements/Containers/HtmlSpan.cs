using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CtrlForm2.Html.Content.Elements.Containers
{
    public class HtmlSpan : HtmlContainer
    {
        #region Properties

        public override string Tag
        {
            get { return "span"; }
        }

        protected override string Prefix
        {
            get { return "spn"; }
        }

        #endregion


        #region Constructors

        public HtmlSpan(string baseId)
            : base(baseId)
        {
        }

        public HtmlSpan()
            : this("")
        {
        }

        #endregion
    }
}
