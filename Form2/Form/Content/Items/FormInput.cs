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

    public abstract class FormInput<C, V> : FormItem, IDisabled, IRequired
    {
        #region Fields

        private string label;

        private OrderElements orderElements;

        private C content;

        private bool? disabled;

        private bool? required;

        private string requiredMessage;

        private string requiredMark;

        private bool? requiredInLabel;

        private bool? requiredInPlaceholder;

        private string optionalMark;

        private bool? optionalInLabel;

        private bool? optionalInPlaceholder;

        private string lastValidationMessage;

        private bool useLastValidationMessage;

        #endregion


        #region Properties

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

                FormSection container = Container as FormSection;

                if (container == null)
                    return OrderElements.NotSet;

                return container.OrderElements;
            }
            set
            {
                orderElements = value;
            }
        }

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

        public abstract bool HasValue
        {
            get;
        }

        public string LastMessage
        {
            get { return lastValidationMessage; }
            set { lastValidationMessage = value; }
        }

        public bool UseLastMessage
        {
            get { return useLastValidationMessage; }
            set { useLastValidationMessage = value; }
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

                FormSection container = Container as FormSection;

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

                FormSection container = Container as FormSection;

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

                FormSection container = Container as FormSection;

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

                FormSection container = Container as FormSection;

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

                FormSection container = Container as FormSection;

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

                FormSection container = Container as FormSection;

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

                FormSection container = Container as FormSection;

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

                FormSection container = Container as FormSection;

                if (container == null)
                    return true;

                return container.IsOptionalInPlaceholder;
            }
        }

        #endregion


        #region Constructors

        public FormInput(string baseId, string formId, string label)
            : base(baseId, formId)
        {
            this.label = label;

            disabled = null;

            required = null;
            requiredMark = null;
            requiredMessage = null;

            lastValidationMessage = null;
            useLastValidationMessage = false;

            orderElements = OrderElements.NotSet;
        }

        public FormInput(string baseId, string formId)
            : this(baseId, formId, "")
        {
        }

        #endregion
    }
}
