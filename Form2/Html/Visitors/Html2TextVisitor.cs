using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Form2.Html.Content;
using Form2.Html.Content.Elements;
using Form2.Html.Content.Elements.Containers;
using Form2.Html.Content.Elements.Input;

namespace Form2.Html.Visitors
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

        public void Visit(HtmlContent h)
        {
            var mi = (from m in GetType().GetMethods()
                      where
                      m.ReturnType.Equals(typeof(void)) &&
                      m.GetParameters().Length == 1 &&
                      m.GetParameters()[0].ParameterType.Equals(h.GetType())
                      select m).SingleOrDefault();

            if (mi != null)
                mi.Invoke(this, new object[] { h });
            else
                throw new NotImplementedException();
        }

        public void Visit(HtmlDiv h)
        {
            sb.AppendLine();
            sb.Append(Tabs(h.Depth));
            sb.Append(string.Format("<{0}", h.Tag));

            foreach (var a in h.Attributes.Where(a => a.IsSet))
                sb.Append(a);

            sb.AppendLine(">");

            foreach (var c in h.Contents)
                Visit(c);

            sb.Append(Tabs(h.Depth));
            sb.AppendLine(string.Format("</{0}>", h.Tag));
        }

        public void Visit(HtmlFieldset h)
        {
            sb.AppendLine();
            sb.Append(Tabs(h.Depth));
            sb.Append(string.Format("<{0}", h.Tag));

            foreach (var a in h.Attributes.Where(a => a.IsSet))
                sb.Append(a);

            sb.AppendLine(">");

            foreach (var c in h.Contents)
                Visit(c);

            sb.Append(Tabs(h.Depth));
            sb.AppendLine(string.Format("</{0}>", h.Tag));
        }

        public void Visit(HtmlScript h)
        {
            sb.AppendLine();
            sb.Append(Tabs(h.Depth));
            sb.AppendLine(string.Format("<{0}>", h.Tag));

            foreach (var c in h.Contents)
                Visit(c);

            sb.Append(Tabs(h.Depth));
            sb.AppendLine(string.Format("</{0}>", h.Tag));
        }

        public void Visit(HtmlSpan h)
        {
            if (h.Container is HtmlDiv)
            {
                sb.AppendLine();
                sb.Append(Tabs(h.Depth));
            }

            sb.Append(string.Format("<{0}", h.Tag));

            foreach (var a in h.Attributes.Where(a => a.IsSet))
                sb.Append(a);

            sb.Append(">");

            foreach (var c in h.Contents)
                Visit(c);

            sb.Append(string.Format("</{0}>", h.Tag));

            if (h.Container is HtmlDiv)
                sb.AppendLine();
        }

        public void Visit(HtmlItalic h)
        {
            if (h.Container is HtmlDiv)
                sb.Append(Tabs(h.Depth));

            sb.Append(string.Format("<{0}", h.Tag));

            foreach (var a in h.Attributes.Where(a => a.IsSet))
                sb.Append(a);

            sb.Append(">");

            foreach (var c in h.Contents)
                Visit(c);

            sb.Append(string.Format("</{0}>", h.Tag));

            if (h.Container is HtmlDiv)
                sb.AppendLine();
        }

        public void Visit(HtmlLabel h)
        {
            sb.Append(Tabs(h.Depth));
            sb.Append(string.Format("<{0}", h.Tag));

            foreach (var a in h.Attributes.Where(a => a.IsSet))
                sb.Append(a);

            sb.Append(">");

            foreach (var c in h.Contents)
                Visit(c);

            sb.AppendLine(string.Format("</{0}>", h.Tag));
        }

        public void Visit(HtmlH3 h)
        {
            sb.Append(Tabs(h.Depth));
            sb.Append(string.Format("<{0}", h.Tag));

            foreach (var a in h.Attributes.Where(a => a.IsSet))
                sb.Append(a);

            sb.Append(">");

            foreach (var c in h.Contents)
                Visit(c);

            sb.AppendLine(string.Format("</{0}>", h.Tag));
        }

        public void Visit(HtmlHR h)
        {
            sb.Append(Tabs(h.Depth));
            sb.Append(string.Format("<{0}", h.Tag));

            foreach (var a in h.Attributes.Where(a => a.IsSet))
                sb.Append(a);

            sb.Append(">");
        }

        public void Visit(HtmlTextBox h)
        {
            sb.Append(Tabs(h.Depth));
            sb.Append(string.Format("<{0}", h.Tag));

            foreach (var a in h.Attributes.Where(a => a.IsSet))
                sb.Append(a);

            foreach (var e in h.Events.Where(e => e.IsSet))
                sb.Append(e);

            sb.AppendLine(">");
        }

        public void Visit(HtmlTextArea h)
        {
            sb.Append(Tabs(h.Depth));
            sb.Append(string.Format("<{0}", h.Tag));

            foreach (var a in h.Attributes.Where(a => a.IsSet && a.Name != "value"))
                sb.Append(a);

            sb.Append(">");

            sb.Append(h.Value.Value);

            sb.AppendLine(string.Format("</{0}>", h.Tag));
        }

        public void Visit(HtmlPasswordBox h)
        {
            sb.Append(Tabs(h.Depth));
            sb.Append(string.Format("<{0}", h.Tag));

            foreach (var a in h.Attributes.Where(a => a.IsSet))
                sb.Append(a);

            sb.AppendLine(">");
        }

        public void Visit(HtmlDateBox h)
        {
            sb.Append(Tabs(h.Depth));
            sb.Append(string.Format("<{0}", h.Tag));

            foreach (var a in h.Attributes.Where(a => a.IsSet))
                sb.Append(a);

            sb.AppendLine(">");
        }

        public void Visit(HtmlDatePicker h)
        {
            sb.Append(Tabs(h.Depth));
            sb.Append(string.Format("<{0}", h.Tag));

            foreach (var a in h.Attributes.Where(a => a.IsSet))
                sb.Append(a);

            sb.AppendLine(">");
        }

        public void Visit(HtmlCheckBox h)
        {
            sb.Append(Tabs(h.Depth));
            sb.Append(string.Format("<{0}", h.Tag));

            foreach (var a in h.Attributes.Where(a => a.IsSet))
                sb.Append(a);

            sb.AppendLine(">");
        }

        public void Visit(HtmlNumberBox h)
        {
            sb.Append(Tabs(h.Depth));
            sb.Append(string.Format("<{0}", h.Tag));

            foreach (var a in h.Attributes.Where(a => a.IsSet))
                sb.Append(a);

            sb.AppendLine(">");
        }

        public void Visit(HtmlSelect h)
        {
            sb.Append(Tabs(h.Depth));
            sb.Append(string.Format("<{0}", h.Tag));

            foreach (var a in h.Attributes.Where(a => a.IsSet))
                sb.Append(a);

            foreach (var e in h.Events.Where(e => e.IsSet))
                sb.Append(e);

            sb.AppendLine(">");

            foreach (var c in h.Contents)
                Visit(c);

            sb.Append(Tabs(h.Depth));
            sb.AppendLine(string.Format("</{0}>", h.Tag));
        }

        public void Visit(HtmlOption h)
        {
            sb.Append(Tabs(h.Depth));
            sb.Append(string.Format("<{0}", h.Tag));

            foreach (var a in h.Attributes.Where(a => a.IsSet))
                sb.Append(a);

            sb.Append(">");

            foreach (var c in h.Contents)
                Visit(c);

            sb.AppendLine(string.Format("</{0}>", h.Tag));
        }

        public void Visit(HtmlRadioGroup h)
        {
            sb.Append(Tabs(h.Depth));
            sb.Append(string.Format("<{0}", h.Tag));

            foreach (var a in h.Attributes.Where(a => a.IsSet))
                sb.Append(a);

            foreach (var e in h.Events.Where(e => e.IsSet))
                sb.Append(e);

            sb.AppendLine(">");

            foreach (var c in h.Contents)
                Visit(c);

            sb.Append(Tabs(h.Depth));
            sb.AppendLine(string.Format("</{0}>", h.Tag));
        }

        public void Visit(HtmlRadioButton h)
        {
            sb.Append(Tabs(h.Depth));
            sb.Append(string.Format("<{0}", h.Tag));

            foreach (var a in h.Attributes.Where(a => a.IsSet))
                sb.Append(a);

            sb.AppendLine(">");
        }

        public void Visit(HtmlButton h)
        {
            sb.Append(Tabs(h.Depth));
            sb.Append(string.Format("<{0}", h.Tag));

            foreach (var a in h.Attributes.Where(a => a.IsSet))
                sb.Append(a);

            foreach (var e in h.Events.Where(e => e.IsSet))
                sb.Append(e);

            sb.AppendLine(">");
        }

        public void Visit(HtmlSubmit h)
        {
            sb.Append(Tabs(h.Depth));
            sb.Append(string.Format("<{0}", h.Tag));

            foreach (var a in h.Attributes.Where(a => a.IsSet))
                sb.Append(a);

            foreach (var e in h.Events.Where(e => e.IsSet))
                sb.Append(e);

            sb.Append(">");

            foreach (var c in h.Contents)
                Visit(c);

            sb.AppendLine(string.Format("</{0}>", h.Tag));
        }

        public void Visit(HtmlText h)
        {
            sb.Append(h.Text);
        }

        public Html2TextVisitor(HtmlContainer h)
        {
            Visit(h);
        }
    }
}
