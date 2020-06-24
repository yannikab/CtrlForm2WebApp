using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Form2.Html.Content.Elements.Input
{
    public class HtmlDateBox : HtmlInput
    {
        #region Properties

        protected override string Prefix
        {
            get { return "dtb"; }
        }

        #endregion


        #region Constructors

        public HtmlDateBox(string baseId)
            : base(baseId, "date")
        {
        }

        #endregion
    }
}
