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

        private FormSection formSection;

        private readonly Stack<FormSection> formSections;

        protected delegate void FormRule(bool isPostBack, FormItem formItem, string argument);

        private readonly List<FormRule> rules;

        private bool submitted;

        #endregion


        #region Properties

        public FormSection FormSection { get { return formSection; } }

        public bool Submitted { get { return submitted; } }

        #endregion


        #region Constructors

        public FormModel()
        {
            formSection = null;
            formSections = new Stack<FormSection>();
            rules = new List<FormRule>();
            submitted = true;
        }

        #endregion


        #region Methods

        public void Initialize()
        {
            formSection = null;
            formSections.Clear();
            rules.Clear();
            submitted = true;

            CreateForm();

            if (formSection == null)
                return;

            AddRules(rules);

            ApplyRules(false, null, null);
        }

        public void Update(NameValueCollection values, FormItem source, string argument)
        {
            if (formSection == null)
                return;

            submitted = false;

            ISubmit iSubmit = source as ISubmit;

            if (iSubmit != null)
                throw new ApplicationException();

            IPostBack iPostBack = source as IPostBack;

            if (iPostBack == null || !iPostBack.IsPostBack)
                throw new ApplicationException();

            new FormPostBackVisitor(formSection, values, source, argument);

            ApplyRules(true, source, argument);

            new FormMessageVisitor(formSection, false);
        }

        public void Submit(NameValueCollection values, FormItem source, string argument)
        {
            if (formSection == null)
                return;

            submitted = false;

            new FormPostBackVisitor(formSection, values, source, argument);

            ApplyRules(true, source, argument);

            ISubmit iSubmit = source as ISubmit;

            if (!iSubmit.IsSubmit)
                throw new ApplicationException();

            if (!formSection.IsValid)
            {
                new FormMessageVisitor(formSection, true);
                return;
            }

            PerformAction();

            Initialize();
        }

        protected abstract void CreateForm();

        protected void OpenSection(string baseId)
        {
            FormSection formSection = new FormSection(baseId);

            if (formSections.Count == 0)
            {
                this.formSection = formSection;

                this.formSection.RequiredMessage = "!";

                this.formSection.RequiredMark = "*";
                this.formSection.RequiredInLabel = true;
                this.formSection.RequiredInPlaceholder = false;

                this.formSection.OptionalMark = "...";
                this.formSection.OptionalInLabel = false;
                this.formSection.OptionalInPlaceholder = false;
                
                this.formSection.OrderElements = OrderElements.InputLabelMark;
            }
            else
            {
                formSections.Peek().Add(formSection);
            }

            formSections.Push(formSection);
        }

        protected void CloseSection(string baseId)
        {
            if (formSections.Count == 0)
                throw new InvalidOperationException("No form section is currently open. Can not close section.");

            if (formSections.Peek().BaseId != baseId)
                throw new InvalidOperationException("Attempting to close a different form section than the one currently open.");

            formSections.Pop();
        }

        protected bool? Hidden
        {
            set
            {
                if (formSections.Count == 0)
                    throw new InvalidOperationException("No form section is currently open. Can not set Hidden property.");

                formSections.Peek().Hidden = value;
            }
        }

        protected bool IsHidden
        {
            get
            {
                if (formSections.Count == 0)
                    throw new InvalidOperationException("No form section is currently open. Can not get Hidden property.");

                return formSections.Peek().IsHidden;
            }
        }

        protected bool? ReadOnly
        {
            set
            {
                if (formSections.Count == 0)
                    throw new InvalidOperationException("No form section is currently open. Can not set ReadOnly property.");

                formSections.Peek().ReadOnly = value;
            }
        }

        protected bool IsReadOnly
        {
            get
            {
                if (formSections.Count == 0)
                    throw new InvalidOperationException("No form section is currently open. Can not get ReadOnly property.");

                return formSections.Peek().IsReadOnly;
            }
        }

        protected bool? Required
        {
            set
            {
                if (formSections.Count == 0)
                    throw new InvalidOperationException("No form section is currently open. Can not set Required property.");

                formSections.Peek().Required = value;
            }
        }

        protected bool IsRequired
        {
            get
            {
                if (formSections.Count == 0)
                    throw new InvalidOperationException("No form section is currently open. Can not get Required property.");

                return formSections.Peek().IsRequired;
            }
        }

        protected string RequiredMessage
        {
            get
            {
                if (formSections.Count == 0)
                    throw new InvalidOperationException("No form section is currently open. Can not get RequiredMessage property.");

                return formSections.Peek().RequiredMessage;
            }
            set
            {
                if (formSections.Count == 0)
                    throw new InvalidOperationException("No form section is currently open. Can not set RequiredMessage property.");

                formSections.Peek().RequiredMessage = value;
            }
        }

        protected string RequiredMark
        {
            get
            {
                if (formSections.Count == 0)
                    throw new InvalidOperationException("No form section is currently open. Can not get RequiredMark property.");

                return formSections.Peek().RequiredMark;
            }
            set
            {
                if (formSections.Count == 0)
                    throw new InvalidOperationException("No form section is currently open. Can not set RequiredMark property.");

                formSections.Peek().RequiredMark = value;
            }
        }

        public bool? RequiredInLabel
        {
            set
            {
                if (formSections.Count == 0)
                    throw new InvalidOperationException("No form section is currently open. Can not set RequiredInLabel property.");

                formSections.Peek().RequiredInLabel = value;
            }
        }

        public bool IsRequiredInLabel
        {
            get
            {
                if (formSections.Count == 0)
                    throw new InvalidOperationException("No form section is currently open. Can not get IsRequiredInLabel property.");

                return formSections.Peek().IsRequiredInLabel;
            }
        }

        public bool? RequiredInPlaceholder
        {
            set
            {
                if (formSections.Count == 0)
                    throw new InvalidOperationException("No form section is currently open. Can not set RequiredInPlaceholder property.");

                formSections.Peek().RequiredInPlaceholder = value;
            }
        }

        public bool IsRequiredInPlaceholder
        {
            get
            {
                if (formSections.Count == 0)
                    throw new InvalidOperationException("No form section is currently open. Can not get IsRequiredInPlaceholder property.");

                return formSections.Peek().IsRequiredInPlaceholder;
            }
        }

        protected string OptionalMark
        {
            get
            {
                if (formSections.Count == 0)
                    throw new InvalidOperationException("No form section is currently open. Can not get OptionalMark property.");

                return formSections.Peek().OptionalMark;
            }
            set
            {
                if (formSections.Count == 0)
                    throw new InvalidOperationException("No form section is currently open. Can not set OptionalMark property.");

                formSections.Peek().OptionalMark = value;
            }
        }

        public bool? OptionalInLabel
        {
            set
            {
                if (formSections.Count == 0)
                    throw new InvalidOperationException("No form section is currently open. Can not set OptionalInLabel property.");

                formSections.Peek().OptionalInLabel = value;
            }
        }

        public bool IsOptionalInLabel
        {
            get
            {
                if (formSections.Count == 0)
                    throw new InvalidOperationException("No form section is currently open. Can not get IsOptionalInLabel property.");

                return formSections.Peek().IsOptionalInLabel;
            }
        }

        public bool? OptionalInPlaceholder
        {
            set
            {
                if (formSections.Count == 0)
                    throw new InvalidOperationException("No form section is currently open. Can not set OptionalInPlaceholder property.");

                formSections.Peek().OptionalInPlaceholder = value;
            }
        }

        public bool IsOptionalInPlaceholder
        {
            get
            {
                if (formSections.Count == 0)
                    throw new InvalidOperationException("No form section is currently open. Can not get IsOptionalInPlaceholder property.");

                return formSections.Peek().IsOptionalInPlaceholder;
            }
        }

        protected OrderElements OrderElements
        {
            get
            {
                if (formSections.Count == 0)
                    throw new InvalidOperationException("No form section is currently open. Can not get OrderElements property.");

                return formSections.Peek().OrderElements;
            }
            set
            {
                if (formSections.Count == 0)
                    throw new InvalidOperationException("No form section is currently open. Can not set OrderElements property.");

                formSections.Peek().OrderElements = value;
            }
        }

        protected void AddItem<T>(T formItem) where T : FormItem
        {
            if (formSections.Count == 0)
                throw new InvalidOperationException("No form section is currently open. Can not add form item.");

            formSections.Peek().Add(formItem);
        }

        public T GetItem<T>(string baseId) where T : FormItem
        {
            if (baseId == null)
                throw new ArgumentNullException();

            if (formSection == null)
                throw new InvalidOperationException("No form container exists. Can not get any form item.");

            return formSection.Get<T>(baseId);
        }

        public FormItem GetItem(string baseId)
        {
            if (baseId == null)
                throw new ArgumentNullException();

            if (formSection == null)
                throw new InvalidOperationException("No form container exists. Can not get any form item.");

            return formSection.Get(baseId);
        }

        protected abstract void AddRules(List<FormRule> rules);

        protected virtual void ApplyRules(bool isPostBack, FormItem formItem, string argument)
        {
            foreach (var r in rules)
                r(isPostBack, formItem, argument);
        }

        protected abstract void PerformAction();

        #endregion
    }
}
