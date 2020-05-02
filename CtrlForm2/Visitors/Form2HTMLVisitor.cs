using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using UserControls.CtrlForm2.FormElements;
using UserControls.CtrlForm2.FormElements.FormItems;
using UserControls.CtrlForm2.FormElements.FormItems.FormItemsInput;
using UserControls.CtrlForm2.HtmlElements.HtmlGroups;
using UserControls.CtrlForm2.HtmlElements.HtmlItems;

namespace UserControls.CtrlForm2.Visitors
{
    public class Form2HtmlVisitor
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

        public void Visit(FormItem formItem, HtmlContainer htmlContainer)
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

        public virtual void Visit(FormGroup formGroup, HtmlContainer htmlContainer)
        {
            HtmlDiv htmlDiv = new HtmlDiv(formGroup.BaseId);
            htmlDiv.Class.Add("form-grouping");
            htmlDiv.Class.Add(string.Format("{0}-{1}", "form-id", formGroup.FormId));
            htmlDiv.Hidden.Value = formGroup.IsHidden;

            if (htmlContainer == null)
                Html = htmlDiv;
            else
                htmlContainer.Add(htmlDiv);

            foreach (var formItem in formGroup.Items)
                Visit(formItem, htmlDiv);
        }

        public virtual void Visit(FormTitle formTitle, HtmlContainer htmlContainer)
        {
            HtmlDiv htmlDiv = new HtmlDiv(formTitle.BaseId);
            htmlDiv.Class.Add("form-title");
            htmlDiv.Class.Add(string.Format("{0}-{1}", "form-id", formTitle.FormId));
            htmlDiv.Hidden.Value = formTitle.IsHidden;
            htmlContainer.Add(htmlDiv);

            HtmlLabel htmlLabel = new HtmlLabel(formTitle.BaseId);
            htmlDiv.Add(htmlLabel);

            htmlLabel.Add(new HtmlText(formTitle.Label));
        }

        public virtual void Visit(FormLabel formLabel, HtmlContainer htmlContainer)
        {
            HtmlDiv htmlDiv = new HtmlDiv(formLabel.BaseId);
            htmlDiv.Hidden.Value = formLabel.IsHidden;
            htmlDiv.Class.Add("form-label");
            htmlDiv.Class.Add(string.Format("{0}-{1}", "form-id", formLabel.FormId));

            htmlContainer.Add(htmlDiv);

            HtmlLabel htmlLabel = new HtmlLabel(formLabel.BaseId);
            htmlDiv.Add(htmlLabel);

            htmlLabel.Add(new HtmlText(formLabel.Label));
        }

        public virtual void Visit(FormTextBox formTextBox, HtmlContainer htmlContainer)
        {
            HtmlDiv htmlDiv = new HtmlDiv(formTextBox.BaseId);
            htmlDiv.Class.Add("form-textbox");
            htmlDiv.Class.Add(string.Format("{0}-{1}", "form-id", formTextBox.FormId));

            bool isRequired = formTextBox.IsRequired ?? false;

            if (!IsPostBack)
            {
                htmlDiv.Class.Add(isRequired ? "form-required" : "form-optional");
            }
            else
            {
                if (!string.IsNullOrEmpty(formTextBox.Text))
                    htmlDiv.Class.Add(formTextBox.IsValid ? "form-valid" : "form-invalid");
                else
                    htmlDiv.Class.Add(isRequired ? "form-not-entered" : "form-optional");
            }

            htmlDiv.Hidden.Value = formTextBox.IsHidden;
            htmlContainer.Add(htmlDiv);

            HtmlTextBox htmlTextBox = new HtmlTextBox(formTextBox.BaseId);
            htmlTextBox.Hidden.Value = formTextBox.IsHidden;
            htmlTextBox.ReadOnly.Value = formTextBox.IsReadOnly;
            htmlTextBox.Value.Value = formTextBox.Text;
            htmlTextBox.PlaceHolder.Value = formTextBox.PlaceHolder;

            HtmlLabel htmlLabel = new HtmlLabel(formTextBox.BaseId);
            htmlLabel.Hidden.Value = formTextBox.IsHidden;
            htmlLabel.For.Value = htmlTextBox.Id.Value;

            switch (formTextBox.ElementOrder)
            {
                case ElementOrder.LabelMarkInput:

                    htmlLabel.Add(new HtmlText(formTextBox.Label));

                    if (isRequired && formTextBox.RequiredMark != null)
                    {
                        HtmlSpan htmlSpan = new HtmlSpan();
                        htmlSpan.Class.Add("form-mark-required");
                        htmlSpan.Add(new HtmlText(formTextBox.RequiredMark));
                        htmlLabel.Add(htmlSpan);
                    }

                    htmlDiv.Add(htmlLabel);
                    htmlDiv.Add(htmlTextBox);

                    break;

                case ElementOrder.MarkLabelInput:

                    if (isRequired && formTextBox.RequiredMark != null)
                    {
                        HtmlSpan htmlSpan = new HtmlSpan();
                        htmlSpan.Class.Add("form-mark-required");
                        htmlSpan.Add(new HtmlText(formTextBox.RequiredMark));
                        htmlLabel.Add(htmlSpan);
                    }

                    htmlLabel.Add(new HtmlText(formTextBox.Label));

                    htmlDiv.Add(htmlLabel);
                    htmlDiv.Add(htmlTextBox);

                    break;

                case ElementOrder.InputLabelMark:

                    htmlLabel.Add(new HtmlText(formTextBox.Label));

                    if (isRequired && formTextBox.RequiredMark != null)
                    {
                        HtmlSpan htmlSpan = new HtmlSpan();
                        htmlSpan.Class.Add("form-mark-required");
                        htmlSpan.Add(new HtmlText(formTextBox.RequiredMark));
                        htmlLabel.Add(htmlSpan);
                    }

                    htmlDiv.Add(htmlTextBox);
                    htmlDiv.Add(htmlLabel);

                    break;

                case ElementOrder.InputMarkLabel:

                    if (isRequired && formTextBox.RequiredMark != null)
                    {
                        HtmlSpan htmlSpan = new HtmlSpan();
                        htmlSpan.Class.Add("form-mark-required");
                        htmlSpan.Add(new HtmlText(formTextBox.RequiredMark));
                        htmlLabel.Add(htmlSpan);
                    }

                    htmlLabel.Add(new HtmlText(formTextBox.Label));

                    htmlDiv.Add(htmlTextBox);
                    htmlDiv.Add(htmlLabel);

                    break;

                case ElementOrder.LabelInputMark:

                    htmlLabel.Add(new HtmlText(formTextBox.Label));

                    htmlDiv.Add(htmlLabel);
                    htmlDiv.Add(htmlTextBox);

                    if (isRequired && formTextBox.RequiredMark != null)
                    {
                        HtmlSpan htmlSpan = new HtmlSpan();
                        htmlSpan.Class.Add("form-mark-required");
                        htmlSpan.Add(new HtmlText(formTextBox.RequiredMark));
                        htmlDiv.Add(htmlSpan);
                    }

                    break;

                case ElementOrder.MarkInputLabel:

                    if (isRequired && formTextBox.RequiredMark != null)
                    {
                        HtmlSpan htmlSpan = new HtmlSpan();
                        htmlSpan.Class.Add("form-mark-required");
                        htmlSpan.Add(new HtmlText(formTextBox.RequiredMark));
                        htmlDiv.Add(htmlSpan);
                    }

                    htmlLabel.Add(new HtmlText(formTextBox.Label));

                    htmlDiv.Add(htmlTextBox);
                    htmlDiv.Add(htmlLabel);

                    break;

                default:
                case ElementOrder.NotSet:

                    break;
            }

            if (!IsPostBack)
                return;

            string message = null;

            if ((isRequired) && !formTextBox.IsEntered)
                message = formTextBox.RequiredMessage;
            else if (((isRequired) || formTextBox.IsEntered) && !formTextBox.IsValid)
                message = formTextBox.ValidationMessage;

            if (message == null)
                return;

            HtmlLabel htmlLabelMessage = new HtmlLabel(string.Format("{0}{1}", formTextBox.BaseId, "Message"));
            htmlLabelMessage.Hidden.Value = formTextBox.IsHidden;
            htmlLabelMessage.For.Value = htmlTextBox.Id.Value;
            htmlLabelMessage.Add(new HtmlText(message));
            htmlDiv.Add(htmlLabelMessage);
        }

        public virtual void Visit(FormTextArea formTextArea, HtmlContainer htmlContainer)
        {
            HtmlDiv htmlDiv = new HtmlDiv(formTextArea.BaseId);
            htmlDiv.Class.Add("form-textarea");
            htmlDiv.Class.Add(string.Format("{0}-{1}", "form-id", formTextArea.FormId));

            bool isRequired = formTextArea.IsRequired ?? false;

            if (!IsPostBack)
            {
                htmlDiv.Class.Add(isRequired ? "form-required" : "form-optional");
            }
            else
            {
                if (!string.IsNullOrEmpty(formTextArea.Text))
                    htmlDiv.Class.Add(formTextArea.IsValid ? "form-valid" : "form-invalid");
                else
                    htmlDiv.Class.Add(isRequired ? "form-not-entered" : "form-optional");
            }

            htmlDiv.Hidden.Value = formTextArea.IsHidden;
            htmlContainer.Add(htmlDiv);

            HtmlTextArea htmlTextArea = new HtmlTextArea(formTextArea.BaseId, formTextArea.Rows, formTextArea.Columns);
            htmlTextArea.Hidden.Value = formTextArea.IsHidden;
            htmlTextArea.ReadOnly.Value = formTextArea.IsReadOnly;
            htmlTextArea.Value.Value = formTextArea.Text;
            htmlTextArea.PlaceHolder.Value = formTextArea.PlaceHolder;

            HtmlLabel htmlLabel = new HtmlLabel(formTextArea.BaseId);
            htmlLabel.Hidden.Value = formTextArea.IsHidden;
            htmlLabel.For.Value = htmlTextArea.Id.Value;

            switch (formTextArea.ElementOrder)
            {
                case ElementOrder.LabelMarkInput:

                    htmlLabel.Add(new HtmlText(formTextArea.Label));

                    if (isRequired && formTextArea.RequiredMark != null)
                    {
                        HtmlSpan htmlSpan = new HtmlSpan(formTextArea.BaseId);
                        htmlSpan.Add(new HtmlText(formTextArea.RequiredMark));
                        htmlLabel.Add(htmlSpan);
                    }

                    htmlDiv.Add(htmlLabel);
                    htmlDiv.Add(htmlTextArea);

                    break;

                case ElementOrder.MarkLabelInput:

                    if (isRequired && formTextArea.RequiredMark != null)
                    {
                        HtmlSpan htmlSpan = new HtmlSpan(formTextArea.BaseId);
                        htmlSpan.Add(new HtmlText(formTextArea.RequiredMark));
                        htmlLabel.Add(htmlSpan);
                    }

                    htmlLabel.Add(new HtmlText(formTextArea.Label));

                    htmlDiv.Add(htmlLabel);
                    htmlDiv.Add(htmlTextArea);

                    break;

                case ElementOrder.InputLabelMark:

                    htmlLabel.Add(new HtmlText(formTextArea.Label));

                    if (isRequired && formTextArea.RequiredMark != null)
                    {
                        HtmlSpan htmlSpan = new HtmlSpan(formTextArea.BaseId);
                        htmlSpan.Add(new HtmlText(formTextArea.RequiredMark));
                        htmlLabel.Add(htmlSpan);
                    }

                    htmlDiv.Add(htmlTextArea);
                    htmlDiv.Add(htmlLabel);

                    break;

                case ElementOrder.InputMarkLabel:

                    if (isRequired && formTextArea.RequiredMark != null)
                    {
                        HtmlSpan htmlSpan = new HtmlSpan(formTextArea.BaseId);
                        htmlSpan.Add(new HtmlText(formTextArea.RequiredMark));
                        htmlLabel.Add(htmlSpan);
                    }

                    htmlLabel.Add(new HtmlText(formTextArea.Label));

                    htmlDiv.Add(htmlTextArea);
                    htmlDiv.Add(htmlLabel);

                    break;

                case ElementOrder.LabelInputMark:

                    htmlLabel.Add(new HtmlText(formTextArea.Label));

                    htmlDiv.Add(htmlLabel);
                    htmlDiv.Add(htmlTextArea);

                    if (isRequired && formTextArea.RequiredMark != null)
                    {
                        HtmlSpan htmlSpan = new HtmlSpan(formTextArea.BaseId);
                        htmlSpan.Add(new HtmlText(formTextArea.RequiredMark));
                        htmlDiv.Add(htmlSpan);
                    }

                    break;

                case ElementOrder.MarkInputLabel:

                    if (isRequired && formTextArea.RequiredMark != null)
                    {
                        HtmlSpan htmlSpan = new HtmlSpan(formTextArea.BaseId);
                        htmlSpan.Add(new HtmlText(formTextArea.RequiredMark));
                        htmlDiv.Add(htmlSpan);
                    }

                    htmlLabel.Add(new HtmlText(formTextArea.Label));

                    htmlDiv.Add(htmlTextArea);
                    htmlDiv.Add(htmlLabel);

                    break;

                default:
                case ElementOrder.NotSet:

                    break;
            }

            if (!IsPostBack)
                return;

            string message = null;

            if ((isRequired) && !formTextArea.IsEntered)
                message = formTextArea.RequiredMessage;
            else if (((isRequired) || formTextArea.IsEntered) && !formTextArea.IsValid)
                message = formTextArea.ValidationMessage;

            if (message == null)
                return;

            HtmlLabel htmlLabelMessage = new HtmlLabel(string.Format("{0}{1}", formTextArea.BaseId, "Message"));
            htmlLabelMessage.Hidden.Value = formTextArea.IsHidden;
            htmlLabelMessage.For.Value = htmlTextArea.Id.Value;
            htmlLabelMessage.Add(new HtmlText(message));
            htmlDiv.Add(htmlLabelMessage);
        }

        public virtual void Visit(FormSubmit formSubmit, HtmlContainer htmlContainer)
        {
            HtmlDiv htmlDiv = new HtmlDiv(formSubmit.BaseId);
            htmlDiv.Class.Add("form-button");
            htmlDiv.Class.Add(string.Format("{0}-{1}", "form-id", formSubmit.FormId));
            htmlDiv.Hidden.Value = formSubmit.IsHidden;
            htmlContainer.Add(htmlDiv);

            HtmlSubmit htmlSubmit = new HtmlSubmit(formSubmit.BaseId);
            htmlDiv.Add(htmlSubmit);

            htmlSubmit.Add(new HtmlText(formSubmit.Text));
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
