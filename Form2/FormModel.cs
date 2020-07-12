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

        public void Update(FormItem formItem, string argument, NameValueCollection form)
        {
            if (formSection == null)
                return;

            submitted = false;

            ISubmit iSubmit = formItem as ISubmit;

            if (iSubmit != null)
                throw new ApplicationException();

            IPostBack iPostBack = formItem as IPostBack;

            if (iPostBack == null || !iPostBack.IsPostBack)
                throw new ApplicationException();

            new FormPostBackVisitor(formSection, form);

            ApplyRules(true, formItem, argument);

            new FormMessageVisitor(formSection, false);
        }

        public void Submit(FormItem formItem, string argument, NameValueCollection form)
        {
            if (formSection == null)
                return;

            submitted = false;

            new FormPostBackVisitor(formSection, form);

            ApplyRules(true, formItem, argument);

            ISubmit iSubmit = formItem as ISubmit;

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

                this.formSection.RequiredMark = "*";
                this.formSection.RequiredMessage = "!";
                this.formSection.ElementOrder = ElementOrder.InputLabelMark;
            }
            else
            {
                formSections.Peek().Add(formSection);
            }

            formSections.Push(formSection);
        }

        protected void CloseSection()
        {
            if (formSections.Count == 0)
                throw new InvalidOperationException("No form section is currently open. Can not close section.");

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

        protected ElementOrder ElementOrder
        {
            get
            {
                if (formSections.Count == 0)
                    throw new InvalidOperationException("No form section is currently open. Can not get ElementOrder property.");

                return formSections.Peek().ElementOrder;
            }
            set
            {
                if (formSections.Count == 0)
                    throw new InvalidOperationException("No form section is currently open. Can not set ElementOrder property.");

                formSections.Peek().ElementOrder = value;
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
