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
using Form2.Html.Content.Elements;

namespace Form2.Form.Visitors
{
    public partial class Form2HtmlVisitor
    {
        #region Fields

        private readonly bool validate;

        private readonly HttpSessionState sessionState;

        private HtmlContainer html;

        #endregion


        #region Properties

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

        private Form2HtmlVisitor(FormGroup formGroup, bool validate, HttpSessionState sessionState)
        {
            this.validate = validate;

            this.sessionState = sessionState;

            Visit(formGroup, null);
        }

        public Form2HtmlVisitor(FormGroup formGroup, HttpSessionState sessionState)
            : this(formGroup, sessionState.Count > 0, sessionState)
        {

        }

        public Form2HtmlVisitor(FormGroup formGroup, bool validate)
            : this(formGroup, validate, null)
        {

        }

        #endregion
    }
}
