using System;
using System.Collections.Generic;
using System.Collections.Specialized;
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

    public class FormPostBackVisitor
    {
        #region Fields

        private readonly NameValueCollection form;

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
            formTextBox.Content = form[formTextBox.BaseId];
        }

        public virtual void Visit(FormTextArea formTextArea)
        {
            formTextArea.Content = form[formTextArea.BaseId];
        }

        public virtual void Visit(FormPasswordBox formPasswordBox)
        {
            formPasswordBox.Content = form[formPasswordBox.BaseId];
        }

        public virtual void Visit(FormDateBox formDateBox)
        {
            formDateBox.Content = form[formDateBox.BaseId];
        }

        public virtual void Visit(FormDatePicker formDatePicker)
        {
            formDatePicker.Content = form[formDatePicker.BaseId];
        }

        public virtual void Visit(FormCheckBox formCheckBox)
        {
            if (form[formCheckBox.BaseId] == null)
            {
                formCheckBox.Content = false;
                return;
            }

            formCheckBox.Content = form[formCheckBox.BaseId].ToLower() == "on";
        }

        public virtual void Visit(FormSelect formSelect)
        {
            int previousIndex = 0;

            var content = formSelect.Content.ToList();

            for (int i = 0; i < content.Count; i++)
                content[i].IsSelected = false;

            if (form[formSelect.BaseId] == null)
                return;

            foreach (var o in form[formSelect.BaseId].Split(','))
            {
                for (int i = previousIndex; i < content.Count; i++)
                {
                    if (formSelect.Header != null && content[i] == formSelect.Header)
                        continue;

                    if (content[i].IsHidden)
                        continue;

                    if (content[i].IsDisabled)
                        continue;

                    if (content[i].Value == o)
                    {
                        content[i].IsSelected = true;
                        previousIndex = i + 1;

                        break;
                    }
                }
            }
        }

        public virtual void Visit(FormRadioGroup formRadioGroup)
        {
            foreach (var c in formRadioGroup.Content)
            {
                if (c.IsHidden)
                    continue;

                if (c.IsDisabled)
                    continue;

                c.IsSelected = c.Value == form[formRadioGroup.BaseId];
            }
        }

        #endregion


        #region Constructors

        public FormPostBackVisitor(FormGroup formGroup, NameValueCollection form)
        {
            this.form = form;

            Visit(formGroup);
        }

        #endregion
    }
}
