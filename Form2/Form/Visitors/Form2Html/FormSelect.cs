using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Form2.Form.Content.Items.Input.Selectors;
using Form2.Form.Enums;
using Form2.Form.Selectables;
using Form2.Html.Content;
using Form2.Html.Content.Elements;
using Form2.Html.Content.Elements.Containers;

namespace Form2.Form.Visitors
{
    public partial class Form2HtmlVisitor
    {
        public virtual void Visit(FormSelect formSelect, HtmlContainer htmlContainer)
        {
            HtmlDiv htmlDiv = new HtmlDiv(formSelect.BaseId);
            htmlDiv.Class.Add("form-select");
            htmlDiv.Class.Add(string.Format("{0}-{1}", "form-id", formSelect.FormId));

            bool isRequired = formSelect.IsRequired ?? false;

            if (!validate)
            {
                htmlDiv.Class.Add(isRequired ? "form-required" : "form-optional");
            }
            else
            {
                if (formSelect.IsRequiredMet)
                    htmlDiv.Class.Add("form-valid");
                else
                    htmlDiv.Class.Add(isRequired ? "form-not-entered" : "form-optional");
            }

            htmlDiv.Hidden.Value = formSelect.IsHidden;
            htmlContainer.Add(htmlDiv);

            HtmlSelect htmlSelect = formSelect.Size.HasValue ?
                new HtmlSelect(formSelect.BaseId, formSelect.Size.Value, formSelect.IsPostBack) :
                new HtmlSelect(formSelect.BaseId, formSelect.IsMultiSelect, formSelect.IsPostBack);
            htmlSelect.Class.Add("form-select");
            htmlSelect.Class.Add(string.Format("{0}-{1}", "form-id", formSelect.FormId));
            htmlSelect.Disabled.Value = formSelect.IsDisabled;

            HtmlLabel htmlLabel = new HtmlLabel(formSelect.BaseId);
            htmlLabel.For.Value = htmlSelect.Id.Value;

            switch (formSelect.ElementOrder)
            {
                case ElementOrder.LabelMarkInput:

                    htmlLabel.Add(new HtmlText(formSelect.Label));

                    if (isRequired && formSelect.RequiredMark != null)
                    {
                        HtmlSpan htmlSpan = new HtmlSpan();
                        htmlSpan.Class.Add("form-mark-required");
                        htmlSpan.Add(new HtmlText(formSelect.RequiredMark));
                        htmlLabel.Add(htmlSpan);
                    }

                    htmlDiv.Add(htmlLabel);
                    htmlDiv.Add(htmlSelect);

                    break;

                case ElementOrder.MarkLabelInput:

                    if (isRequired && formSelect.RequiredMark != null)
                    {
                        HtmlSpan htmlSpan = new HtmlSpan();
                        htmlSpan.Class.Add("form-mark-required");
                        htmlSpan.Add(new HtmlText(formSelect.RequiredMark));
                        htmlLabel.Add(htmlSpan);
                    }

                    htmlLabel.Add(new HtmlText(formSelect.Label));

                    htmlDiv.Add(htmlLabel);
                    htmlDiv.Add(htmlSelect);

                    break;

                case ElementOrder.InputLabelMark:

                    htmlLabel.Add(new HtmlText(formSelect.Label));

                    if (isRequired && formSelect.RequiredMark != null)
                    {
                        HtmlSpan htmlSpan = new HtmlSpan();
                        htmlSpan.Class.Add("form-mark-required");
                        htmlSpan.Add(new HtmlText(formSelect.RequiredMark));
                        htmlLabel.Add(htmlSpan);
                    }

                    htmlDiv.Add(htmlSelect);
                    htmlDiv.Add(htmlLabel);

                    break;

                case ElementOrder.InputMarkLabel:

                    if (isRequired && formSelect.RequiredMark != null)
                    {
                        HtmlSpan htmlSpan = new HtmlSpan();
                        htmlSpan.Class.Add("form-mark-required");
                        htmlSpan.Add(new HtmlText(formSelect.RequiredMark));
                        htmlLabel.Add(htmlSpan);
                    }

                    htmlLabel.Add(new HtmlText(formSelect.Label));

                    htmlDiv.Add(htmlSelect);
                    htmlDiv.Add(htmlLabel);

                    break;

                case ElementOrder.LabelInputMark:

                    htmlLabel.Add(new HtmlText(formSelect.Label));

                    htmlDiv.Add(htmlLabel);
                    htmlDiv.Add(htmlSelect);

                    if (isRequired && formSelect.RequiredMark != null)
                    {
                        HtmlSpan htmlSpan = new HtmlSpan();
                        htmlSpan.Class.Add("form-mark-required");
                        htmlSpan.Add(new HtmlText(formSelect.RequiredMark));
                        htmlDiv.Add(htmlSpan);
                    }

                    break;

                case ElementOrder.MarkInputLabel:

                    if (isRequired && formSelect.RequiredMark != null)
                    {
                        HtmlSpan htmlSpan = new HtmlSpan();
                        htmlSpan.Class.Add("form-mark-required");
                        htmlSpan.Add(new HtmlText(formSelect.RequiredMark));
                        htmlDiv.Add(htmlSpan);
                    }

                    htmlLabel.Add(new HtmlText(formSelect.Label));

                    htmlDiv.Add(htmlSelect);
                    htmlDiv.Add(htmlLabel);

                    break;

                default:
                case ElementOrder.NotSet:

                    break;
            }

            foreach (var formOption in formSelect.Content)
                Visit(formOption, htmlSelect);

            if (!validate)
                return;

            if (sessionState != null)
            {
                string viewStateString = (string)sessionState[formSelect.SessionKey];

                if (viewStateString == null)
                    return;

                int previousIndex = 0;

                var content = formSelect.Content.ToList();

                for (int i = 0; i < content.Count; i++)
                    content[i].IsSelected = false;

                foreach (var o in viewStateString.Split(','))
                {
                    for (int i = previousIndex; i < content.Count; i++)
                    {
                        if (formSelect.Header != null && content[i] == formSelect.Header)
                            continue;

                        if (content[i].IsHidden ?? false)
                            continue;

                        if (content[i].IsDisabled ?? false)
                            continue;

                        if (content[i].Value == o)
                        {
                            content[i].IsSelected = true;
                            previousIndex = i + 1;

                            break;
                        }
                    }
                }
            }

            string message = null;

            if (!formSelect.IsRequiredMet)
                message = formSelect.RequiredMessage;
            else if (!formSelect.IsValid)
                message = formSelect.ValidationMessage;

            if (message == null)
                return;

            HtmlLabel htmlLabelMessage = new HtmlLabel(string.Format("{0}{1}", formSelect.BaseId, "Message"));
            htmlLabelMessage.For.Value = htmlSelect.Id.Value;
            htmlLabelMessage.Add(new HtmlText(message));
            htmlDiv.Add(htmlLabelMessage);
        }

        public virtual void Visit(FormOption formOption, HtmlContainer htmlContainer)
        {
            HtmlOption htmlOption = new HtmlOption(formOption.Value);
            htmlOption.Add(new HtmlText(formOption.Text));
            htmlOption.Hidden.Value = formOption.IsHidden;
            htmlOption.Disabled.Value = formOption.IsDisabled;
            htmlOption.Selected.Value = formOption.IsSelected;

            htmlContainer.Add(htmlOption);
        }
    }
}
