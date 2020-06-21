using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Form2.Html.Attributes.Variable.String;

namespace Form2.Html.Content.Elements.Input
{
    public class HtmlDatePicker : HtmlInput
    {
        #region Fields

        private readonly AttrPlaceHolder attrPlaceHolder;

        private readonly AttrDataDateFormat attrDataDateFormat;

        private readonly AttrDataProvide attrDataProvide;

        #endregion


        #region Properties

        protected override string Prefix
        {
            get { return "dtp"; }
        }

        public AttrPlaceHolder PlaceHolder
        {
            get { return attrPlaceHolder; }
        }

        public AttrDataDateFormat DataDateFormat
        {
            get { return attrDataDateFormat; }
        }

        public AttrDataProvide DataProvide
        {
            get { return attrDataProvide; }
        }

        #endregion


        #region Constructors

        public HtmlDatePicker(string baseId)
            : base(baseId, "text")
        {
            attributes.Add(attrPlaceHolder = new AttrPlaceHolder());
            attributes.Add(attrDataDateFormat = new AttrDataDateFormat());
            attributes.Add(attrDataProvide = new AttrDataProvide("datepicker"));
        }

        #endregion
    }
}
