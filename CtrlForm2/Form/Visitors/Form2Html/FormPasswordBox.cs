using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using CtrlForm2.Form.Content.Items.Input;
using CtrlForm2.Form.Enums;
using CtrlForm2.Html.Content;
using CtrlForm2.Html.Content.Elements;
using CtrlForm2.Html.Content.Elements.Containers;
using CtrlForm2.Html.Content.Elements.Input;

namespace CtrlForm2.Form.Visitors
{
    public partial class Form2HtmlVisitor
    {
        public virtual void Visit(FormPasswordBox formPasswordBox, HtmlContainer htmlContainer)
        {
            HtmlDiv htmlDiv = new HtmlDiv(formPasswordBox.BaseId);
            htmlDiv.Class.Add("form-textbox");
            htmlDiv.Class.Add(string.Format("{0}-{1}", "form-id", formPasswordBox.FormId));

            bool isRequired = formPasswordBox.IsRequired ?? false;

            if (!IsPostBack)
            {
                htmlDiv.Class.Add(isRequired ? "form-required" : "form-optional");
            }
            else
            {
                if (!string.IsNullOrEmpty(formPasswordBox.Text))
                    htmlDiv.Class.Add(formPasswordBox.IsValid ? "form-valid" : "form-invalid");
                else
                    htmlDiv.Class.Add(isRequired ? "form-not-entered" : "form-optional");
            }

            htmlDiv.Hidden.Value = formPasswordBox.IsHidden;
            htmlContainer.Add(htmlDiv);

            HtmlPasswordBox htmlPasswordBox = new HtmlPasswordBox(formPasswordBox.BaseId);
            htmlPasswordBox.Hidden.Value = formPasswordBox.IsHidden;
            htmlPasswordBox.ReadOnly.Value = formPasswordBox.IsReadOnly;
            htmlPasswordBox.Value.Value = formPasswordBox.Text;
            htmlPasswordBox.PlaceHolder.Value = formPasswordBox.PlaceHolder;
            htmlPasswordBox.Disabled.Value = formPasswordBox.IsDisabled;

            HtmlLabel htmlLabel = new HtmlLabel(formPasswordBox.BaseId);
            htmlLabel.Hidden.Value = formPasswordBox.IsHidden;
            htmlLabel.For.Value = htmlPasswordBox.Id.Value;

            switch (formPasswordBox.ElementOrder)
            {
                case ElementOrder.LabelMarkInput:

                    htmlLabel.Add(new HtmlText(formPasswordBox.Label));

                    if (isRequired && formPasswordBox.RequiredMark != null)
                    {
                        HtmlSpan htmlSpan = new HtmlSpan();
                        htmlSpan.Class.Add("form-mark-required");
                        htmlSpan.Add(new HtmlText(formPasswordBox.RequiredMark));
                        htmlLabel.Add(htmlSpan);
                    }

                    htmlDiv.Add(htmlLabel);
                    htmlDiv.Add(htmlPasswordBox);

                    break;

                case ElementOrder.MarkLabelInput:

                    if (isRequired && formPasswordBox.RequiredMark != null)
                    {
                        HtmlSpan htmlSpan = new HtmlSpan();
                        htmlSpan.Class.Add("form-mark-required");
                        htmlSpan.Add(new HtmlText(formPasswordBox.RequiredMark));
                        htmlLabel.Add(htmlSpan);
                    }

                    htmlLabel.Add(new HtmlText(formPasswordBox.Label));

                    htmlDiv.Add(htmlLabel);
                    htmlDiv.Add(htmlPasswordBox);

                    break;

                case ElementOrder.InputLabelMark:

                    htmlLabel.Add(new HtmlText(formPasswordBox.Label));

                    if (isRequired && formPasswordBox.RequiredMark != null)
                    {
                        HtmlSpan htmlSpan = new HtmlSpan();
                        htmlSpan.Class.Add("form-mark-required");
                        htmlSpan.Add(new HtmlText(formPasswordBox.RequiredMark));
                        htmlLabel.Add(htmlSpan);
                    }

                    htmlDiv.Add(htmlPasswordBox);
                    htmlDiv.Add(htmlLabel);

                    break;

                case ElementOrder.InputMarkLabel:

                    if (isRequired && formPasswordBox.RequiredMark != null)
                    {
                        HtmlSpan htmlSpan = new HtmlSpan();
                        htmlSpan.Class.Add("form-mark-required");
                        htmlSpan.Add(new HtmlText(formPasswordBox.RequiredMark));
                        htmlLabel.Add(htmlSpan);
                    }

                    htmlLabel.Add(new HtmlText(formPasswordBox.Label));

                    htmlDiv.Add(htmlPasswordBox);
                    htmlDiv.Add(htmlLabel);

                    break;

                case ElementOrder.LabelInputMark:

                    htmlLabel.Add(new HtmlText(formPasswordBox.Label));

                    htmlDiv.Add(htmlLabel);
                    htmlDiv.Add(htmlPasswordBox);

                    if (isRequired && formPasswordBox.RequiredMark != null)
                    {
                        HtmlSpan htmlSpan = new HtmlSpan();
                        htmlSpan.Class.Add("form-mark-required");
                        htmlSpan.Add(new HtmlText(formPasswordBox.RequiredMark));
                        htmlDiv.Add(htmlSpan);
                    }

                    break;

                case ElementOrder.MarkInputLabel:

                    if (isRequired && formPasswordBox.RequiredMark != null)
                    {
                        HtmlSpan htmlSpan = new HtmlSpan();
                        htmlSpan.Class.Add("form-mark-required");
                        htmlSpan.Add(new HtmlText(formPasswordBox.RequiredMark));
                        htmlDiv.Add(htmlSpan);
                    }

                    htmlLabel.Add(new HtmlText(formPasswordBox.Label));

                    htmlDiv.Add(htmlPasswordBox);
                    htmlDiv.Add(htmlLabel);

                    break;

                default:
                case ElementOrder.NotSet:

                    break;
            }

            if (!IsPostBack)
                return;

            string message = null;

            if (isRequired && !formPasswordBox.IsEntered)
                message = formPasswordBox.RequiredMessage;
            else if ((isRequired || formPasswordBox.IsEntered) && !formPasswordBox.IsValid)
                message = formPasswordBox.ValidationMessage;

            if (message == null)
                return;

            HtmlLabel htmlLabelMessage = new HtmlLabel(string.Format("{0}{1}", formPasswordBox.BaseId, "Message"));
            htmlLabelMessage.Hidden.Value = formPasswordBox.IsHidden;
            htmlLabelMessage.For.Value = htmlPasswordBox.Id.Value;
            htmlLabelMessage.Add(new HtmlText(message));
            htmlDiv.Add(htmlLabelMessage);
        }
    }
}
