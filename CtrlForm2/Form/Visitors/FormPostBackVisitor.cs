using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using CtrlForm2.Form.Groups;
using CtrlForm2.Form.Items;
using CtrlForm2.Form.Items.Input;
using CtrlForm2.Form.Items.Input.Selectors;
using CtrlForm2.Form.Selectables;

namespace CtrlForm2.Form.Visitors
{
    public class FormPostBackVisitor
    {
        #region Fields

        private readonly NameValueCollection form;

        #endregion


        #region Methods

        public void Visit(FormItem formItem)
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
            foreach (var i in formGroup.Items)
            {
                if (i is FormItemInput || i is FormGroup)
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

        public virtual void Visit(FormSelect formSelector)
        {
            foreach (var o in formSelector.Options)
                Visit(o);
        }

        public virtual void Visit(FormOption formOption)
        {
            formOption.IsSelected = form[formOption.FormSelect.BaseId].Split(',').Contains(formOption.Value);
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
