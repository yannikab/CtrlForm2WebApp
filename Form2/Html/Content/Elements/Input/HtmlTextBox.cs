using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Form2.Html.Attributes.Variable.Decimal;
using Form2.Html.Attributes.Variable.Integer;
using Form2.Html.Attributes.Variable.String;
using Form2.Html.Events;

namespace Form2.Html.Content.Elements.Input
{
    public class HtmlTextBox : HtmlInput
    {
        #region Fields

        private readonly AttrPlaceholder attrPlaceholder;

        private readonly AttrDataNumber attrDataNumber;

        private readonly AttrDataMin attrDataMin;

        private readonly AttrDataMax attrDataMax;

        private readonly AttrDataStep attrDataStep;

        private readonly AttrDataPrecision attrDataPrecision;

        private readonly EventChange eventChange;

        private readonly EventFocus eventFocus;

        private readonly EventBlur eventBlur;

        #endregion


        #region Properties

        protected override string Prefix
        {
            get { return "txt"; }
        }

        public AttrPlaceholder Placeholder
        {
            get { return attrPlaceholder; }
        }

        public AttrDataNumber DataNumber
        {
            get { return attrDataNumber; }
        }

        public AttrDataMin DataMin
        {
            get { return attrDataMin; }
        }

        public AttrDataMax DataMax
        {
            get { return attrDataMax; }
        }

        public AttrDataStep DataStep
        {
            get { return attrDataStep; }
        }

        public AttrDataPrecision DataPrecision
        {
            get { return attrDataPrecision; }
        }

        public EventChange Change
        {
            get { return eventChange; }
        }

        public EventFocus Focus
        {
            get { return eventFocus; }
        }

        public EventBlur Blur
        {
            get { return eventBlur; }
        }

        #endregion


        #region Constructors

        public HtmlTextBox(string name)
            : base(name, "text")
        {
            attributes.Add(attrPlaceholder = new AttrPlaceholder());

            attributes.Add(attrDataNumber = new AttrDataNumber());

            attributes.Add(attrDataMin = new AttrDataMin());

            attributes.Add(attrDataMax = new AttrDataMax());

            attributes.Add(attrDataStep = new AttrDataStep());

            attributes.Add(attrDataPrecision = new AttrDataPrecision());

            events.Add(eventChange = new EventChange());

            events.Add(eventFocus = new EventFocus());

            events.Add(eventBlur = new EventBlur());
        }

        #endregion
    }
}
