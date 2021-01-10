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

        private HtmlLabel GetLabel(FormInput formInput, HtmlElement htmlElement)
        {
            HtmlLabel htmlLabel = verbose ? new HtmlLabel(formInput.Path) : new HtmlLabel();
            htmlLabel.Class.Add("formInputLabel");
            htmlLabel.For.Value = htmlElement.Id.Value;
            htmlLabel.Add(new HtmlText(formInput.Label));

            return htmlLabel;
        }

        private string GetMarkClass(FormInput formInput)
        {
            return formInput.IsMarkedRequired ? "formMarkRequired" : formInput.IsMarkedOptional ? "formMarkOptional" : "";
        }

        private HtmlText GetMarkHtmlText(FormInput formInput)
        {
            return new HtmlText((formInput.IsMarkedRequired ? formInput.RequiredMark : formInput.IsMarkedOptional ? formInput.OptionalMark : "").Replace(" ", "&nbsp;"));
        }

        private void AddLabel(FormInput formInput, HtmlElement htmlElement, HtmlDiv htmlDiv)
        {
            if (formInput.IsHidden)
                return;

            if (string.IsNullOrWhiteSpace(formInput.Label))
                return;

            htmlDiv.Add(GetLabel(formInput, htmlElement));
        }

        private void AddMark(FormInput formInput, HtmlElement htmlElement, HtmlDiv htmlDiv)
        {
            if (!(formInput.IsMarkedRequired || formInput.IsMarkedOptional))
                return;

            HtmlLabel htmlLabel = new HtmlLabel();
            htmlLabel.Class.Add(GetMarkClass(formInput));
            htmlLabel.Add(GetMarkHtmlText(formInput));
            htmlLabel.For.Value = htmlElement.Id.Value;

            htmlDiv.Add(htmlLabel);
        }

        private void AddLabelMark(FormInput formInput, HtmlElement htmlElement, HtmlDiv htmlDiv, bool labelFirst)
        {
            if (formInput.IsHidden)
                return;

            if (string.IsNullOrWhiteSpace(formInput.Label))
                return;

            HtmlLabel htmlLabel = GetLabel(formInput, htmlElement);

            if (!(formInput.IsMarkedRequired || formInput.IsMarkedOptional))
            {
                htmlDiv.Add(htmlLabel);
                return;
            }

            HtmlSpan htmlSpan = new HtmlSpan();
            htmlSpan.Class.Add(GetMarkClass(formInput));
            htmlSpan.Add(GetMarkHtmlText(formInput));

            if (labelFirst)
                htmlLabel.Add(htmlSpan);
            else
                htmlLabel.Insert(0, htmlSpan);

            htmlDiv.Add(htmlLabel);
        }

        private void AddLabelMark(FormInput formInput, HtmlElement htmlElement, HtmlDiv htmlDiv)
        {
            AddLabelMark(formInput, htmlElement, htmlDiv, true);
        }

        private void AddMarkLabel(FormInput formInput, HtmlElement htmlElement, HtmlDiv htmlDiv)
        {
            AddLabelMark(formInput, htmlElement, htmlDiv, false);
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
                html.Add(htmlScript);
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
