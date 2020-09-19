using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
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

    public class FormLastMessageVisitor
    {
        #region Fields

        private readonly bool submit;

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
        }

        public virtual void Visit(FormLabel formLabel)
        {
        }

        public virtual void Visit(FormButton formButton)
        {
        }

        public virtual void Visit(FormGroup formGroup)
        {
            foreach (var i in formGroup.Contents)
                Visit(i);
        }

        public virtual void Visit(FormTextBox formTextBox)
        {
            if (submit)
                formTextBox.LastMessage = formTextBox.IsRequired && !formTextBox.HasValue ? formTextBox.RequiredMessage : formTextBox.ValidationMessage;

            formTextBox.UseLastMessage = !submit;
        }

        public virtual void Visit(FormTextArea formTextArea)
        {
            if (submit)
                formTextArea.LastMessage = formTextArea.IsRequired && !formTextArea.HasValue ? formTextArea.RequiredMessage : formTextArea.ValidationMessage;

            formTextArea.UseLastMessage = !submit;
        }

        public virtual void Visit(FormPasswordBox formPasswordBox)
        {
            if (submit)
                formPasswordBox.LastMessage = formPasswordBox.IsRequired && !formPasswordBox.HasValue ? formPasswordBox.RequiredMessage : formPasswordBox.ValidationMessage;

            formPasswordBox.UseLastMessage = !submit;
        }

        public virtual void Visit(FormDateBox formDateBox)
        {
            if (submit)
                formDateBox.LastMessage = formDateBox.IsRequired && !formDateBox.HasValue ? formDateBox.RequiredMessage : formDateBox.ValidationMessage;

            formDateBox.UseLastMessage = !submit;
        }

        public virtual void Visit(FormDatePicker formDatePicker)
        {
            if (submit)
                formDatePicker.LastMessage = formDatePicker.IsRequired && !formDatePicker.HasValue ? formDatePicker.RequiredMessage : formDatePicker.ValidationMessage;

            formDatePicker.UseLastMessage = !submit;
        }

        public virtual void Visit(FormCheckBox formCheckBox)
        {
            if (submit)
                formCheckBox.LastMessage = formCheckBox.IsRequired && !formCheckBox.HasValue ? formCheckBox.RequiredMessage : formCheckBox.ValidationMessage;

            formCheckBox.UseLastMessage = !submit;
        }

        public virtual void Visit(FormNumberSpinner formNumberSpinner)
        {
            if (submit)
                formNumberSpinner.LastMessage = formNumberSpinner.IsRequired && !formNumberSpinner.HasValue ? formNumberSpinner.RequiredMessage : formNumberSpinner.ValidationMessage;

            formNumberSpinner.UseLastMessage = !submit;
        }

        public virtual void Visit(FormSelect formSelect)
        {
            if (submit)
                formSelect.LastMessage = formSelect.IsRequired && !formSelect.HasValue ? formSelect.RequiredMessage : formSelect.ValidationMessage;

            formSelect.UseLastMessage = !submit;
        }

        public virtual void Visit(FormRadioGroup formRadioGroup)
        {
            if (submit)
                formRadioGroup.LastMessage = formRadioGroup.IsRequired && !formRadioGroup.HasValue ? formRadioGroup.RequiredMessage : formRadioGroup.ValidationMessage;

            formRadioGroup.UseLastMessage = !submit;
        }

        #endregion


        #region Constructors


        public FormLastMessageVisitor(FormGroup formGroup, bool submit)
        {
            this.submit = submit;

            Visit(formGroup);
        }

        #endregion
    }
}
