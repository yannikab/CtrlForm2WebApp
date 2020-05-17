using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Form2.Html.Content.Elements.Containers
{
    public class HtmlDiv : HtmlContainer
    {
        #region Properties

        public override string Tag
        {
            get { return "div"; }
        }

        protected override string Prefix
        {
            get { return "div"; }
        }

        #endregion


        #region Constructors

        public HtmlDiv(string baseId)
            : base(baseId)
        {
        }

        public HtmlDiv()
            : this("")
        {
        }

        #endregion
    }
}
