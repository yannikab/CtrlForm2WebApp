using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using CtrlForm2.Html.Attributes.Variable.String;

namespace CtrlForm2.Html.Elements.Items
{
    public class HtmlDatePicker : HtmlInput
    {
        #region Fields

        #endregion


        #region Properties

        protected override string IdPrefix
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
