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
    public class Form2HtmlIconVisitor : Form2HtmlVisitor
    {
        public override void Visit(FormTextBox f, HtmlGroup parent)
        {
            HtmlDiv div = new HtmlDiv(f.BaseId);
            div.Class.Add("form-textbox");
            div.Class.Add(string.Format("{0}-{1}", idPrefix, f.FormId));
            div.Hidden.Value = f.IsHidden;
            parent.Add(div);

            HtmlTextBox textBox = new HtmlTextBox(f.BaseId);
            textBox.Hidden.Value = f.IsHidden;
            textBox.ReadOnly.Value = f.IsReadOnly;
            textBox.PlaceHolder.Value = f.PlaceHolder;
            textBox.Value.Value = f.InitialText;


            textBox.Class.Add("form-control");

            HtmlLabel label = new HtmlLabel(f.BaseId);
            label.Hidden.Value = f.IsHidden;

            label.For.Value = textBox.Id.Value;

            switch (f.ElementOrder)
            {
                case ElementOrder.LabelMarkInput:

                    label.Add(new HtmlText(f.Label));

                    if (f.IsRequired && f.RequiredMark != null)
                    {
                        HtmlSpan span = new HtmlSpan();
                        span.Class.Add("form-mark-required");
                        span.Add(new HtmlText(f.RequiredMark));
                        label.Add(span);
                    }

                    div.Add(label);

                    if (f.Icon == FormIcon.NotSet)
                    {
                        div.Add(textBox);
                    }
                    else
                    {
                        HtmlDiv divInputGroup = new HtmlDiv();
                        divInputGroup.Class.Add("input-group");
                        div.Add(divInputGroup);

                        divInputGroup.Add(textBox);

                        HtmlDiv divInputGroupAppend = new HtmlDiv();
                        divInputGroupAppend.Class.Add("input-group-append");
                        divInputGroup.Add(divInputGroupAppend);

                        HtmlSpan spanInputGroupText = new HtmlSpan();
                        spanInputGroupText.Class.Add("input-group-text");

                        divInputGroupAppend.Add(spanInputGroupText);

                        HtmlItalic italic = new HtmlItalic();
                        foreach (var cn in f.Icon.GetClassNames())
                            italic.Class.Add(cn);

                        spanInputGroupText.Add(italic);
                    }

                    break;

                case ElementOrder.MarkLabelInput:

                    if (f.IsRequired && f.RequiredMark != null)
                    {
                        HtmlSpan span = new HtmlSpan();
                        span.Class.Add("form-mark-required");
                        span.Add(new HtmlText(f.RequiredMark));
                        label.Add(span);
                    }

                    label.Add(new HtmlText(f.Label));

                    div.Add(label);

                    if (f.Icon == FormIcon.NotSet)
                    {
                        div.Add(textBox);
                    }
                    else
                    {
                        HtmlDiv divInputGroup = new HtmlDiv();
                        divInputGroup.Class.Add("input-group");
                        div.Add(divInputGroup);

                        divInputGroup.Add(textBox);

                        HtmlDiv divInputGroupAppend = new HtmlDiv();
                        divInputGroupAppend.Class.Add("input-group-append");
                        divInputGroup.Add(divInputGroupAppend);

                        HtmlSpan spanInputGroupText = new HtmlSpan();
                        spanInputGroupText.Class.Add("input-group-text");

                        divInputGroupAppend.Add(spanInputGroupText);

                        HtmlItalic italic = new HtmlItalic();
                        foreach (var cn in f.Icon.GetClassNames())
                            italic.Class.Add(cn);

                        spanInputGroupText.Add(italic);
                    }

                    break;

                case ElementOrder.InputLabelMark:

                    label.Add(new HtmlText(f.Label));

                    if (f.IsRequired && f.RequiredMark != null)
                    {
                        HtmlSpan span = new HtmlSpan();
                        span.Class.Add("form-mark-required");
                        span.Add(new HtmlText(f.RequiredMark));
                        label.Add(span);
                    }

                    div.Add(textBox);
                    div.Add(label);

                    break;

                case ElementOrder.InputMarkLabel:

                    if (f.IsRequired && f.RequiredMark != null)
                    {
                        HtmlSpan span = new HtmlSpan();
                        span.Class.Add("form-mark-required");
                        span.Add(new HtmlText(f.RequiredMark));
                        label.Add(span);
                    }

                    label.Add(new HtmlText(f.Label));

                    div.Add(textBox);
                    div.Add(label);

                    break;

                case ElementOrder.LabelInputMark:

                    label.Add(new HtmlText(f.Label));

                    div.Add(label);
                    div.Add(textBox);

                    if (f.IsRequired && f.RequiredMark != null)
                    {
                        HtmlSpan span = new HtmlSpan();
                        span.Class.Add("form-mark-required");
                        span.Add(new HtmlText(f.RequiredMark));
                        div.Add(span);
                    }

                    break;

                case ElementOrder.MarkInputLabel:

                    if (f.IsRequired && f.RequiredMark != null)
                    {
                        HtmlSpan span = new HtmlSpan();
                        span.Class.Add("form-mark-required");
                        span.Add(new HtmlText(f.RequiredMark));
                        div.Add(span);
                    }

                    label.Add(new HtmlText(f.Label));

                    div.Add(textBox);
                    div.Add(label);

                    break;

                default:
                case ElementOrder.NotSet:

                    break;
            }
        }

        public Form2HtmlIconVisitor(FormItem f)
            : base(f)
        {
        }
    }
}
