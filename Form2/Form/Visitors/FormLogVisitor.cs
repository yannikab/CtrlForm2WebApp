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
using Form2.Form.Interfaces;

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

        protected readonly bool showMarks;

        protected readonly bool showRequired;

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

        protected virtual string Mark(IRequired formItem)
        {
            if (showMarks && formItem.IsRequired && showRequired)
                return formItem.RequiredMark;
            else if (showMarks && !formItem.IsRequired && !showRequired)
                return formItem.OptionalMark;
            else
                return "";
        }

        public virtual void Visit(FormTitle formTitle)
        {
            sb.AppendLine();
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
            foreach (var c in formGroup.Contents.Where(c => !c.IsHidden))
                Visit(c);
        }

        public virtual void Visit(FormTextBox formTextBox)
        {
            sb.AppendLine(string.Format("{0}{1}: {2}", formTextBox.Label, Mark(formTextBox), formTextBox.Value));
        }

        public virtual void Visit(FormTextArea formTextArea)
        {
            sb.AppendLine(string.Format("{0}{1}: {2}", formTextArea.Label, Mark(formTextArea), formTextArea.Value));
        }

        public virtual void Visit(FormPasswordBox formPasswordBox)
        {
            sb.AppendLine(string.Format("{0}{1}: {2}", formPasswordBox.Label, Mark(formPasswordBox), formPasswordBox.Value));
        }

        public virtual void Visit(FormDateBox formDateBox)
        {
            sb.AppendLine(string.Format("{0}{1}: {2}", formDateBox.Label, Mark(formDateBox), formDateBox.Value));
        }

        public virtual void Visit(FormDatePicker formDatePicker)
        {
            sb.AppendLine(string.Format("{0}{1}: {2}", formDatePicker.Label, Mark(formDatePicker), formDatePicker.HasValue ? formDatePicker.Value.ToString(formDatePicker.DateFormat.Replace('m', 'M'), CultureInfo.InvariantCulture) : ""));
        }

        public virtual void Visit(FormCheckBox formCheckBox)
        {
            sb.AppendLine(string.Format("{0}{1}: {2}", formCheckBox.Label, Mark(formCheckBox), formCheckBox.Value ? yes : no));
        }

        public virtual void Visit(FormNumberBox formNumberBox)
        {
            sb.AppendLine(string.Format("{0}{1}: {2}", formNumberBox.Label, Mark(formNumberBox), formNumberBox.Value));
        }

        public virtual void Visit(FormSelect formSelect)
        {
            StringBuilder sbValues = new StringBuilder();

            foreach (var v in formSelect.Value)
                sbValues.Append(string.Format("{0}, ", v.Text));

            string value = sbValues.ToString();

            if (value.EndsWith(", "))
                value = value.Substring(0, value.Length - 2);

            sb.AppendLine(string.Format("{0}{1}: {2}", formSelect.Label, Mark(formSelect), value));
        }

        public virtual void Visit(FormRadioGroup formRadioGroup)
        {
            sb.AppendLine(string.Format("{0}{1}: {2}", formRadioGroup.Label, Mark(formRadioGroup), formRadioGroup.Value != null ? formRadioGroup.Value.Text : ""));
        }

        #endregion


        #region Constructors

        public FormLogVisitor(FormGroup formGroup, string yes, string no, bool showMarks, bool showRequired)
        {
            this.yes = yes;
            this.no = no;
            this.showMarks = showMarks;
            this.showRequired = showRequired;

            sb.AppendLine();

            if (!formGroup.IsHidden)
                Visit(formGroup);
        }

        #endregion
    }
}
