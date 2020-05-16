﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

using CtrlForm2.Form.Content;
using CtrlForm2.Form.Enums;
using CtrlForm2.Form.Visitors;
using CtrlForm2.Html.Content.Elements;
using CtrlForm2.Html.Visitors;

namespace CtrlForm2.UserControls
{
    public abstract class CtrlForm2Base : UserControl
    {
        #region Fields

        protected Literal LtrContent { get; set; }

        private readonly Stack<FormGroup> groups = new Stack<FormGroup>();

        private FormGroup formGroup;

        private HtmlContainer htmlContainer;

        #endregion

        #region Properties

        private FormGroup FormGroup
        {
            get { return formGroup; }
            set { formGroup = value; }
        }

        private HtmlContainer HtmlContainer
        {
            get { return htmlContainer; }
            set { htmlContainer = value; }
        }

        #endregion

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            CreateForm();

            if (FormGroup == null)
                return;

            if (IsPostBack)
                new FormPostBackVisitor(FormGroup, Request.Form);

            HtmlContainer = new Form2HtmlVisitor(FormGroup, IsPostBack).Html;

            LtrContent.Text = new Html2TextVisitor(HtmlContainer).Text;
        }

        protected abstract void CreateForm();

        protected void OpenGroup(string baseId)
        {
            FormGroup formGroup = new FormGroup(baseId);

            if (groups.Count == 0)
            {
                FormGroup = formGroup;
                FormGroup.IsHidden = false;
                FormGroup.IsRequired = false;
                FormGroup.RequiredMark = "*";
                FormGroup.RequiredMessage = "!";
                FormGroup.IsReadOnly = false;
                FormGroup.ElementOrder = ElementOrder.InputLabelMark;
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

        protected void AddItem(FormContent formItem)
        {
            if (formItem is FormGroup)
                throw new InvalidOperationException("Only form items can be added to a group. Can not add form group.");

            if (groups.Count == 0)
                throw new InvalidOperationException("No form group is currently open. Can not add form item.");

            groups.Peek().Add(formItem);
        }

        protected T GetItem<T>(string baseId) where T : FormItem
        {
            if (FormGroup == null)
                throw new InvalidOperationException("No form container exists. Can not get any form item.");

            return FormGroup.Get<T>(baseId);
        }
    }
}