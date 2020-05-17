using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Form2.Form.Interfaces;

namespace Form2.Form.Content.Items
{
    [SuppressMessage("Style", "IDE0019:Use pattern matching", Justification = "<Pending>")]

    public class FormSubmit : FormItem, IDisabled, IPostBack
    {
        #region Fields

        private string content;

        private bool? isDisabled;

        private bool isPostBack;

        #endregion


        #region Properties

        public string Content
        {
            get { return content; }
            set { content = value; }
        }

        public string Value
        {
            get { return content ?? ""; }
        }

        #endregion


        #region IDisabled

        public bool? IsDisabled
        {
            get
            {
                if (isDisabled.HasValue)
                    return isDisabled.Value;

                FormGroup container = Container as FormGroup;

                if (container == null)
                    return null;

                return container.IsDisabled;
            }
            set
            {
                isDisabled = value;
            }
        }

        #endregion


        #region IPostBack

        public bool IsPostBack
        {
            get { return isPostBack; }
            set { isPostBack = value; }
        }

        #endregion


        #region Constructors

        public FormSubmit(string baseId, string formId)
            : base(baseId, formId)
        {
            Content = "";

            IsDisabled = null;
        }

        public FormSubmit(string baseId)
            : this(baseId, baseId.ToLower())
        {
        }

        #endregion


        #region Object

        public override string ToString()
        {
            return string.Format("{0} (BaseId: '{1}', Value: '{2}')", GetType().Name, BaseId, Value);
        }

        #endregion
    }
}
