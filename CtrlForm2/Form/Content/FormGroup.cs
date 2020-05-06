using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using CtrlForm2.Form.Content.Items;
using CtrlForm2.Form.Enums;
using CtrlForm2.Form.Interfaces;

namespace CtrlForm2.Form.Content
{
    public class FormGroup : FormContent, IRequired
    {
        #region Fields

        private readonly List<FormContent> contents;

        private bool? isReadOnly;

        private bool? isRequired;

        private string requiredMark;

        private string requiredMessage;

        private ElementOrder elementOrder;

        #endregion


        #region Properties

        public IReadOnlyList<FormContent> Contents
        {
            get { return contents; }
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
            get { return Contents.OfType<FormInput>().Any(i => i.IsEntered); }
        }

        #endregion


        #region Methods

        public void Add(FormContent c)
        {
            if (contents.Contains(c))
                return;

            contents.Add(c);

            c.Container = this;
        }

        public bool Remove(FormContent c)
        {
            if (!contents.Contains(c))
                return false;

            bool removed = contents.Remove(c);

            if (removed)
                c.Container = null;

            return removed;
        }

        public T Get<T>(string baseId) where T : FormItem
        {
            foreach (var item in Contents.OfType<T>())
                if (item.BaseId == baseId)
                    return item;

            foreach (var g in Contents.OfType<FormGroup>())
            {
                T formItem = g.Get<T>(baseId);

                if (formItem != default(T))
                    return formItem;
            }

            return default;
        }

        #endregion


        #region Constructors

        public FormGroup(string baseId, string formId)
            : base(baseId.Replace("-", ""), formId)
        {
            contents = new List<FormContent>();

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
