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
using Form2.Form.Interfaces;
using Form2.Html.Content;
using Form2.Html.Content.Elements;
using Form2.Html.Content.Elements.Containers;

namespace Form2.Form.Visitors
{
    public partial class Form2HtmlVisitor
    {
        #region Fields

        private readonly bool initialize;

        private readonly bool verbose;

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

        void AddMark(IRequired formItem, HtmlContainer htmlContainer)
        {
            if (formItem is IHidden && (formItem as IHidden).IsHidden)
                return;

            if (formItem is IDisabled && (formItem as IDisabled).IsDisabled)
                return;

            if (formItem is IReadOnly && (formItem as IReadOnly).IsReadOnly)
                return;

            if (formItem.IsRequired)
            {
                if (formItem.IsRequiredInLabel && !string.IsNullOrWhiteSpace(formItem.RequiredMark))
                {
                    HtmlLabel htmlLabel = new HtmlLabel("");
                    htmlLabel.Class.Add("formMarkRequired");
                    htmlLabel.Add(new HtmlText(formItem.RequiredMark.Replace(" ", "&nbsp;")));
                    htmlContainer.Add(htmlLabel);
                }
            }
            else
            {
                if (formItem.IsOptionalInLabel && !string.IsNullOrWhiteSpace(formItem.OptionalMark))
                {
                    HtmlLabel htmlLabel = new HtmlLabel("");
                    htmlLabel.Class.Add("formMarkOptional");
                    htmlLabel.Add(new HtmlText(formItem.OptionalMark.Replace(" ", "&nbsp;")));
                    htmlContainer.Add(htmlLabel);
                }
            }
        }

        #endregion


        #region Constructors

        public Form2HtmlVisitor(FormModel formModel, bool verbose)
        {
            initialize = formModel.Submitted;

            this.verbose = verbose;

            Visit(formModel.FormGroup, null);
        }

        #endregion
    }
}
