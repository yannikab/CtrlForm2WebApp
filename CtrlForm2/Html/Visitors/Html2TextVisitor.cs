using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using CtrlForm2.Html.Content;
using CtrlForm2.Html.Content.Elements;
using CtrlForm2.Html.Content.Elements.Containers;
using CtrlForm2.Html.Content.Elements.Input;

namespace CtrlForm2.Html.Visitors
{
    [SuppressMessage("Style", "IDE0060:Remove unused parameter", Justification = "<Pending>")]

    class Html2TextVisitor
    {
        private readonly StringBuilder sb = new StringBuilder();

        public string Text
        {
            get { return sb.ToString(); }
        }

        protected string Tabs(int depth)
        {
            return new string('\t', depth);
        }

        public void Visit(HtmlContent e)
        {
            var mi = (from m in GetType().GetMethods()
                      where
                      m.ReturnType.Equals(typeof(void)) &&
                      m.GetParameters().Length == 1 &&
                      m.GetParameters()[0].ParameterType.Equals(e.GetType())
                      select m).SingleOrDefault();

            if (mi != null)
                mi.Invoke(this, new object[] { e });
            else
                throw new NotImplementedException();
        }

        public void Visit(HtmlDiv e)
        {
            sb.AppendLine();
            sb.Append(Tabs(e.Depth));
            sb.Append(string.Format("<{0}", e.Tag));

            foreach (var a in e.Attributes.Where(a => a.IsSet))
                sb.Append(a);

            sb.AppendLine(">");

            foreach (var c in e.Contents)
                Visit(c);

            sb.Append(Tabs(e.Depth));
            sb.AppendLine(string.Format("</{0}>", e.Tag));
        }

        public void Visit(HtmlSpan e)
        {
            if (e.Container is HtmlDiv)
            {
                sb.AppendLine();
                sb.Append(Tabs(e.Depth));
            }

            sb.Append(string.Format("<{0}", e.Tag));

            foreach (var a in e.Attributes.Where(a => a.IsSet))
                sb.Append(a);

            sb.Append(">");

            foreach (var c in e.Contents)
                Visit(c);

            sb.Append(string.Format("</{0}>", e.Tag));

            if (e.Container is HtmlDiv)
                sb.AppendLine();
        }

        public void Visit(HtmlItalic e)
        {
            sb.Append(string.Format("<{0}", e.Tag));

            foreach (var a in e.Attributes.Where(a => a.IsSet))
                sb.Append(a);

            sb.Append(">");

            foreach (var c in e.Contents)
                Visit(c);

            sb.Append(string.Format("</{0}>", e.Tag));
        }

        public void Visit(HtmlLabel e)
        {
            sb.Append(Tabs(e.Depth));
            sb.Append(string.Format("<{0}", e.Tag));

            foreach (var a in e.Attributes.Where(a => a.IsSet))
                sb.Append(a);

            sb.Append(">");

            foreach (var c in e.Contents)
                Visit(c);

            sb.AppendLine(string.Format("</{0}>", e.Tag));
        }

        public void Visit(HtmlTextBox e)
        {
            sb.Append(Tabs(e.Depth));
            sb.Append(string.Format("<{0}", e.Tag));

            foreach (var a in e.Attributes.Where(a => a.IsSet))
                sb.Append(a);

            sb.AppendLine(" />");
        }

        public void Visit(HtmlTextArea e)
        {
            sb.Append(Tabs(e.Depth));
            sb.Append(string.Format("<{0}", e.Tag));

            foreach (var a in e.Attributes.Where(a => a.IsSet && a.Name != "value"))
                sb.Append(a);

            sb.Append(">");

            sb.Append(e.Value.Value);

            sb.AppendLine(string.Format("</{0}>", e.Tag));
        }

        public void Visit(HtmlPasswordBox e)
        {
            sb.Append(Tabs(e.Depth));
            sb.Append(string.Format("<{0}", e.Tag));

            foreach (var a in e.Attributes.Where(a => a.IsSet))
                sb.Append(a);

            sb.AppendLine(" />");
        }

        public void Visit(HtmlDatePicker e)
        {
            sb.Append(Tabs(e.Depth));
            sb.Append(string.Format("<{0}", e.Tag));

            foreach (var a in e.Attributes.Where(a => a.IsSet))
                sb.Append(a);

            sb.AppendLine(" />");
        }

        public void Visit(HtmlCheckBox e)
        {
            sb.Append(Tabs(e.Depth));
            sb.Append(string.Format("<{0}", e.Tag));

            foreach (var a in e.Attributes.Where(a => a.IsSet))
                sb.Append(a);

            sb.AppendLine(" />");
        }

        public void Visit(HtmlSelect e)
        {
            sb.Append(Tabs(e.Depth));
            sb.Append(string.Format("<{0}", e.Tag));

            foreach (var a in e.Attributes.Where(a => a.IsSet))
                sb.Append(a);

            sb.AppendLine(">");

            foreach (var c in e.Contents)
                Visit(c);

            sb.Append(Tabs(e.Depth));
            sb.AppendLine(string.Format("</{0}>", e.Tag));
        }

        public void Visit(HtmlOption e)
        {
            sb.Append(Tabs(e.Depth));
            sb.Append(string.Format("<{0}", e.Tag));

            foreach (var a in e.Attributes.Where(a => a.IsSet))
                sb.Append(a);

            sb.Append(">");

            foreach (var c in e.Contents)
                Visit(c);

            sb.AppendLine(string.Format("</{0}>", e.Tag));
        }

        public void Visit(HtmlRadioGroup e)
        {
            sb.Append(Tabs(e.Depth));
            sb.Append(string.Format("<{0}", e.Tag));

            foreach (var a in e.Attributes.Where(a => a.IsSet))
                sb.Append(a);

            sb.AppendLine(">");

            foreach (var c in e.Contents)
                Visit(c);

            sb.Append(Tabs(e.Depth));
            sb.AppendLine(string.Format("</{0}>", e.Tag));
        }

        public void Visit(HtmlRadioButton e)
        {
            sb.Append(Tabs(e.Depth));
            sb.Append(string.Format("<{0}", e.Tag));

            foreach (var a in e.Attributes.Where(a => a.IsSet))
                sb.Append(a);

            sb.AppendLine(" />");
        }

        public void Visit(HtmlSubmit e)
        {
            sb.Append(Tabs(e.Depth));
            sb.Append(string.Format("<{0}", e.Tag));

            foreach (var a in e.Attributes.Where(a => a.IsSet))
                sb.Append(a);

            sb.Append(">");

            foreach (var c in e.Contents)
                Visit(c);

            sb.AppendLine(string.Format("</{0}>", e.Tag));
        }

        public void Visit(HtmlText t)
        {
            sb.Append(t.Text);
        }

        public Html2TextVisitor(HtmlContainer e)
        {
            Visit(e);
        }
    }
}
