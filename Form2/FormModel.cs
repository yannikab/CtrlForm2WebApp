using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Form2.Form.Content;
using Form2.Form.Enums;
using Form2.Form.Interfaces;
using Form2.Form.Visitors;

namespace Form2
{
    [SuppressMessage("Style", "IDE0019:Use pattern matching", Justification = "<Pending>")]
    [SuppressMessage("Style", "IDE0031:Use null propagation", Justification = "<Pending>")]

    public abstract class FormModel
    {
        #region Fields

        private FormGroup formGroup;

        private readonly Stack<FormGroup> formGroups;

        protected delegate void FormRule(bool isUpdate, FormItem source, string argument);

        private readonly List<FormRule> rules;

        private bool submitted;

        #endregion


        #region Properties

        public FormGroup FormGroup { get { return formGroup; } }

        public bool Submitted { get { return submitted; } }

        #endregion


        #region Constructors

        public FormModel()
        {
            formGroup = null;
            formGroups = new Stack<FormGroup>();
            rules = new List<FormRule>();
            submitted = true;
        }

        #endregion


        #region Methods

        public void Initialize()
        {
            formGroup = null;
            formGroups.Clear();
            rules.Clear();
            submitted = true;

            CreateForm();

            if (formGroup == null)
                return;

            AddRules(rules);

            ApplyRules(false, null, null);
        }

        public void Update(NameValueCollection values, FormItem source, string argument)
        {
            if (formGroup == null)
                return;

            submitted = false;

            ISubmit iSubmit = source as ISubmit;

            if (iSubmit != null)
                throw new ApplicationException();

            IUpdate iUpdate = source as IUpdate;

            if (iUpdate == null || !iUpdate.IsUpdate)
                throw new ApplicationException();

            new FormUpdateVisitor(formGroup, values, source, argument);

            ApplyRules(true, source, argument);

            new FormLastMessageVisitor(formGroup, false);
        }

        public void Submit(NameValueCollection values, FormItem source, string argument)
        {
            if (formGroup == null)
                return;

            submitted = false;

            new FormUpdateVisitor(formGroup, values, source, argument);

            ApplyRules(true, source, argument);

            ISubmit iSubmit = source as ISubmit;

            if (!iSubmit.IsSubmit)
                throw new ApplicationException();

            if (!formGroup.IsValid)
            {
                new FormLastMessageVisitor(formGroup, true);
                return;
            }

            PerformAction();

            Initialize();
        }

        protected abstract void CreateForm();

        protected void OpenGroup(string name)
        {
            FormGroup formGroup = new FormGroup(name);

            if (formGroups.Count == 0)
            {
                this.formGroup = formGroup;

                this.formGroup.RequiredMessage = "!";

                this.formGroup.RequiredMark = "*";
                this.formGroup.RequiredInLabel = true;
                this.formGroup.RequiredInPlaceholder = false;

                this.formGroup.OptionalMark = "...";
                this.formGroup.OptionalInLabel = false;
                this.formGroup.OptionalInPlaceholder = false;
                
                this.formGroup.OrderElements = OrderElements.InputLabelMark;
            }
            else
            {
                formGroups.Peek().Add(formGroup);
            }

            formGroups.Push(formGroup);
        }

        protected void CloseGroup(string name)
        {
            if (formGroups.Count == 0)
                throw new InvalidOperationException("No form group is currently open. Can not close group.");

            if (formGroups.Peek().Name != name)
                throw new InvalidOperationException("Attempting to close a different form group than the one currently open.");

            formGroups.Pop();
        }

        protected bool? Hidden
        {
            set
            {
                if (formGroups.Count == 0)
                    throw new InvalidOperationException("No form group is currently open. Can not set Hidden property.");

                formGroups.Peek().Hidden = value;
            }
        }

        protected bool IsHidden
        {
            get
            {
                if (formGroups.Count == 0)
                    throw new InvalidOperationException("No form group is currently open. Can not get Hidden property.");

                return formGroups.Peek().IsHidden;
            }
        }

        protected bool? ReadOnly
        {
            set
            {
                if (formGroups.Count == 0)
                    throw new InvalidOperationException("No form group is currently open. Can not set ReadOnly property.");

                formGroups.Peek().ReadOnly = value;
            }
        }

        protected bool IsReadOnly
        {
            get
            {
                if (formGroups.Count == 0)
                    throw new InvalidOperationException("No form group is currently open. Can not get ReadOnly property.");

                return formGroups.Peek().IsReadOnly;
            }
        }

        protected bool? Required
        {
            set
            {
                if (formGroups.Count == 0)
                    throw new InvalidOperationException("No form group is currently open. Can not set Required property.");

                formGroups.Peek().Required = value;
            }
        }

        protected bool IsRequired
        {
            get
            {
                if (formGroups.Count == 0)
                    throw new InvalidOperationException("No form group is currently open. Can not get Required property.");

                return formGroups.Peek().IsRequired;
            }
        }

        protected string RequiredMessage
        {
            get
            {
                if (formGroups.Count == 0)
                    throw new InvalidOperationException("No form group is currently open. Can not get RequiredMessage property.");

                return formGroups.Peek().RequiredMessage;
            }
            set
            {
                if (formGroups.Count == 0)
                    throw new InvalidOperationException("No form group is currently open. Can not set RequiredMessage property.");

                formGroups.Peek().RequiredMessage = value;
            }
        }

        protected string RequiredMark
        {
            get
            {
                if (formGroups.Count == 0)
                    throw new InvalidOperationException("No form group is currently open. Can not get RequiredMark property.");

                return formGroups.Peek().RequiredMark;
            }
            set
            {
                if (formGroups.Count == 0)
                    throw new InvalidOperationException("No form group is currently open. Can not set RequiredMark property.");

                formGroups.Peek().RequiredMark = value;
            }
        }

        public bool? RequiredInLabel
        {
            set
            {
                if (formGroups.Count == 0)
                    throw new InvalidOperationException("No form group is currently open. Can not set RequiredInLabel property.");

                formGroups.Peek().RequiredInLabel = value;
            }
        }

        public bool IsRequiredInLabel
        {
            get
            {
                if (formGroups.Count == 0)
                    throw new InvalidOperationException("No form group is currently open. Can not get IsRequiredInLabel property.");

                return formGroups.Peek().IsRequiredInLabel;
            }
        }

        public bool? RequiredInPlaceholder
        {
            set
            {
                if (formGroups.Count == 0)
                    throw new InvalidOperationException("No form group is currently open. Can not set RequiredInPlaceholder property.");

                formGroups.Peek().RequiredInPlaceholder = value;
            }
        }

        public bool IsRequiredInPlaceholder
        {
            get
            {
                if (formGroups.Count == 0)
                    throw new InvalidOperationException("No form group is currently open. Can not get IsRequiredInPlaceholder property.");

                return formGroups.Peek().IsRequiredInPlaceholder;
            }
        }

        protected string OptionalMark
        {
            get
            {
                if (formGroups.Count == 0)
                    throw new InvalidOperationException("No form group is currently open. Can not get OptionalMark property.");

                return formGroups.Peek().OptionalMark;
            }
            set
            {
                if (formGroups.Count == 0)
                    throw new InvalidOperationException("No form group is currently open. Can not set OptionalMark property.");

                formGroups.Peek().OptionalMark = value;
            }
        }

        public bool? OptionalInLabel
        {
            set
            {
                if (formGroups.Count == 0)
                    throw new InvalidOperationException("No form group is currently open. Can not set OptionalInLabel property.");

                formGroups.Peek().OptionalInLabel = value;
            }
        }

        public bool IsOptionalInLabel
        {
            get
            {
                if (formGroups.Count == 0)
                    throw new InvalidOperationException("No form group is currently open. Can not get IsOptionalInLabel property.");

                return formGroups.Peek().IsOptionalInLabel;
            }
        }

        public bool? OptionalInPlaceholder
        {
            set
            {
                if (formGroups.Count == 0)
                    throw new InvalidOperationException("No form group is currently open. Can not set OptionalInPlaceholder property.");

                formGroups.Peek().OptionalInPlaceholder = value;
            }
        }

        public bool IsOptionalInPlaceholder
        {
            get
            {
                if (formGroups.Count == 0)
                    throw new InvalidOperationException("No form group is currently open. Can not get IsOptionalInPlaceholder property.");

                return formGroups.Peek().IsOptionalInPlaceholder;
            }
        }

        protected OrderElements OrderElements
        {
            get
            {
                if (formGroups.Count == 0)
                    throw new InvalidOperationException("No form group is currently open. Can not get OrderElements property.");

                return formGroups.Peek().OrderElements;
            }
            set
            {
                if (formGroups.Count == 0)
                    throw new InvalidOperationException("No form group is currently open. Can not set OrderElements property.");

                formGroups.Peek().OrderElements = value;
            }
        }

        protected void AddItem<T>(T formItem) where T : FormItem
        {
            if (formGroups.Count == 0)
                throw new InvalidOperationException("No form group is currently open. Can not add form item.");

            formGroups.Peek().Add(formItem);
        }

        public T GetItem<T>(string path) where T : FormItem
        {
            if (path == null)
                throw new ArgumentNullException();

            if (formGroup == null)
                throw new InvalidOperationException("No form container exists. Can not get any form item.");

            return formGroup.Get<T>(path);
        }

        public FormItem GetItem(string path)
        {
            if (path == null)
                throw new ArgumentNullException();

            if (formGroup == null)
                throw new InvalidOperationException("No form container exists. Can not get any form item.");

            return formGroup.Get(path);
        }

        protected virtual void AddRules(List<FormRule> rules)
        {
        }

        protected virtual void ApplyRules(bool isUpdate, FormItem source, string argument)
        {
            foreach (var r in rules)
                r(isUpdate, source, argument);
        }

        protected abstract void PerformAction();

        #endregion
    }
}
