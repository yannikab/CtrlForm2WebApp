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
using Form2.Form.Content.Items;
using Form2.Form.Enums;
using Form2.Form.Interfaces;
using Form2.Form.Visitors;
using Form2.Html.Content.Elements;
using Form2.Html.Visitors;

namespace Form2
{
    [SuppressMessage("Style", "IDE0019:Use pattern matching", Justification = "<Pending>")]

    public abstract class Form2Base
    {
        #region Fields

        private readonly Stack<FormGroup> groups = new Stack<FormGroup>();

        private FormGroup formGroup;

        private readonly List<Action> rules = new List<Action>();

        private HtmlContainer htmlContainer;

        #endregion


        #region Methods

        public string GetText(bool IsPostBack, NameValueCollection form, HttpSessionState sessionState)
        {
            CreateForm();

            if (formGroup == null)
                return "";

            if (IsPostBack)
            {
                new FormPostBackVisitor(formGroup, form);
            }
            else
            {
                foreach (var formItem in formGroup.Get<FormItem>().Where(f => f is IRequired))
                    sessionState.Remove(formItem.SessionKey);
            }

            ApplyRules();

            if (IsPostBack)
            {
                FormItem eventTarget = formGroup.Get(form["__EVENTTARGET"]);

                ISubmit iSubmit = eventTarget as ISubmit;

                if (iSubmit == null)
                {
                    IPostBack iPostBack = eventTarget as IPostBack;

                    if (iPostBack == null)
                        throw new ApplicationException();

                    if (iPostBack.IsPostBack)
                    {
                        htmlContainer = new Form2HtmlVisitor(formGroup, sessionState).Html;
                    }
                    else
                    {
                        throw new ApplicationException();
                    }
                }
                else if (iSubmit.IsSubmit)
                {
                    if (formGroup.IsValid)
                    {
                        PerformAction();

                        foreach (var formItem in formGroup.Get<FormItem>().Where(f => f is IRequired))
                            sessionState.Remove(formItem.SessionKey);
                        
                        formGroup = null;
                        CreateForm();
                        ApplyRules();

                        htmlContainer = new Form2HtmlVisitor(formGroup, false).Html;
                    }
                    else
                    {
                        foreach (var formItem in formGroup.Get<FormItem>().Where(f => f is IRequired))
                            sessionState.Remove(formItem.SessionKey);

                        foreach (var formItem in formGroup.Get<FormItem>().Where(f => f is IRequired))
                        {
                            if (formItem is IHidden && ((formItem as IHidden).IsHidden ?? false))
                                continue;

                            if (formItem is IDisabled && ((formItem as IDisabled).IsDisabled ?? false))
                                continue;

                            sessionState[formItem.SessionKey] = form[formItem.BaseId];
                        }

                        htmlContainer = new Form2HtmlVisitor(formGroup, true).Html;
                    }
                }
                else
                {
                    throw new ApplicationException();
                }
            }
            else
            {
                htmlContainer = new Form2HtmlVisitor(formGroup, false).Html;
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
                this.formGroup.IsHidden = false;
                this.formGroup.IsDisabled = false;
                this.formGroup.IsRequired = false;
                this.formGroup.RequiredMark = "*";
                this.formGroup.RequiredMessage = "!";
                this.formGroup.IsReadOnly = false;
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

        protected bool? IsHidden
        {
            get
            {
                if (groups.Count == 0)
                    throw new InvalidOperationException("No form group is currently open. Can not get IsHidden default property.");

                return groups.Peek().IsHidden;
            }

            set
            {
                if (groups.Count == 0)
                    throw new InvalidOperationException("No form group is currently open. Can not set IsHidden default property.");

                groups.Peek().IsHidden = value;
            }
        }

        protected bool? IsReadOnly
        {
            get
            {
                if (groups.Count == 0)
                    throw new InvalidOperationException("No form group is currently open. Can not get IsReadOnly default property.");

                return groups.Peek().IsReadOnly;
            }

            set
            {
                if (groups.Count == 0)
                    throw new InvalidOperationException("No form group is currently open. Can not set IsReadOnly default property.");

                groups.Peek().IsReadOnly = value;
            }
        }

        protected bool? IsRequired
        {
            get
            {
                if (groups.Count == 0)
                    throw new InvalidOperationException("No form group is currently open. Can not get IsRequired default property.");

                return groups.Peek().IsRequired;
            }

            set
            {
                if (groups.Count == 0)
                    throw new InvalidOperationException("No form group is currently open. Can not set IsRequired default property.");

                groups.Peek().IsRequired = value;
            }
        }

        protected string RequiredMark
        {
            get
            {
                if (groups.Count == 0)
                    throw new InvalidOperationException("No form group is currently open. Can not get RequiredMark default property.");

                return groups.Peek().RequiredMark;
            }

            set
            {
                if (groups.Count == 0)
                    throw new InvalidOperationException("No form group is currently open. Can not set RequiredMark default property.");

                groups.Peek().RequiredMark = value;
            }
        }

        protected string RequiredMessage
        {
            get
            {
                if (groups.Count == 0)
                    throw new InvalidOperationException("No form group is currently open. Can not get RequiredMessage default property.");

                return groups.Peek().RequiredMessage;
            }

            set
            {
                if (groups.Count == 0)
                    throw new InvalidOperationException("No form group is currently open. Can not set RequiredMessage default property.");

                groups.Peek().RequiredMessage = value;
            }
        }

        protected ElementOrder ElementOrder
        {
            get
            {
                if (groups.Count == 0)
                    throw new InvalidOperationException("No form group is currently open. Can not get ElementOrder default property.");

                return groups.Peek().ElementOrder;
            }

            set
            {
                if (groups.Count == 0)
                    throw new InvalidOperationException("No form group is currently open. Can not set ElementOrder default property.");

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

        protected void AddRule(Action rule)
        {
            rules.Add(rule);
        }

        protected virtual void ApplyRules()
        {
            foreach (var r in rules)
                r();
        }

        protected abstract void PerformAction();

        #endregion
    }
}
