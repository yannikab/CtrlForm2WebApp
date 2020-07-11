using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.SessionState;

using Form2.Form.Content;
using Form2.Form.Enums;
using Form2.Form.Interfaces;
using Form2.Form.Visitors;
using Form2.Html.Content.Elements;

namespace Form2
{
    [SuppressMessage("Style", "IDE0019:Use pattern matching", Justification = "<Pending>")]
    [SuppressMessage("Style", "IDE0031:Use null propagation", Justification = "<Pending>")]

    public abstract class Form2Model
    {
        #region Fields

        private FormSection formSection;

        private readonly Stack<FormSection> formSections;

        protected delegate void FormRule(bool isPostBack, FormItem formItem, string argument);

        private readonly List<FormRule> rules;

        private HtmlContainer htmlContainer;

        #endregion


        #region Properties

        protected FormSection FormSection { get { return formSection; } }

        public HtmlContainer Html { get { return htmlContainer; } }

        #endregion


        #region Constructors

        public Form2Model()
        {
            formSection = null;
            formSections = new Stack<FormSection>();
            rules = new List<FormRule>();
        }

        #endregion


        #region Methods

        public void Initialize(HttpSessionState session)
        {
            formSection = null;
            formSections.Clear();
            rules.Clear();

            CreateForm();
            AddRules(rules);

            if (formSection == null)
                return;

            foreach (var formItem in formSection.Get<FormItem>().Where(f => f is IRequired))
                session.Remove(formItem.SessionKey);

            ApplyRules(false, null, null);

            htmlContainer = new Form2HtmlVisitor(formSection, false).Html;
        }

        public void Update(FormItem formItem, string argument, NameValueCollection form, HttpSessionState session)
        {
            if (formSection == null)
                return;

            ISubmit iSubmit = formItem as ISubmit;

            if (iSubmit != null)
                throw new ApplicationException();

            IPostBack iPostBack = formItem as IPostBack;

            if (iPostBack == null || !iPostBack.IsPostBack)
                throw new ApplicationException();

            new FormPostBackVisitor(formSection, form);

            ApplyRules(true, formItem, argument);

            htmlContainer = new Form2HtmlVisitor(formSection, session).Html;
        }

        public void Submit(FormItem formItem, string argument, NameValueCollection form, HttpSessionState session)
        {
            if (formSection == null)
                return;

            new FormPostBackVisitor(formSection, form);

            ApplyRules(true, formItem, argument);

            ISubmit iSubmit = formItem as ISubmit;

            if (!iSubmit.IsSubmit)
                throw new ApplicationException();

            if (formSection.IsValid)
            {
                PerformAction();

                Initialize(session);
            }
            else
            {
                foreach (var fi in formSection.Get<FormItem>().Where(f => f is IRequired))
                    session.Remove(fi.SessionKey);

                foreach (var fi in formSection.Get<FormItem>().Where(f => f is IRequired))
                {
                    if (fi is IHidden && (fi as IHidden).IsHidden)
                        continue;

                    if (fi is IDisabled && (fi as IDisabled).IsDisabled)
                        continue;

                    session[fi.SessionKey] = form[fi.BaseId];
                }

                htmlContainer = new Form2HtmlVisitor(formSection, true).Html;
            }
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
