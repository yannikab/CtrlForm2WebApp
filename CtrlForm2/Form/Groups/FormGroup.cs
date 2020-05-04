using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using CtrlForm2.Form.Enums;
using CtrlForm2.Form.Interfaces;
using CtrlForm2.Form.Items;
using CtrlForm2.Form.Items.Input;

namespace CtrlForm2.Form.Groups
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

                if (Container == null)
                    return null;

                return Container.IsReadOnly;
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

                if (Container == null)
                    return ElementOrder.NotSet;

                return Container.ElementOrder;
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

                if (Container == null)
                    return null;

                return Container.IsRequired;
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

                if (Container == null)
                    return null;

                return Container.RequiredMark;
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

        public virtual bool IsEntered
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

            item.Container = this;
        }

        public bool Remove(FormItem item)
        {
            if (!items.Contains(item))
                return false;

            bool removed = items.Remove(item);

            if (removed)
                item.Container = null;

            return removed;
        }

        #endregion


        #region Constructors

        public FormGroup(string baseId, string formId)
            : base(baseId.Replace("-", ""), formId)
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
