using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using CtrlForm2.Form.Content;
using CtrlForm2.Form.Content.Items;
using CtrlForm2.Form.Content.Items.Input;
using CtrlForm2.Form.Content.Items.Input.Selectors;

namespace CtrlForm2.Form.Visitors
{
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

        public virtual void Visit(FormGroup formGroup)
        {
            foreach (var i in formGroup.Contents)
            {
                if (i is FormInput || i is FormGroup)
                    Visit(i);
            }
        }

        public virtual void Visit(FormTextBox formTextBox)
        {
            formTextBox.Text = form[formTextBox.BaseId];
        }

        public virtual void Visit(FormTextArea formTextArea)
        {
            formTextArea.Text = form[formTextArea.BaseId];
        }

        public virtual void Visit(FormPasswordBox formPasswordBox)
        {
            formPasswordBox.Text = form[formPasswordBox.BaseId];
        }

        public virtual void Visit(FormDatePicker formDatePicker)
        {
            try { formDatePicker.Date = Convert.ToDateTime(form[formDatePicker.BaseId]); } catch { }
        }

        public virtual void Visit(FormCheckBox formCheckBox)
        {
            formCheckBox.IsChecked = form[formCheckBox.BaseId] == "on";
        }

        public virtual void Visit(FormSelect formSelect)
        {
            int previousIndex = 0;

            var options = formSelect.Options.ToList();

            for (int i = 0; i < options.Count; i++)
                options[i].IsSelected = false;

            if (form[formSelect.BaseId] == null)
                return;

            foreach (var o in form[formSelect.BaseId].Split(','))
            {
                for (int i = previousIndex; i < options.Count; i++)
                {
                    if (options[i].Value == o)
                    {
                        options[i].IsSelected = true;
                        previousIndex = i + 1;

                        break;
                    }
                }
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
