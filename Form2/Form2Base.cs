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
using Form2.Html.Visitors;

namespace Form2
{
    [SuppressMessage("Style", "IDE0019:Use pattern matching", Justification = "<Pending>")]
    [SuppressMessage("Style", "IDE0031:Use null propagation", Justification = "<Pending>")]

    public abstract class Form2Base
    {
        #region Fields

        private readonly Stack<FormSection> formSections;

        private FormSection formSection;

        protected delegate void FormRule(bool isPostBack, string eventTarget, string eventArgument);

        private readonly List<FormRule> rules;

        private HtmlContainer htmlContainer;

        #endregion


        #region Properties

        protected FormSection FormSection { get { return formSection; } }

        #endregion


        #region Constructors

        public Form2Base()
        {
            formSections = new Stack<FormSection>();

            formSection = null;
            rules = new List<FormRule>();

            CreateForm();
            AddRules(rules);
        }

        #endregion


        #region Methods

        public string GetText(bool isPostBack, NameValueCollection form, HttpSessionState session)
        {
            if (formSection == null)
                return "";

            if (!isPostBack)
            {
                foreach (var formItem in formSection.Get<FormItem>().Where(f => f is IRequired))
                    session.Remove(formItem.SessionKey);
            }
            else
            {
                new FormPostBackVisitor(formSection, form);
            }

            ApplyRules(isPostBack, form);

            if (!isPostBack)
            {
                htmlContainer = new Form2HtmlVisitor(formSection, false).Html;
                return new Html2TextVisitor(htmlContainer).Text;
            }

            FormItem eventTarget = formSection.Get(form["__EVENTTARGET"]);

            ISubmit iSubmit = eventTarget as ISubmit;

            if (iSubmit == null)
            {
                IPostBack iPostBack = eventTarget as IPostBack;

                if (iPostBack == null || !iPostBack.IsPostBack)
                    throw new ApplicationException();

                htmlContainer = new Form2HtmlVisitor(formSection, session).Html;
            }
            else
            {
                if (!iSubmit.IsSubmit)
                    throw new ApplicationException();

                if (formSection.IsValid)
                {
                    PerformAction();

                    formSection = null;
                    rules.Clear();

                    CreateForm();
                    AddRules(rules);

                    if (formSection == null)
                        return "";

                    foreach (var formItem in formSection.Get<FormItem>().Where(f => f is IRequired))
                        session.Remove(formItem.SessionKey);

                    ApplyRules(false, null);

                    htmlContainer = new Form2HtmlVisitor(formSection, false).Html;
                }
                else
                {
                    foreach (var formItem in formSection.Get<FormItem>().Where(f => f is IRequired))
                        session.Remove(formItem.SessionKey);

                    foreach (var formItem in formSection.Get<FormItem>().Where(f => f is IRequired))
                    {
                        if (formItem is IHidden && (formItem as IHidden).IsHidden)
                            continue;

                        if (formItem is IDisabled && (formItem as IDisabled).IsDisabled)
                            continue;

                        session[formItem.SessionKey] = form[formItem.BaseId];
                    }

                    htmlContainer = new Form2HtmlVisitor(formSection, true).Html;
                }
            }

            return new Html2TextVisitor(htmlContainer).Text;
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

        protected T GetItem<T>(string baseId) where T : FormItem
        {
            if (formSection == null)
                throw new InvalidOperationException("No form container exists. Can not get any form item.");

            return formSection.Get<T>(baseId);
        }

        protected FormItem GetItem(string baseId)
        {
            if (formSection == null)
                throw new InvalidOperationException("No form container exists. Can not get any form item.");

            return formSection.Get(baseId);
        }

        protected abstract void AddRules(List<FormRule> rules);

        protected virtual void ApplyRules(bool isPostBack, NameValueCollection form)
        {
            foreach (var r in rules)
                r(isPostBack, form != null ? form["__EVENTTARGET"] : null, form != null ? form["__EVENTARGUMENT"] : null);
        }

        protected abstract void PerformAction();

        #endregion
    }
}
