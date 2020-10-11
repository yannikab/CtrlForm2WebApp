﻿using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Form2.Form.Interfaces;

namespace Form2.Form.Content.Items
{
    [SuppressMessage("Style", "IDE0019:Use pattern matching", Justification = "<Pending>")]

    public class FormButton : FormItem, IDisabled, ISubmit
    {
        #region Fields

        private string content;

        private bool? disabled;

        private bool submit;

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

        public bool? Disabled
        {
            set { disabled = value; }
        }

        public bool IsDisabled
        {
            get
            {
                if (disabled.HasValue)
                    return disabled.Value;

                FormGroup container = Container as FormGroup;

                if (container == null)
                    return false;

                return container.IsDisabled;
            }
        }

        #endregion


        #region ISubmit

        public bool Submit
        {
            get { return submit; }
            set { submit = value; }
        }

        #endregion


        #region Constructors

        public FormButton(string name)
            : base(name)
        {
            content = "";

            disabled = null;

            submit = false;
        }

        #endregion


        #region Object

        public override string ToString()
        {
            return string.Format("{0} (Name: '{1}', Value: '{2}')", GetType().Name, Name, Value);
        }

        #endregion
    }
}
