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

        private OrderElements orderElements;

        private bool? disabled;

        private bool? required;

        private string requiredMark;

        private string requiredMessage;

        private bool? requiredInLabel;

        private bool? requiredInPlaceholder;

        private string optionalMark;

        private bool? optionalInLabel;

        private bool? optionalInPlaceholder;

        private bool? readOnly;

        private bool? directInput;

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

            foreach (var g in Contents.OfType<FormGroup>())
            {
                foreach (var f in g.Get<T>())
                    yield return f;
            }
        }

        public T Get<T>(string path) where T : FormItem
        {
            foreach (var f in Contents.OfType<T>())
                if (f.Path == path)
                    return f;

            foreach (var g in Contents.OfType<FormGroup>())
            {
                T formItem = g.Get<T>(path);

                if (formItem != default(T))
                    return formItem;
            }

            return default(T);
        }

        public FormItem Get(string path)
        {
            foreach (var f in Contents.OfType<FormItem>())
                if (f.Path == path)
                    return f;

            foreach (var g in Contents.OfType<FormGroup>())
            {
                FormItem formItem = g.Get(path);

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

                if (Container == null)
                    return null;

                return Container.RequiredMark;
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

        public bool? DirectInput
        {
            set { directInput = value; }
        }

        public bool IsDirectInput
        {
            get
            {
                if (IsReadOnly)
                    return false;

                if (directInput.HasValue)
                    return directInput.Value;

                FormGroup container = Container as FormGroup;

                if (container == null)
                    return false;

                return container.IsDirectInput;
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

                foreach (var s in Contents.OfType<FormGroup>())
                {
                    if (!s.IsValid)
                        return false;
                }

                return true;
            }
        }

        #endregion


        #region Constructors

        public FormGroup(string name)
            : base(name)
        {
            contents = new List<FormContent>();

            requiredMark = null;
            orderElements = OrderElements.NotSet;
        }

        #endregion


        #region Object

        public override string ToString()
        {
            return string.Format("{0} (Name: '{1}', Path: '{2}')", GetType().Name, Name, Path);
        }

        #endregion
    }
}
