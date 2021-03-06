﻿using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Form2.Form.Enums;
using Form2.Form.Interfaces;

namespace Form2.Form.Content.Items
{
    [SuppressMessage("Style", "IDE0019:Use pattern matching", Justification = "<Pending>")]

    public class FormButton : FormItem, IDisabled, IUpdate, ISubmit
    {
        #region Fields

        private string content;

        private bool? disabled;

        private bool update;

        private bool submit;

        private string parameter;

        private ButtonType type;

        private ConfirmationType confirmationType;

        private string confirmationMessage;

        #endregion


        #region Properties

        public string Content
        {
            get { return content; }
            set { content = value; }
        }

        public string Value
        {
            get { return content ?? string.Empty; }
        }

        public string Parameter
        {
            get { return parameter ?? string.Empty; }
        }

        public ButtonType Type
        {
            get { return type; }
            set { type = value; }
        }

        public ConfirmationType ConfirmationType
        {
            get { return confirmationType; }
            set { confirmationType = value; }
        }

        public string ConfirmationMessage
        {
            get { return confirmationMessage; }
            set { confirmationMessage = value; }
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


        #region IUpdate

        public bool Update
        {
            get { return update; }
            set { update = value; }
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

        public FormButton(string name, string parameter)
            : base(name)
        {
            this.content = string.Empty;

            this.disabled = null;

            this.update = false;
            this.submit = false;
            this.parameter = parameter;
            this.type = ButtonType.NotSet;
            this.confirmationType = ConfirmationType.NotSet;
            this.confirmationMessage = string.Empty;
        }

        public FormButton(string name)
            : this(name, string.Empty)
        {
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
