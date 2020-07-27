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

        private readonly AttrPlaceholder attrPlaceholder;

        private readonly AttrDataDateFormat attrDataDateFormat;

        private readonly AttrDataProvide attrDataProvide;

        private readonly AttrAutoComplete attrAutoComplete;

        #endregion


        #region Properties

        protected override string Prefix
        {
            get { return "dtp"; }
        }

        public AttrPlaceholder Placeholder
        {
            get { return attrPlaceholder; }
        }

        public AttrDataDateFormat DataDateFormat
        {
            get { return attrDataDateFormat; }
        }

        public AttrDataProvide DataProvide
        {
            get { return attrDataProvide; }
        }

        public AttrAutoComplete AutoComplete
        {
            get { return attrAutoComplete; }
        }

        #endregion


        #region Constructors

        public HtmlDatePicker(string name)
            : base(name, "text")
        {
            attributes.Add(attrPlaceholder = new AttrPlaceholder());
            attributes.Add(attrDataDateFormat = new AttrDataDateFormat());
            attributes.Add(attrDataProvide = new AttrDataProvide("datepicker"));
            attributes.Add(attrAutoComplete = new AttrAutoComplete());
        }

        #endregion
    }
}
