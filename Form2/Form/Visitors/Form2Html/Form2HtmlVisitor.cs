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

        private HtmlLabel Mark(IRequired formItem)
        {
            HtmlLabel htmlLabel = null;

            if (formItem is IHidden && (formItem as IHidden).IsHidden)
                return htmlLabel;

            if (formItem is IDisabled && (formItem as IDisabled).IsDisabled)
                return htmlLabel;

            if (formItem is IReadOnly && (formItem as IReadOnly).IsReadOnly)
                return htmlLabel;

            if (formItem.IsRequired)
            {
                if (formItem.IsRequiredInLabel && !string.IsNullOrWhiteSpace(formItem.RequiredMark))
                {
                    htmlLabel = new HtmlLabel("");
                    htmlLabel.Class.Add("formMarkRequired");
                    htmlLabel.Add(new HtmlText(formItem.RequiredMark.Replace(" ", "&nbsp;")));
                }
            }
            else
            {
                if (formItem.IsOptionalInLabel && !string.IsNullOrWhiteSpace(formItem.OptionalMark))
                {
                    htmlLabel = new HtmlLabel("");
                    htmlLabel.Class.Add("formMarkOptional");
                    htmlLabel.Add(new HtmlText(formItem.OptionalMark.Replace(" ", "&nbsp;")));
                }
            }

            return htmlLabel;
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
