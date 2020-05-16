using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

using CtrlForm2.Form.Content.Items.Input;
using CtrlForm2.Form.Content.Items.Input.Selectors;
using CtrlForm2.Form.Enums;

namespace CtrlForm2.Form.Selectables
{
    public class FormRadioButton : FormSelectable
    {
        #region Fields

        private FormRadioGroup formRadioGroup;

        private string value;

        private string text;

        #endregion


        #region Properties

        public FormRadioGroup FormRadioGroup
        {
            get { return formRadioGroup; }
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
            if (container != null && !(container is FormRadioGroup))
                throw new ArgumentException();

            this.formRadioGroup = container as FormRadioGroup;
        }

        #endregion 


        #region Constructors

        public FormRadioButton(string value, string text)
        {
            this.value = value;
            this.text = text;
        }

        public FormRadioButton(int value, string text)
            : this(value.ToString(), text)
        {
        }

        public FormRadioButton(string text)
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
