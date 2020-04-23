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
    [SuppressMessage("Style", "IDE0060:Remove unused parameter", Justification = "<Pending>")]

    public class Form2HtmlVisitor
    {
        protected static readonly string idPrefix = "form-id";

        private HtmlGroup html;

        public HtmlGroup Html
        {
            get { return html; }
            protected set { html = value; }
        }

        public void Visit(object o, HtmlGroup parent)
        {
            var mi = (from m in GetType().GetMethods()
                      where
                      m.ReturnType.Equals(typeof(void)) &&
                      m.GetParameters().Length == 2 &&
                      m.GetParameters()[0].ParameterType.Equals(o.GetType()) &&
                      m.GetParameters()[1].ParameterType.Equals(typeof(HtmlGroup))
                      select m).SingleOrDefault();

            if (mi != null)
                mi.Invoke(this, new object[] { o, parent });
        }

        public virtual void Visit(FormItem f, HtmlGroup parent)
        {
            throw new NotImplementedException();
        }

        public virtual void Visit(FormItemInput f, HtmlGroup parent)
        {
            throw new NotImplementedException();
        }

        public virtual void Visit(FormGroup f, HtmlGroup parent)
        {
            HtmlDiv div = new HtmlDiv();
            div.Class.Add("form-grouping");
            div.Class.Add(string.Format("{0}-{1}", idPrefix, f.FormId));
            div.Hidden.Value = f.IsHidden;

            if (parent == null)
                Html = div;
            else
                parent.Add(div);

            foreach (var c in f.Contents)
                Visit(c, div);
        }

        public virtual void Visit(FormLabel f, HtmlGroup parent)
        {
            HtmlDiv div = new HtmlDiv(f.BaseId);
            div.Class.Add("form-label");
            div.Class.Add(string.Format("{0}-{1}", idPrefix, f.FormId));
            div.Hidden.Value = f.IsHidden;
            parent.Add(div);

            HtmlLabel label = new HtmlLabel(f.BaseId);
            div.Add(label);

            label.Add(new HtmlText(f.Label));
        }

        public virtual void Visit(FormTitle f, HtmlGroup parent)
        {
            HtmlDiv div = new HtmlDiv(f.BaseId);
            div.Hidden.Value = f.IsHidden;
            div.Class.Add("form-title");
            div.Class.Add(string.Format("{0}-{1}", idPrefix, f.FormId));
            parent.Add(div);

            HtmlLabel label = new HtmlLabel(f.BaseId);
            div.Add(label);

            HtmlText text = new HtmlText(f.Label);
            label.Add(text);
        }

        public virtual void Visit(FormTextBox f, HtmlGroup parent)
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
                    div.Add(textBox);

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
                    div.Add(textBox);

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

        public virtual void Visit(FormTextArea f, HtmlGroup parent)
        {
            HtmlDiv div = new HtmlDiv(f.BaseId);
            div.Class.Add("form-textarea");
            div.Class.Add(string.Format("{0}-{1}", idPrefix, f.FormId));
            div.Hidden.Value = f.IsHidden;
            parent.Add(div);

            HtmlTextArea textArea = new HtmlTextArea(f.BaseId, f.Rows, f.Columns);
            textArea.Hidden.Value = f.IsHidden;
            textArea.ReadOnly.Value = f.IsReadOnly;
            textArea.PlaceHolder.Value = f.PlaceHolder;
            textArea.Value.Value = f.InitialText;

            HtmlLabel label = new HtmlLabel(f.BaseId);
            label.Hidden.Value = f.IsHidden;
            label.For.Value = textArea.Id.Value;

            switch (f.ElementOrder)
            {
                case ElementOrder.LabelMarkInput:

                    label.Add(new HtmlText(f.Label));

                    if (f.IsRequired && f.RequiredMark != null)
                    {
                        HtmlSpan span = new HtmlSpan(f.BaseId);
                        span.Add(new HtmlText(f.RequiredMark));
                        label.Add(span);
                    }

                    div.Add(label);
                    div.Add(textArea);

                    break;

                case ElementOrder.MarkLabelInput:

                    if (f.IsRequired && f.RequiredMark != null)
                    {
                        HtmlSpan span = new HtmlSpan(f.BaseId);
                        span.Add(new HtmlText(f.RequiredMark));
                        label.Add(span);
                    }

                    label.Add(new HtmlText(f.Label));

                    div.Add(label);
                    div.Add(textArea);

                    break;

                case ElementOrder.InputLabelMark:

                    label.Add(new HtmlText(f.Label));

                    if (f.IsRequired && f.RequiredMark != null)
                    {
                        HtmlSpan span = new HtmlSpan(f.BaseId);
                        span.Add(new HtmlText(f.RequiredMark));
                        label.Add(span);
                    }

                    div.Add(textArea);
                    div.Add(label);

                    break;

                case ElementOrder.InputMarkLabel:

                    if (f.IsRequired && f.RequiredMark != null)
                    {
                        HtmlSpan span = new HtmlSpan(f.BaseId);
                        span.Add(new HtmlText(f.RequiredMark));
                        label.Add(span);
                    }

                    label.Add(new HtmlText(f.Label));

                    div.Add(textArea);
                    div.Add(label);

                    break;

                case ElementOrder.LabelInputMark:

                    label.Add(new HtmlText(f.Label));

                    div.Add(label);
                    div.Add(textArea);

                    if (f.IsRequired && f.RequiredMark != null)
                    {
                        HtmlSpan span = new HtmlSpan(f.BaseId);
                        span.Add(new HtmlText(f.RequiredMark));
                        div.Add(span);
                    }

                    break;

                case ElementOrder.MarkInputLabel:

                    if (f.IsRequired && f.RequiredMark != null)
                    {
                        HtmlSpan span = new HtmlSpan(f.BaseId);
                        span.Add(new HtmlText(f.RequiredMark));
                        div.Add(span);
                    }

                    label.Add(new HtmlText(f.Label));

                    div.Add(textArea);
                    div.Add(label);

                    break;

                default:
                case ElementOrder.NotSet:

                    break;
            }
        }

        public virtual void Visit(FormSubmit f, HtmlGroup parent)
        {
            HtmlDiv div = new HtmlDiv(f.BaseId);
            div.Class.Add("form-button");
            div.Class.Add(string.Format("{0}-{1}", idPrefix, f.FormId));
            div.Hidden.Value = f.IsHidden;
            parent.Add(div);

            HtmlSubmit button = new HtmlSubmit(f.BaseId);
            div.Add(button);

            button.Add(new HtmlText(f.Text));
        }

        public Form2HtmlVisitor(FormItem f)
        {
            if (f is FormGroup)
            {
                Visit((object)f, null);
            }
            else
            {
                HtmlDiv div = new HtmlDiv("Container");
                div.Class.Add("form-group");
                div.Class.Add(string.Format("{0}-{1}", idPrefix, "container"));

                Html = div;

                Visit((object)f, div);
            }
        }
    }
}
