using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using CtrlForm2.Html.Attributes.Variable.String;

namespace CtrlForm2.Html.Content.Elements.Input
{
    public class HtmlPasswordBox : HtmlInput
    {
        #region Fields

        private readonly AttrPlaceHolder attrPlaceHolder;

        #endregion


        #region Properties

        protected override string Prefix
        {
            get { return "txt"; }
        }

        public AttrPlaceHolder PlaceHolder
        {
            get { return attrPlaceHolder; }
        }

        #endregion


        #region Constructors

        public HtmlPasswordBox(string baseId)
            : base(baseId, "password")
        {
            attributes.Add(attrPlaceHolder = new AttrPlaceHolder());
        }

        #endregion
    }
}
