using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CtrlForm2.Html.Content.Elements.Input
{
    public class HtmlDatePicker : HtmlInput
    {
        #region Fields

        #endregion


        #region Properties

        protected override string Prefix
        {
            get { return "dtp"; }
        }

        #endregion


        #region Constructors

        public HtmlDatePicker(string baseId)
            : base(baseId, "date")
        {
        }

        #endregion
    }
}
