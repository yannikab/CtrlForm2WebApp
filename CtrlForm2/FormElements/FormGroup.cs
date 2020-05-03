using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using UserControls.CtrlForm2.FormElements.FormItems;
using UserControls.CtrlForm2.FormElements.FormItems.FormItemsInput;
using UserControls.CtrlForm2.Interfaces;

namespace UserControls.CtrlForm2.FormElements
{
    public class FormGroup : FormItem, IRequired
    {
        #region Fields

        private readonly List<FormItem> items;

        private bool? isReadOnly;

        private bool? isRequired;

        private string requiredMark;

        private string requiredMessage;

        private ElementOrder elementOrder;

        #endregion


        #region Properties

        public IReadOnlyList<FormItem> Items
        {
            get { return items; }
        }

        public bool? IsReadOnly
        {
            get
            {
                if (isReadOnly.HasValue)
                    return isReadOnly.Value;

                if (Group == null)
                    return null;

                return Group.IsReadOnly;
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

                if (Group == null)
                    return ElementOrder.NotSet;

                return Group.ElementOrder;
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

                if (Group == null)
                    return null;

                return Group.IsRequired;
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

                if (Group == null)
                    return null;

                return Group.RequiredMark;
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

                if (Group == null)
                    return null;

                return Group.RequiredMessage;
            }
            set
            {
                requiredMessage = value;
            }
        }

        public bool IsEntered
        {
            get { return Items.OfType<FormItemInput>().Any(i => i.IsEntered); }
        }

        #endregion


        #region Methods

        public void Add(FormItem item)
        {
            if (items.Contains(item))
                return;

            items.Add(item);

            item.Group = this;
        }

        public bool Remove(FormItem item)
        {
            if (!items.Contains(item))
                return false;

            item.Group = null;

            return items.Remove(item);
        }

        #endregion


        #region Constructors

        public FormGroup(string baseId, string formId)
            : base(baseId, formId)
        {
            items = new List<FormItem>();

            requiredMark = null;
            elementOrder = ElementOrder.NotSet;
        }

        public FormGroup(string baseId)
            : this(baseId.Replace("-", ""), baseId.ToLower())
        {
        }

        #endregion


        #region Object

        public override string ToString()
        {
            return string.Format("{0}: {1}, FormId: {2}", GetType().Name, BaseId, FormId);
        }

        #endregion
    }
}
