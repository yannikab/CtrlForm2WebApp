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

    public class FormSection : FormContent, IDisabled, IRequired, IReadOnly
    {
        #region Fields

        private readonly List<FormContent> contents;

        private OrderElements orderElements;

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

        public OrderElements OrderElements
        {
            get
            {
                if (orderElements != OrderElements.NotSet)
                    return orderElements;

                if (Container == null)
                    return OrderElements.NotSet;

                return Container.OrderElements;
            }

            set
            {
                orderElements = value;
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

            foreach (var s in Contents.OfType<FormSection>())
            {
                foreach (var f in s.Get<T>())
                    yield return f;
            }
        }

        public T Get<T>(string baseId) where T : FormItem
        {
            foreach (var f in Contents.OfType<T>())
                if (f.BaseId == baseId)
                    return f;

            foreach (var s in Contents.OfType<FormSection>())
            {
                T formItem = s.Get<T>(baseId);

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

            foreach (var s in Contents.OfType<FormSection>())
            {
                FormItem formItem = s.Get(baseId);

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

                FormSection container = Container as FormSection;

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
                if (IsHidden)
                    return false; 
                
                if (IsDisabled)
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
                if (IsHidden)
                    return false; 
                
                if (IsDisabled)
                    return false;

                if (readOnly.HasValue)
                    return readOnly.Value;

                if (Container == null)
                    return false;

                return Container.IsReadOnly;
            }
        }

        #endregion


        #region IsValid

        public bool IsValid
        {
            get
            {
                foreach (var c in Contents)
                {
                    if (c is FormSection)
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

                foreach (var s in Contents.OfType<FormSection>())
                {
                    if (!s.IsValid)
                        return false;
                }

                return true;
            }
        }

        #endregion


        #region Constructors

        public FormSection(string baseId, string formId)
            : base(baseId.Replace("-", ""), formId)
        {
            contents = new List<FormContent>();

            requiredMark = null;
            orderElements = OrderElements.NotSet;
        }

        public FormSection(string baseId)
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
