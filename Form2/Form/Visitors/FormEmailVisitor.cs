using System;
using System.Collections.Generic;
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

    public class FormEmailVisitor
    {
        #region Fields

        private string subject;

        protected readonly StringBuilder sb = new StringBuilder();

        private readonly string yes;

        private readonly string no;

        #endregion


        #region Properties

        public string Subject
        {
            get { return subject; }
        }

        public string Body
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
            subject = formTitle.Value.Trim();
        }

        public virtual void Visit(FormLabel formLabel)
        {
        }

        public virtual void Visit(FormButton formButton)
        {
        }

        public virtual void Visit(FormSection formSection)
        {
            if (formSection.IsHidden)
                return;

            foreach (var i in formSection.Contents)
                Visit(i);
        }

        public virtual void Visit(FormTextBox formTextBox)
        {
            if (formTextBox.IsHidden)
                return;

            sb.AppendLine(string.Format("<b>{0}:</b> {1}<br /><br />", formTextBox.Label, formTextBox.Value.Trim()));
        }

        public virtual void Visit(FormTextArea formTextArea)
        {
            if (formTextArea.IsHidden)
                return;

            sb.AppendLine(string.Format("<b>{0}:</b> {1}<br /><br />", formTextArea.Label, formTextArea.Value.Trim()));
        }

        public virtual void Visit(FormPasswordBox formPasswordBox)
        {
            if (formPasswordBox.IsHidden)
                return;

            sb.AppendLine(string.Format("<b>{0}:</b> {1}<br /><br />", formPasswordBox.Label, formPasswordBox.Value.Trim()));
        }

        public virtual void Visit(FormDateBox formDateBox)
        {
            if (formDateBox.IsHidden)
                return;

            sb.AppendLine(string.Format("<b>{0}:</b> {1}<br /><br />", formDateBox.Label, formDateBox.Value));
        }

        public virtual void Visit(FormDatePicker formDatePicker)
        {
            if (formDatePicker.IsHidden)
                return;

            sb.AppendLine(string.Format("<b>{0}:</b> {1}<br /><br />", formDatePicker.Label, formDatePicker.HasValue ? formDatePicker.Value.ToString(formDatePicker.DateFormat.Replace('m', 'M'), CultureInfo.InvariantCulture) : ""));
        }

        public virtual void Visit(FormCheckBox formCheckBox)
        {
            if (formCheckBox.IsHidden)
                return;

            sb.AppendLine(string.Format("<b>{0}:</b> {1}<br /><br />", formCheckBox.Label, formCheckBox.Value ? yes : no));
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

            sb.AppendLine(string.Format("<b>{0}:</b> {1}<br /><br />", formSelect.Label, value));
        }

        public virtual void Visit(FormRadioGroup formRadioGroup)
        {
            if (formRadioGroup.IsHidden)
                return;

            sb.AppendLine(string.Format("<b>{0}:</b> {1}<br /><br />", formRadioGroup.Label, formRadioGroup.Value != null ? formRadioGroup.Value.Text : ""));
        }

        #endregion


        #region Constructors

        public FormEmailVisitor(FormSection formSection, string yes, string no)
        {
            this.yes = yes;
            this.no = no;

            Visit(formSection);
        }

        #endregion
    }
}
