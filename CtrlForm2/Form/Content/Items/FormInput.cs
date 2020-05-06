using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using CtrlForm2.Form.Enums;
using CtrlForm2.Form.Interfaces;

namespace CtrlForm2.Form.Content.Items
{
    [SuppressMessage("Style", "IDE0019:Use pattern matching", Justification = "<Pending>")]

    public abstract class FormInput : FormItem, IRequired
    {
        #region Fields

        private string label;

        private bool? isReadOnly;

        private bool? isRequired;

        private string requiredMark;

        private string requiredMessage;

        private ElementOrder elementOrder;

        #endregion


        #region Properties

        public string Label
        {
            get { return label; }
            set { label = value; }
        }

        public bool? IsReadOnly
        {
            get
            {
                if (isReadOnly.HasValue)
                    return isReadOnly.Value;

                FormGroup container = Container as FormGroup;

                if (container == null)
                    return null;

                return container.IsReadOnly;
            }

            set
            {
                isReadOnly = value;
            }
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

        #endregion


        #region IRequired

        public bool? IsRequired
        {
            get
            {
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

        public abstract bool IsEntered
        {
            get;
        }

        #endregion


        #region Constructors

        public FormInput(string baseId, string formId, string label)
            : base(baseId, formId)
        {
            this.label = label;
            requiredMark = null;
            elementOrder = ElementOrder.NotSet;
        }

        public FormInput(string baseId, string formId)
            : this(baseId, formId, "")
        {
        }

        #endregion
    }
}
