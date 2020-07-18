using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Form2.Html.Events
{
    public abstract class HtmlEvent
    {
        #region Fields

        private string value;

        #endregion


        #region Properties

        public abstract string Name { get; }

        public string Value
        {
            get { return this.value; }
            set { this.value = value; }
        }

        public bool IsSet
        {
            get { return value != null; }
        }

        #endregion

        #region Constructors

        public HtmlEvent(string value)
        {
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
