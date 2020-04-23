using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using UserControls.CtrlForm2.Interfaces;

namespace UserControls.CtrlForm2.FormElements.FormItems.FormItemsInput
{
    [SuppressMessage("Style", "IDE0019:Use pattern matching", Justification = "<Pending>")]

    public abstract class FormItemInput : FormItem, IRequired
    {
        #region Fields

        private string label;

        private bool isReadOnly;

        private bool isRequired;

        private string requiredMark;

        private ElementOrder elementOrder;

        #endregion


        #region Properties

        public string Label
        {
            get { return label; }
            set { label = value; }
        }

        public bool IsReadOnly
        {
            get { return isReadOnly; }
            set { isReadOnly = value; }
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

        public bool IsRequired
        {
            get { return isRequired; }
            set { isRequired = value; }
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

        #endregion


        public FormItemInput(string baseId, string formId)
            : base(baseId, formId)
        {
            label = "";
            requiredMark = null;
            elementOrder = ElementOrder.NotSet;
        }
    }
}
