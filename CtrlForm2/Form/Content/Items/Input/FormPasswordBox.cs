using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using CtrlForm2.Form.Enums;
using CtrlForm2.Form.Interfaces;

namespace CtrlForm2.Form.Content.Items.Input
{
    public class FormPasswordBox : FormInput, IValidate<FormPasswordBox>
    {
        #region Fields

        private string text;

        private string placeHolder;

        private bool isDisabled;

        private FormIcon icon;

        private Func<FormPasswordBox, string> validator;

        private Action<FormPasswordBox> actionInvalid;

        #endregion


        #region Properties

        public string Text
        {
            get { return text; }
            set { text = value; }
        }

        public string PlaceHolder
        {
            get { return placeHolder; }
            set { placeHolder = value; }
        }

        public bool IsDisabled
        {
            get { return isDisabled; }
            set { isDisabled = value; }
        }

        public FormIcon Icon
        {
            get { return icon; }
            set { icon = value; }
        }

        #endregion


        #region IRequired

        public override bool IsEntered
        {
            get { return !string.IsNullOrEmpty(Text); }
        }

        #endregion


        #region IValidate<FormTextBox>

        public Func<FormPasswordBox, string> Validator
        {
            get { return validator; }
            set { validator = value; }
        }

        public Action<FormPasswordBox> ActionInvalid
        {
            get { return actionInvalid; }
            set { actionInvalid = value; }
        }

        public string ValidationMessage
        {
            get { return Validator(this); }
        }

        public bool IsValid
        {
            get { return (IsReadOnly ?? false) || string.IsNullOrEmpty(ValidationMessage); }
        }

        #endregion


        #region Constructors

        public FormPasswordBox(string baseId, string formId)
            : base(baseId, formId)
        {
            text = "";
            placeHolder = "";
            icon = FormIcon.NotSet;
            validator = (s) => { return null; };
            actionInvalid = (s) => { return; };
        }

        public FormPasswordBox(string baseId)
            : this(baseId, baseId.ToLower())
        {
        }

        #endregion


        #region Object

        public override string ToString()
        {
            return string.Format("{0}: {1}, Label: {2}, Text: {3}", GetType().Name, BaseId, Label, Text);
        }

        #endregion
    }
}
