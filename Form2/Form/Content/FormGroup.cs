using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Form2.Form.Enums;
using Form2.Form.Interfaces;

namespace Form2.Form.Content
{
    [SuppressMessage("Style", "IDE0019:Use pattern matching", Justification = "<Pending>")]
    [SuppressMessage("Style", "IDE0034:Simplify 'default' expression", Justification = "<Pending>")]

    public class FormGroup : FormContent, IDisabled, IRequired, IReadOnly
    {
        #region Fields

        private readonly List<FormContent> contents;

        private ElementOrder elementOrder;

        private bool? disabled;

        private bool? required;

        private string requiredMark;

        private string requiredMessage;

        private bool? readOnly;

        #endregion


        #region Properties

        public IReadOnlyList<FormContent> Contents
        {
            get { return contents; }
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

        public IEnumerable<T> Get<T>()
        {
            foreach (var f in Contents.OfType<T>())
                if (f is T)
                    yield return f;

            foreach (var g in Contents.OfType<FormGroup>())
            {
                foreach (var f in g.Get<T>())
                    yield return f;
            }
        }

        public T Get<T>(string baseId) where T : FormItem
        {
            foreach (var f in Contents.OfType<T>())
                if (f.BaseId == baseId)
                    return f;

            foreach (var g in Contents.OfType<FormGroup>())
            {
                T formItem = g.Get<T>(baseId);

                if (formItem != default(T))
                    return formItem;
            }

            return default(T);
        }

        public FormItem Get(string baseId)
        {
            foreach (var f in Contents.OfType<FormItem>())
                if (f.BaseId == baseId)
                    return f;

            foreach (var g in Contents.OfType<FormGroup>())
            {
                FormItem formItem = g.Get(baseId);

                if (formItem != null)
                    return formItem;
            }

            return null;
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

        public bool? Required
        {
            set { required = value; }
        }

        public bool IsRequired
        {
            get
            {
                if (IsDisabled)
                    return false;

                if (IsHidden)
                    return false;

                if (required.HasValue)
                    return required.Value;

                if (Container == null)
                    return false;

                return Container.IsRequired;
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

        public virtual bool IsRequiredMet
        {
            get { return Contents.OfType<IRequired>().All(c => c.IsRequiredMet); }
        }

        #endregion


        #region IReadOnly

        public bool? ReadOnly
        {
            set { readOnly = value; }
        }

        public bool IsReadOnly
        {
            get
            {
                if (IsDisabled)
                    return false;

                if (IsHidden)
                    return false;

                if (readOnly.HasValue)
                    return readOnly.Value;

                if (Container == null)
                    return false;

                return Container.IsReadOnly;
            }
        }

        #endregion


        #region 

        public bool IsValid
        {
            get
            {
                foreach (var c in Contents)
                {
                    if (c is FormGroup)
                        continue;

                    if (c is IHidden && (c as IHidden).IsHidden)
                        continue;

                    if (c is IDisabled && (c as IDisabled).IsDisabled)
                        continue;

                    if (c is IReadOnly && (c as IReadOnly).IsReadOnly)
                        continue;

                    if (c is IValidate == false)
                        continue;

                    if ((c as IValidate).IsValid == false)
                        return false;
                }

                foreach (var c in Contents.OfType<FormGroup>())
                {
                    if (!c.IsValid)
                        return false;
                }

                return true;
            }
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
            return string.Format("{0} (BaseId: '{1}', FormId: '{2}')", GetType().Name, BaseId, FormId);
        }

        #endregion
    }
}
