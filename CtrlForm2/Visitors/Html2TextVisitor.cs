using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using UserControls.CtrlForm2.HtmlElements.HtmlGroups;
using UserControls.CtrlForm2.HtmlElements.HtmlItems;

namespace UserControls.CtrlForm2.Visitors
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

        public void Visit(HtmlItem e)
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
            //sb.AppendLine(string.Format(@"<{0}{1}{2}{3}>",
            //    e.Tag,
            //    e.Id.ToString(),
            //    e.Class.ToString(),
            //    e.Hidden.ToString()
            //));
            sb.Append(string.Format("<{0}", e.Tag));

            foreach (var a in e.Attributes.Where(a => a.IsSet))
                sb.Append(a);

            sb.AppendLine(">");

            foreach (var c in e.Items)
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

            //sb.Append(string.Format(@"<{0}{1}{2}{3}>",
            //    e.Tag,
            //    e.Id.ToString(),
            //    e.Class.ToString(),
            //    e.Hidden.ToString()
            //));
            sb.Append(string.Format("<{0}", e.Tag));

            foreach (var a in e.Attributes.Where(a => a.IsSet))
                sb.Append(a);

            sb.Append(">");

            foreach (var c in e.Items)
                Visit(c);

            sb.Append(string.Format("</{0}>", e.Tag));

            if (e.Container is HtmlDiv)
                sb.AppendLine();
        }

        public void Visit(HtmlItalic e)
        {
            //sb.Append(string.Format(@"<{0}{1}{2}{3}>",
            //    e.Tag,
            //    e.Id.ToString(),
            //    e.Class.ToString(),
            //    e.Hidden.ToString()
            //));
            sb.Append(string.Format("<{0}", e.Tag));

            foreach (var a in e.Attributes.Where(a => a.IsSet))
                sb.Append(a);

            sb.Append(">");

            foreach (var c in e.Items)
                Visit(c);

            sb.Append(string.Format("</{0}>", e.Tag));
        }

        public void Visit(HtmlLabel e)
        {
            sb.Append(Tabs(e.Depth));
            //sb.Append(string.Format(@"<{0}{1}{2}{3}{4}>",
            //    e.Tag,
            //    e.Id.ToString(),
            //    e.Class.ToString(),
            //    e.Hidden.ToString(),
            //    e.For.ToString()
            //));
            sb.Append(string.Format("<{0}", e.Tag));

            foreach (var a in e.Attributes.Where(a => a.IsSet))
                sb.Append(a);

            sb.Append(">");

            foreach (var c in e.Items)
                Visit(c);

            sb.AppendLine(string.Format("</{0}>", e.Tag));
        }

        public void Visit(HtmlTextBox e)
        {
            sb.Append(Tabs(e.Depth));
            //sb.AppendLine(string.Format(@"<{0}{1}{2}{3}{4}{5}{6}{7}{8}>",
            //    e.Tag,
            //    e.Id.ToString(),
            //    e.Class.ToString(),
            //    e.Hidden.ToString(),
            //    e.ReadOnly.ToString(),
            //    e.Type.ToString(),
            //    e.Name.ToString(),
            //    e.Value.ToString(),
            //    e.PlaceHolder.ToString()
            //));
            sb.Append(string.Format("<{0}", e.Tag));

            foreach (var a in e.Attributes.Where(a => a.IsSet))
                sb.Append(a);

            sb.AppendLine(">");
        }

        public void Visit(HtmlTextArea e)
        {
            sb.Append(Tabs(e.Depth));
            //sb.Append(string.Format(@"<{0}{1}{2}{3}{4}{5}{6}{7}{8}>",
            //    e.Tag,
            //    e.Id.ToString(),
            //    e.Class.ToString(),
            //    e.Hidden.ToString(),
            //    e.ReadOnly.ToString(),
            //    e.Name.ToString(),
            //    e.PlaceHolder.ToString(),
            //    e.Rows.ToString(),
            //    e.Cols.ToString()
            //));
            sb.Append(string.Format("<{0}", e.Tag));

            foreach (var a in e.Attributes.Where(a => a.IsSet && a.Name != "value"))
                sb.Append(a);

            sb.Append(">");

            sb.Append(e.Value.Value);

            sb.AppendLine(string.Format("</{0}>", e.Tag));
        }

        public void Visit(HtmlSubmit e)
        {
            sb.Append(Tabs(e.Depth));
            //sb.Append(string.Format(@"<{0}{1}{2}{3}{4}>",
            //    e.Tag,
            //    e.Id.ToString(),
            //    e.Class.ToString(),
            //    e.Hidden.ToString(),
            //    e.Type.ToString()
            //));
            sb.Append(string.Format("<{0}", e.Tag));

            foreach (var a in e.Attributes.Where(a => a.IsSet))
                sb.Append(a);

            sb.Append(">");

            foreach (var c in e.Items)
                Visit(c);

            sb.AppendLine(string.Format("</{0}>", e.Tag));
        }

        public void Visit(HtmlText t)
        {
            sb.Append(t.Text);
        }

        public Html2TextVisitor(HtmlItem e)
        {
            Visit(e);
        }
    }
}
