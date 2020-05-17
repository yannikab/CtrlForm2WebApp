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

        private ElementOrder elementOrder;

        private C content;

        private bool? isDisabled;

        private bool? isRequired;

        private string requiredMark;

        private string requiredMessage;

        #endregion


        #region Properties

        public string Label
        {
            get { return label; }
            set { label = value; }
        }

        public ElementOrder ElementOrder
        {
            get
            {
                if (elementOrder != ElementOrder.NotSet)
                    return elementOrder;

                FormGroup container = Container as FormGroup;

                if (container == null)
                    return ElementOrder.NotSet;

                return container.ElementOrder;
            }
            set
            {
                elementOrder = value;
            }
        }

        public virtual C Content
        {
            get { return content; }
            set { content = value; }
        }

        public abstract V Value
        {
            get;
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


        #region IRequired

        public virtual bool? IsRequired
        {
            get
            {
                // a user can not be expected to fill out an input element that is disabled
                if (IsDisabled ?? false)
                    return false;

                // a user can not be expected to fill out an input element that is hidden
                if (IsHidden ?? false)
                    return false;

                if (isRequired.HasValue)
                    return isRequired.Value;

                FormGroup container = Container as FormGroup;

                if (container == null)
                    return null;

                return container.IsRequired;
            }
            set
            {
                isRequired = value;
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

        public abstract bool IsRequiredMet
        {
            get;
        }

        #endregion


        #region Constructors

        public FormInput(string baseId, string formId, string label)
            : base(baseId, formId)
        {
            Label = label;

            IsDisabled = null;

            IsRequired = null;
            RequiredMark = null;
            RequiredMessage = null;

            ElementOrder = ElementOrder.NotSet;
        }

        public FormInput(string baseId, string formId)
            : this(baseId, formId, "")
        {
        }

        #endregion
    }
}
