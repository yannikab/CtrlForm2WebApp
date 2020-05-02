using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using UserControls.CtrlForm2.FormElements;
using UserControls.CtrlForm2.FormElements.FormItems;
using UserControls.CtrlForm2.FormElements.FormItems.FormItemsInput;

namespace UserControls.CtrlForm2.Visitors
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
