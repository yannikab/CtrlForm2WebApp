using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

using Form2.Form.Content;
using Form2.Form.Content.Items;
using Form2.Form.Interfaces;

namespace Form2.Form.Visitors
{
    public class FormLastMessageVisitor
    {
        #region Fields

        private static readonly MethodInfo[] visitorMethods;

        private readonly bool submit;

        #endregion


        #region Methods

        public void Visit(FormContent formContent)
        {
            var mi = visitorMethods.SingleOrDefault(m => formContent.GetType().Equals(m.GetParameters()[0].ParameterType));

            if (mi == null)
                mi = visitorMethods.SingleOrDefault(m => formContent.GetType().IsSubclassOf(m.GetParameters()[0].ParameterType));

            if (mi != null)
                mi.Invoke(this, new object[] { formContent });
        }

        public virtual void Visit(FormGroup formGroup)
        {
            foreach (var i in formGroup.Contents)
                Visit(i);
        }

        public virtual void Visit(FormInput formInput)
        {
            if (submit)
                formInput.LastMessage = formInput.IsRequired && !formInput.HasValue ? formInput.RequiredMessage : formInput.ValidationMessage;

            formInput.UseLastMessage = 
                !submit &&
                !formInput.IsHidden &&
                !formInput.IsDisabled &&
                (!(formInput is IReadOnly) || !(formInput as IReadOnly).IsReadOnly);
        }

        #endregion


        #region Constructors

        static FormLastMessageVisitor()
        {
            visitorMethods = (from mi in typeof(FormLastMessageVisitor).GetMethods()
                              where
                              mi.Name == "Visit" &&
                              mi.ReturnType.Equals(typeof(void)) &&
                              mi.GetParameters().Length == 1 &&
                              mi.GetParameters()[0].ParameterType.Equals(typeof(FormContent)) == false
                              select mi).ToArray();
        }

        public FormLastMessageVisitor(FormGroup formGroup, bool submit)
        {
            this.submit = submit;

            Visit(formGroup);
        }

        #endregion
    }
}
