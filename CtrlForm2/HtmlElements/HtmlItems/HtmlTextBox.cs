using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using UserControls.CtrlForm2.HtmlElements.HtmlAttributes;

namespace UserControls.CtrlForm2.HtmlElements.HtmlItems
{
    public class HtmlTextBox : HtmlInput
    {
        #region Fields

        private readonly AttrPlaceHolder attrPlaceHolder;

        #endregion


        #region Properties

        protected override string IdPrefix
        {
            get { return "txt"; }
        }

        public AttrPlaceHolder PlaceHolder
        {
            get { return attrPlaceHolder; }
        }

        #endregion


        #region Constructors

        public HtmlTextBox(string baseId)
            : base(baseId, "text")
        {
            attributes.Add(attrPlaceHolder = new AttrPlaceHolder());
        }

        #endregion
    }
}
