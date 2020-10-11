using System;
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

    public abstract class FormInput : FormItem, IDisabled, IRequired, IValidate
    {
        #region Fields

        private string label;

        private OrderElements orderElements;

        private bool? disabled;

        private bool? required;

        private string requiredMessage;

        private string requiredMark;

        private bool? requiredInLabel;

        private bool? requiredInPlaceholder;

        private string optionalMark;

        private bool? optionalInLabel;

        private bool? optionalInPlaceholder;

        private string lastMessage;

        private bool useLastMessage;

        #endregion


        #region Properties

        public abstract bool HasValue
        {
            get;
        }

        public string Label
        {
            get { return label; }
            set { label = value; }
        }

        public OrderElements OrderElements
        {
            get
            {
                if (orderElements != OrderElements.NotSet)
                    return orderElements;

                FormGroup container = Container as FormGroup;

                if (container == null)
                    return OrderElements.NotSet;

                return container.OrderElements;
            }
            set
            {
                orderElements = value;
            }
        }

        public bool IsLabeled
        {
            get { return !string.IsNullOrWhiteSpace(label); }
        }

        public bool IsMarkedRequired
        {
            get { return IsLabeled && !string.IsNullOrWhiteSpace(RequiredMark) && IsRequiredInLabel; }
        }

        public bool IsMarkedOptional
        {
            get { return IsLabeled && !string.IsNullOrWhiteSpace(OptionalMark) && IsOptionalInLabel; }
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


        #region IRequired

        public virtual bool? Required
        {
            set { required = value; }
        }

        public virtual bool IsRequired
        {
            get
            {
                // a user can not be expected to fill out an input element that is hidden
                if (IsHidden)
                    return false;

                // a user can not be expected to fill out an input element that is disabled
                if (IsDisabled)
                    return false;

                if (required.HasValue)
                    return required.Value;

                FormGroup container = Container as FormGroup;

                if (container == null)
                    return false;

                return container.IsRequired;
            }
        }

        public string RequiredMessage
        {
            get
            {
                if (requiredMessage != null)
                    return requiredMessage;

                if (Container == null)
                    return null;

                return Container.RequiredMessage;
            }
            set
            {
                requiredMessage = value;
            }
        }

        public string RequiredMark
        {
            get
            {
                if (requiredMark != null)
                    return requiredMark;

                FormGroup container = Container as FormGroup;

                if (container == null)
                    return null;

                return container.RequiredMark;
            }
            set
            {
                requiredMark = value;
            }
        }

        public bool? RequiredInLabel
        {
            set { requiredInLabel = value; }
        }

        public bool IsRequiredInLabel
        {
            get
            {
                if (requiredInLabel.HasValue)
                    return requiredInLabel.Value;

                FormGroup container = Container as FormGroup;

                if (container == null)
                    return true;

                return container.IsRequiredInLabel;
            }
        }

        public bool? RequiredInPlaceholder
        {
            set { requiredInPlaceholder = value; }
        }

        public bool IsRequiredInPlaceholder
        {
            get
            {
                if (requiredInPlaceholder.HasValue)
                    return requiredInPlaceholder.Value;

                FormGroup container = Container as FormGroup;

                if (container == null)
                    return true;

                return container.IsRequiredInPlaceholder;
            }
        }

        public string OptionalMark
        {
            get
            {
                if (optionalMark != null)
                    return optionalMark;

                FormGroup container = Container as FormGroup;

                if (container == null)
                    return null;

                return container.OptionalMark;
            }
            set
            {
                optionalMark = value;
            }
        }

        public bool? OptionalInLabel
        {
            set { optionalInLabel = value; }
        }

        public bool IsOptionalInLabel
        {
            get
            {
                if (optionalInLabel.HasValue)
                    return optionalInLabel.Value;

                FormGroup container = Container as FormGroup;

                if (container == null)
                    return true;

                return container.IsOptionalInLabel;
            }
        }

        public bool? OptionalInPlaceholder
        {
            set { optionalInPlaceholder = value; }
        }

        public bool IsOptionalInPlaceholder
        {
            get
            {
                if (optionalInPlaceholder.HasValue)
                    return optionalInPlaceholder.Value;

                FormGroup container = Container as FormGroup;

                if (container == null)
                    return true;

                return container.IsOptionalInPlaceholder;
            }
        }

        public string LastMessage
        {
            get { return lastMessage; }
            set { lastMessage = value; }
        }

        public bool UseLastMessage
        {
            get { return useLastMessage; }
            set { useLastMessage = value; }
        }

        #endregion


        #region IValidate

        public abstract string ValidationMessage
        {
            get;
        }

        public abstract bool IsValid
        {
            get;
        }

        #endregion


        #region Constructors

        public FormInput(string name)
            : base(name)
        {
            label = null;

            orderElements = OrderElements.NotSet;

            lastMessage = null;
            useLastMessage = false;
        }

        #endregion
    }


    public abstract class FormInput<C, V> : FormInput, IValidate<V>
    {
        #region Fields

        private C content;

        private Func<V, string> validator;

        private Action<V> actionInvalid;

        #endregion


        #region Properties

        public virtual C Content
        {
            get { return content; }
            set
            {
                if (value == null)
                    throw new ArgumentNullException();

                content = value;
            }
        }

        public abstract V Value
        {
            get;
        }

        #endregion


        #region IValidate<V>

        public override string ValidationMessage
        {
            get { return Validator(Value); }
        }

        public Func<V, string> Validator
        {
            get { return validator; }
            set { validator = value; }
        }

        public Action<V> ActionInvalid
        {
            get { return actionInvalid; }
            set { actionInvalid = value; }
        }

        #endregion


        #region Constructors

        public FormInput(string name)
            : base(name)
        {
            validator = (v) => { return null; };
            actionInvalid = (v) => { return; };
        }

        #endregion
    }
}
