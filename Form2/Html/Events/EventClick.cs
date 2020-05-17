using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Form2.Html.Interfaces;

namespace Form2.Html.Events
{
    [SuppressMessage("Style", "IDE0016:Use 'throw' expression", Justification = "<Pending>")]

    public class EventClick : IHtmlEvent
    {
        #region Fields

        private readonly string value;

        #endregion


        #region IHtmlEvent

        public string Name
        {
            get { return "onclick"; }
        }

        public string Value
        {
            get { return value; }
        }

        public bool IsSet
        {
            get { return value != ""; }
        }

        #endregion


        #region Constructors

        public EventClick(string value)
        {
            if (value == null)
                throw new ArgumentNullException();

            this.value = value;
        }

        #endregion


        #region Object

        public override string ToString()
        {
            return IsSet ? string.Format(@" {0}=""{1}""", Name, Value) : "";
        }

        #endregion
    }
}
