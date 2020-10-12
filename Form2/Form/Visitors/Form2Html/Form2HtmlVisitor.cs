using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

using Form2.Form.Content;
using Form2.Form.Content.Items;
using Form2.Form.Interfaces;
using Form2.Html.Content;
using Form2.Html.Content.Elements;
using Form2.Html.Content.Elements.Containers;

namespace Form2.Form.Visitors
{
    [SuppressMessage("Style", "IDE0018:Inline variable declaration", Justification = "<Pending>")]

    public partial class Form2HtmlVisitor
    {
        #region Fields

        private static readonly MethodInfo[] visitorMethods;

        private readonly bool initialize;

        private readonly bool verbose;

        private readonly ScriptRegistry scriptRegistry = new ScriptRegistry();

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

        public void Visit(FormContent formContent, HtmlContainer htmlContainer)
        {
            var mi = visitorMethods.SingleOrDefault(m => m.GetParameters()[0].ParameterType.Equals(formContent.GetType()));

            if (mi != null)
                mi.Invoke(this, new object[] { formContent, htmlContainer });
            else
                throw new NotImplementedException();
        }

        private void AddLabel(FormInput formInput, HtmlElement htmlElement, HtmlDiv htmlDiv)
        {
            if (formInput.IsHidden)
                return;

            if (!formInput.IsLabeled)
                return;

            HtmlLabel htmlLabel = new HtmlLabel(verbose ? formInput.Path : "");
            htmlLabel.Class.Add("formInputLabel");
            htmlLabel.For.Value = htmlElement.Id.Value;
            htmlLabel.Add(new HtmlText(formInput.Label));

            htmlDiv.Add(htmlLabel);
        }

        private void AddMark(FormInput formInput, HtmlElement htmlElement, HtmlDiv htmlDiv)
        {
            if (formInput.IsHidden)
                return;

            if (formInput.IsDisabled)
                return;

            if (formInput is IReadOnly && (formInput as IReadOnly).IsReadOnly)
                return;

            if (!formInput.IsLabeled)
                return;

            HtmlLabel htmlLabelMark;

            if (formInput.IsMarkedRequired)
            {
                htmlLabelMark = new HtmlLabel("");
                htmlLabelMark.Class.Add("formMarkRequired");
                htmlLabelMark.For.Value = htmlElement.Id.Value;
                htmlLabelMark.Add(new HtmlText(formInput.RequiredMark.Replace(" ", "&nbsp;")));
            }
            else if (formInput.IsMarkedOptional)
            {
                htmlLabelMark = new HtmlLabel("");
                htmlLabelMark.Class.Add("formMarkOptional");
                htmlLabelMark.For.Value = htmlElement.Id.Value;
                htmlLabelMark.Add(new HtmlText(formInput.OptionalMark.Replace(" ", "&nbsp;")));
            }
            else
            {
                throw new ApplicationException("Form input item should be either marked required or optional in this context.");
            }

            htmlDiv.Add(htmlLabelMark);
        }

        private void AddLabelMark(FormInput formInput, HtmlElement htmlElement, HtmlDiv htmDiv)
        {
            if (!(formInput.IsMarkedRequired || formInput.IsMarkedOptional))
            {
                AddLabel(formInput, htmlElement, htmDiv);
                return;
            }

            HtmlDiv htmlDivLabelMark = new HtmlDiv("");

            AddLabel(formInput, htmlElement, htmlDivLabelMark);
            AddMark(formInput, htmlElement, htmlDivLabelMark);

            if (htmlDivLabelMark.Contents.Count > 0)
                htmDiv.Add(htmlDivLabelMark);
        }

        private void AddMarkLabel(FormInput formInput, HtmlElement htmlElement, HtmlDiv htmDiv)
        {
            if (!(formInput.IsMarkedRequired || formInput.IsMarkedOptional))
            {
                AddLabel(formInput, htmlElement, htmDiv);
                return;
            }

            HtmlDiv htmlDivLabelMark = new HtmlDiv("");

            AddMark(formInput, htmlElement, htmlDivLabelMark);
            AddLabel(formInput, htmlElement, htmlDivLabelMark);

            if (htmlDivLabelMark.Contents.Count > 0)
                htmDiv.Add(htmlDivLabelMark);
        }

        #endregion


        #region Constructors

        static Form2HtmlVisitor()
        {
            visitorMethods = (from mi in typeof(Form2HtmlVisitor).GetMethods()
                              where
                              mi.Name == "Visit" &&
                              mi.ReturnType.Equals(typeof(void)) &&
                              mi.GetParameters().Length == 2 &&
                              mi.GetParameters()[0].ParameterType.Equals(typeof(FormContent)) == false &&
                              mi.GetParameters()[1].ParameterType.Equals(typeof(HtmlContainer))
                              select mi).ToArray();
        }

        public Form2HtmlVisitor(FormModel formModel, bool verbose)
        {
            initialize = formModel.Submitted;

            this.verbose = verbose;

            Visit(formModel.FormGroup, null);

            AddScripts();
        }

        private void AddScripts()
        {
            foreach (var s in scriptRegistry.SelectedScripts)
            {
                HtmlScript htmlScript = new HtmlScript();
                Html.Add(htmlScript);
                htmlScript.Add(new HtmlText(FormatScript(scriptRegistry.GetText(s), htmlScript.Depth + 1)));
            }
        }

        private string FormatScript(string script, int depth)
        {
            var split = script.Split(new string[] { Environment.NewLine }, StringSplitOptions.None).Select(s => !string.IsNullOrWhiteSpace(s) ? s : "");

            var leadingWhitespace = split.Where(s => s != "").Min(s => new Regex("^ *").Match(s).Length);

            StringBuilder sb = new StringBuilder();

            foreach (var s in split)
            {
                if (s != "")
                    sb.Append(new string('\t', depth));

                sb.AppendLine(s != "" ? s.Substring(leadingWhitespace).Replace("    ", "\t") : "");
            }

            return sb.ToString();
        }

        #endregion


        #region ScriptRegistry

        private partial class ScriptRegistry
        {
            private static readonly Dictionary<string, string> scriptRegistry = new Dictionary<string, string>();

            public IEnumerable<string> Scripts
            {
                get { return scriptRegistry.Keys.Cast<string>(); }
            }

            private readonly ISet<string> selectedScripts = new HashSet<string>();

            public IEnumerable<string> SelectedScripts
            {
                get { return selectedScripts; }
            }

            public void Include(string scriptName)
            {
                if (!scriptRegistry.ContainsKey(scriptName))
                    throw new ArgumentOutOfRangeException("scriptName");

                selectedScripts.Add(scriptName);
            }

            public string GetText(string scriptName)
            {
                if (!scriptRegistry.ContainsKey(scriptName))
                    throw new ArgumentOutOfRangeException("scriptName");

                return scriptRegistry[scriptName];
            }

            static ScriptRegistry()
            {
                var properties = typeof(ScriptRegistry)
                    .GetProperties(BindingFlags.NonPublic | BindingFlags.Static)
                    .Where(p => p.CanRead && !p.CanWrite && p.PropertyType == typeof(string));

                foreach (var p in properties)
                {
                    string scriptText = (string)p.GetValue(null, null);

                    if (string.IsNullOrWhiteSpace(scriptText))
                        throw new ApplicationException("Invalid script text.");

                    scriptRegistry.Add(p.Name, scriptText);
                }
            }
        }

        #endregion
    }
}
