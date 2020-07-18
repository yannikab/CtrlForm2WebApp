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
        
        private readonly NameValueCollection values;
        
        private readonly FormItem source;

        private readonly string argument;

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

        public virtual void Visit(FormSection formSection)
        {
            foreach (var i in formSection.Contents)
                Visit(i);
        }

        public virtual void Visit(FormTextBox formTextBox)
        {
            formTextBox.Content = values[formTextBox.BaseId];
        }

        public virtual void Visit(FormTextArea formTextArea)
        {
            formTextArea.Content = values[formTextArea.BaseId];
        }

        public virtual void Visit(FormPasswordBox formPasswordBox)
        {
            formPasswordBox.Content = values[formPasswordBox.BaseId];
        }

        public virtual void Visit(FormDateBox formDateBox)
        {
            formDateBox.Content = values[formDateBox.BaseId];
        }

        public virtual void Visit(FormDatePicker formDatePicker)
        {
            formDatePicker.Content = values[formDatePicker.BaseId];
        }

        public virtual void Visit(FormCheckBox formCheckBox)
        {
            if (values[formCheckBox.BaseId] == null)
            {
                formCheckBox.Content = false;
                return;
            }

            formCheckBox.Content = values[formCheckBox.BaseId].ToLower() == "on";
        }

        public virtual void Visit(FormNumberBox formNumberBox)
        {
            formNumberBox.Content = values[formNumberBox.BaseId];

            if (source != formNumberBox)
                return;

            switch (argument)
            {
                case "Incr":
                    formNumberBox.Content = (formNumberBox.Value + formNumberBox.Step).ToString();
                    break;

                case "Decr":
                    formNumberBox.Content = (formNumberBox.Value - formNumberBox.Step).ToString();
                    break;

                default:
                    break;
            }
        }

        public virtual void Visit(FormSelect formSelect)
        {
            int previousIndex = 0;

            var content = formSelect.Content.ToList();

            for (int i = 0; i < content.Count; i++)
                content[i].IsSelected = false;

            if (values[formSelect.BaseId] == null)
                return;

            foreach (var o in values[formSelect.BaseId].Split(','))
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

                c.IsSelected = c.Value == values[formRadioGroup.BaseId];
            }
        }

        #endregion


        #region Constructors

        public FormPostBackVisitor(FormSection formSection, NameValueCollection values, FormItem source, string argument)
        {
            this.values = values;
            this.source = source;
            this.argument = argument;

            Visit(formSection);
        }

        #endregion
    }
}
