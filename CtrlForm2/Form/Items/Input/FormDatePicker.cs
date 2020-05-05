using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using CtrlForm2.Form.Interfaces;

namespace CtrlForm2.Form.Items.Input
{
    public class FormDatePicker : FormItemInput, IValidate<FormDatePicker>
    {
        #region Fields

        private DateTime? date;

        private bool isDisabled;

        private Func<FormDatePicker, string> validator;

        private Action<FormDatePicker> actionInvalid;

        #endregion


        #region Properties

        public DateTime? Date
        {
            get { return date; }
            set { date = value; }
        }

        public override bool IsEntered
        {
            get { return date.HasValue; }
        }

        public bool IsDisabled
        {
            get { return isDisabled; }
            set { isDisabled = value; }
        }

        #endregion


        #region IValidate<FormDatePicker>

        public Func<FormDatePicker, string> Validator
        {
            get { return validator; }
            set { validator = value; }
        }

        public Action<FormDatePicker> ActionInvalid
        {
            get { return actionInvalid; }
            set { actionInvalid = value; }
        }

        public bool IsValid
        {
            get { return string.IsNullOrEmpty(ValidationMessage); }
        }

        public string ValidationMessage
        {
            get { return Validator(this); }
        }

        #endregion


        #region Constructors

        public FormDatePicker(string baseId, string formId)
            : base(baseId, formId)
        {
            date = null;
        }

        public FormDatePicker(string baseId)
            : this(baseId, baseId.ToLower())
        {
        }

        #endregion
    }
}
