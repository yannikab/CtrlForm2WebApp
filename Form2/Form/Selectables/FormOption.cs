using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

using Form2.Form.Content.Items.Input;
using Form2.Form.Content.Items.Input.Selectors;

namespace Form2.Form.Selectables
{
    public class FormOption : FormSelectable
    {
        #region Fields

        private FormSelect formSelect;

        private string value;

        private string text;

        #endregion


        #region Properties

        public FormSelect FormSelect
        {
            get { return formSelect; }
        }

        public string Value
        {
            get { return value; }
            set { this.value = value; }
        }

        public string Text
        {
            get { return text; }
            set { text = value; }
        }

        #endregion


        #region Methods

        public override void SetContainer<S, V>(FormSelector<S, V> container)
        {
            if (container != null && !(container is FormSelect))
                throw new ArgumentException();

            this.formSelect = container as FormSelect;
        }

        #endregion 


        #region Constructors

        public FormOption(string value, string text)
        {
            this.value = value;
            this.text = text;
        }

        public FormOption(int value, string text)
            : this(value.ToString(), text)
        {
        }

        public FormOption(string text)
            : this(new Regex(@"(\p{Z}|\p{P}|\p{S})*").Replace(CultureInfo.InvariantCulture.TextInfo.ToTitleCase(text), ""), text)
        {
        }

        #endregion


        #region Object

        public override string ToString()
        {
            return string.Format("{0} (Value: '{1}', Text: '{2}', IsSelected: {3})", GetType().Name, Value, Text, IsSelected);
        }

        #endregion
    }
}
