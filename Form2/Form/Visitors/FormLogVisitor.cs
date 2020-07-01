using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Form2.Form.Content;
using Form2.Form.Content.Items;
using Form2.Form.Content.Items.Input;
using Form2.Form.Content.Items.Input.Selectors;

namespace Form2.Form.Visitors
{
    [SuppressMessage("Style", "IDE0019:Use pattern matching", Justification = "<Pending>")]
    [SuppressMessage("Style", "IDE0029:Use coalesce expression", Justification = "<Pending>")]

    public class FormLogVisitor
    {
        #region Fields

        protected readonly StringBuilder sb = new StringBuilder();

        private readonly string yes;

        private readonly string no;

        #endregion


        #region Properties

        public string Text
        {
            get { return sb.ToString(); }
        }

        #endregion


        #region Methods

        public void Visit(FormContent formItem)
        {
            var mi = (from m in GetType().GetMethods()
                      where
                      m.ReturnType.Equals(typeof(void)) &&
                      m.GetParameters().Length == 1 &&
                      m.GetParameters()[0].ParameterType.Equals(formItem.GetType())
                      select m).SingleOrDefault();

            if (mi != null)
                mi.Invoke(this, new object[] { formItem });
            else
                throw new NotImplementedException();
        }

        public virtual void Visit(FormTitle formTitle)
        {
            if (formTitle.IsHidden)
                return;

            sb.AppendLine(formTitle.Value.Trim());
        }

        public virtual void Visit(FormLabel formLabel)
        {
        }

        public virtual void Visit(FormButton formButton)
        {
        }

        public virtual void Visit(FormGroup formGroup)
        {
            if (formGroup.IsHidden)
                return;

            foreach (var i in formGroup.Contents)
                Visit(i);
        }

        public virtual void Visit(FormTextBox formTextBox)
        {
            if (formTextBox.IsHidden)
                return;

            sb.AppendLine(string.Format("{0}: {1}", formTextBox.Label, formTextBox.Value.Trim()));
        }

        public virtual void Visit(FormTextArea formTextArea)
        {
            if (formTextArea.IsHidden)
                return;

            sb.AppendLine(string.Format("{0}: {1}", formTextArea.Label, formTextArea.Value.Trim()));
        }

        public virtual void Visit(FormPasswordBox formPasswordBox)
        {
            if (formPasswordBox.IsHidden)
                return;

            sb.AppendLine(string.Format("{0}: {1}", formPasswordBox.Label, formPasswordBox.Value.Trim()));
        }

        public virtual void Visit(FormDateBox formDateBox)
        {
            if (formDateBox.IsHidden)
                return;

            sb.AppendLine(string.Format("{0}: {1}", formDateBox.Label, formDateBox.Value));
        }

        public virtual void Visit(FormDatePicker formDatePicker)
        {
            if (formDatePicker.IsHidden)
                return;

            sb.AppendLine(string.Format("{0}: {1}", formDatePicker.Label, formDatePicker.HasValue ? formDatePicker.Value.ToString(formDatePicker.DateFormat.Replace('m', 'M'), CultureInfo.InvariantCulture) : ""));
        }

        public virtual void Visit(FormCheckBox formCheckBox)
        {
            if (formCheckBox.IsHidden)
                return;

            sb.AppendLine(string.Format("{0}: {1}", formCheckBox.Label, formCheckBox.Value ? yes : no));
        }

        public virtual void Visit(FormSelect formSelect)
        {
            if (formSelect.IsHidden)
                return;

            StringBuilder sbValues = new StringBuilder();

            foreach (var v in formSelect.Value)
                sbValues.Append(string.Format("{0}, ", v.Text));

            string value = sbValues.ToString();

            if (value.EndsWith(", "))
                value = value.Substring(0, value.Length - 2);

            sb.AppendLine(string.Format("{0}: {1}", formSelect.Label, value));
        }

        public virtual void Visit(FormRadioGroup formRadioGroup)
        {
            if (formRadioGroup.IsHidden)
                return;

            sb.AppendLine(string.Format("{0}: {1}", formRadioGroup.Label, formRadioGroup.Value != null ? formRadioGroup.Value.Text : ""));
        }

        #endregion


        #region Constructors

        public FormLogVisitor(FormGroup formGroup, string yes, string no)
        {
            this.yes = yes;
            this.no = no;

            sb.AppendLine();

            Visit(formGroup);
        }

        #endregion
    }
}
