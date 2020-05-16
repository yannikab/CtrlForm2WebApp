﻿using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using CtrlForm2.Form.Content;
using CtrlForm2.Html.Content.Elements;

namespace CtrlForm2.Form.Visitors
{
    public partial class Form2HtmlVisitor
    {
        #region Fields

        private readonly bool isPostBack;

        private HtmlContainer html;

        #endregion


        #region Properties

        protected bool IsPostBack
        {
            get { return isPostBack; }
        }

        public HtmlContainer Html
        {
            get { return html; }
            protected set { html = value; }
        }

        #endregion


        #region Methods

        public void Visit(FormContent formItem, HtmlContainer htmlContainer)
        {
            var mi = (from m in GetType().GetMethods()
                      where
                      m.ReturnType.Equals(typeof(void)) &&
                      m.GetParameters().Length == 2 &&
                      m.GetParameters()[0].ParameterType.Equals(formItem.GetType()) &&
                      m.GetParameters()[1].ParameterType.Equals(typeof(HtmlContainer))
                      select m).SingleOrDefault();

            if (mi != null)
                mi.Invoke(this, new object[] { formItem, htmlContainer });
            else
                throw new NotImplementedException();
        }

        #endregion


        #region Constructors

        public Form2HtmlVisitor(FormGroup formGroup, bool isPostBack)
        {
            this.isPostBack = isPostBack;

            Visit(formGroup, null);
        }

        #endregion
    }
}