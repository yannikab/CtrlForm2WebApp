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

        private readonly Stack<FormGroup> groups = new Stack<FormGroup>();

        private FormGroup formGroup;

        protected delegate void FormRule(bool isPostBack, string eventTarget, string eventArgument);

        private readonly List<FormRule> rules;

        private HtmlContainer htmlContainer;

        #endregion


        #region Properties

        protected FormGroup FormGroup { get { return formGroup; } }

        #endregion


        #region Constructors

        public Form2Base()
        {
            formGroup = null;
            rules = new List<FormRule>();

            CreateForm();
            AddRules(rules);
        }

        #endregion


        #region Methods

        public string GetText(bool isPostBack, NameValueCollection form, HttpSessionState session)
        {
            if (formGroup == null)
                return "";

            if (!isPostBack)
            {
                foreach (var formItem in formGroup.Get<FormItem>().Where(f => f is IRequired))
                    session.Remove(formItem.SessionKey);
            }
            else
            {
                new FormPostBackVisitor(formGroup, form);
            }

            ApplyRules(isPostBack, form);

            if (!isPostBack)
            {
                htmlContainer = new Form2HtmlVisitor(formGroup, false).Html;
                return new Html2TextVisitor(htmlContainer).Text;
            }

            FormItem eventTarget = formGroup.Get(form["__EVENTTARGET"]);

            ISubmit iSubmit = eventTarget as ISubmit;

            if (iSubmit == null)
            {
                IPostBack iPostBack = eventTarget as IPostBack;

                if (iPostBack == null || !iPostBack.IsPostBack)
                    throw new ApplicationException();

                htmlContainer = new Form2HtmlVisitor(formGroup, session).Html;
            }
            else
            {
                if (!iSubmit.IsSubmit)
                    throw new ApplicationException();

                if (formGroup.IsValid)
                {
                    PerformAction();

                    formGroup = null;
                    rules.Clear();

                    CreateForm();
                    AddRules(rules);

                    if (formGroup == null)
                        return "";

                    foreach (var formItem in formGroup.Get<FormItem>().Where(f => f is IRequired))
                        session.Remove(formItem.SessionKey);

                    ApplyRules(false, null);

                    htmlContainer = new Form2HtmlVisitor(formGroup, false).Html;
                }
                else
                {
                    foreach (var formItem in formGroup.Get<FormItem>().Where(f => f is IRequired))
                        session.Remove(formItem.SessionKey);

                    foreach (var formItem in formGroup.Get<FormItem>().Where(f => f is IRequired))
                    {
                        if (formItem is IHidden && (formItem as IHidden).IsHidden)
                            continue;

                        if (formItem is IDisabled && (formItem as IDisabled).IsDisabled)
                            continue;

                        session[formItem.SessionKey] = form[formItem.BaseId];
                    }

                    htmlContainer = new Form2HtmlVisitor(formGroup, true).Html;
                }
            }

            return new Html2TextVisitor(htmlContainer).Text;
        }

        protected abstract void CreateForm();

        protected void OpenGroup(string baseId)
        {
            FormGroup formGroup = new FormGroup(baseId);

            if (groups.Count == 0)
            {
                this.formGroup = formGroup;

                this.formGroup.RequiredMark = "*";
                this.formGroup.RequiredMessage = "!";
                this.formGroup.ElementOrder = ElementOrder.InputLabelMark;
            }
            else
            {
                groups.Peek().Add(formGroup);
            }

            groups.Push(formGroup);
        }

        protected void CloseGroup()
        {
            if (groups.Count == 0)
                throw new InvalidOperationException("No form group is currently open. Can not close group.");

            groups.Pop();
        }

        protected bool? Hidden
        {
            set
            {
                if (groups.Count == 0)
                    throw new InvalidOperationException("No form group is currently open. Can not set Hidden property.");

                groups.Peek().Hidden = value;
            }
        }

        protected bool IsHidden
        {
            get
            {
                if (groups.Count == 0)
                    throw new InvalidOperationException("No form group is currently open. Can not get Hidden property.");

                return groups.Peek().IsHidden;
            }
        }

        protected bool? ReadOnly
        {
            set
            {
                if (groups.Count == 0)
                    throw new InvalidOperationException("No form group is currently open. Can not set ReadOnly property.");

                groups.Peek().ReadOnly = value;
            }
        }

        protected bool IsReadOnly
        {
            get
            {
                if (groups.Count == 0)
                    throw new InvalidOperationException("No form group is currently open. Can not get ReadOnly property.");

                return groups.Peek().IsReadOnly;
            }
        }

        protected bool? Required
        {
            set
            {
                if (groups.Count == 0)
                    throw new InvalidOperationException("No form group is currently open. Can not set Required property.");

                groups.Peek().Required = value;
            }
        }

        protected bool IsRequired
        {
            get
            {
                if (groups.Count == 0)
                    throw new InvalidOperationException("No form group is currently open. Can not get Required property.");

                return groups.Peek().IsRequired;
            }
        }

        protected string RequiredMark
        {
            get
            {
                if (groups.Count == 0)
                    throw new InvalidOperationException("No form group is currently open. Can not get RequiredMark property.");

                return groups.Peek().RequiredMark;
            }
            set
            {
                if (groups.Count == 0)
                    throw new InvalidOperationException("No form group is currently open. Can not set RequiredMark property.");

                groups.Peek().RequiredMark = value;
            }
        }

        protected string RequiredMessage
        {
            get
            {
                if (groups.Count == 0)
                    throw new InvalidOperationException("No form group is currently open. Can not get RequiredMessage property.");

                return groups.Peek().RequiredMessage;
            }
            set
            {
                if (groups.Count == 0)
                    throw new InvalidOperationException("No form group is currently open. Can not set RequiredMessage property.");

                groups.Peek().RequiredMessage = value;
            }
        }

        protected ElementOrder ElementOrder
        {
            get
            {
                if (groups.Count == 0)
                    throw new InvalidOperationException("No form group is currently open. Can not get ElementOrder property.");

                return groups.Peek().ElementOrder;
            }
            set
            {
                if (groups.Count == 0)
                    throw new InvalidOperationException("No form group is currently open. Can not set ElementOrder property.");

                groups.Peek().ElementOrder = value;
            }
        }

        protected void AddItem<T>(T formItem) where T : FormItem
        {
            if (groups.Count == 0)
                throw new InvalidOperationException("No form group is currently open. Can not add form item.");

            groups.Peek().Add(formItem);
        }

        protected T GetItem<T>(string baseId) where T : FormItem
        {
            if (formGroup == null)
                throw new InvalidOperationException("No form container exists. Can not get any form item.");

            return formGroup.Get<T>(baseId);
        }

        protected FormItem GetItem(string baseId)
        {
            if (formGroup == null)
                throw new InvalidOperationException("No form container exists. Can not get any form item.");

            return formGroup.Get(baseId);
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
