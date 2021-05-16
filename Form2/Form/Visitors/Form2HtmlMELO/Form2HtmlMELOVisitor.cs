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
using Form2.Form.Visitors.Interfaces;
using Form2.Html.Content;
using Form2.Html.Content.Elements;
using Form2.Html.Content.Elements.Containers;

namespace Form2.Form.Visitors
{
    [SuppressMessage("Style", "IDE0018:Inline variable declaration", Justification = "<Pending>")]
    [SuppressMessage("CodeQuality", "IDE0051:Remove unused private members", Justification = "<Pending>")]

    public partial class Form2HtmlMELOVisitor : IForm2HtmlVisitor
    {
        #region Fields

        private static readonly MethodInfo[] visitorMethods;

        private readonly bool initialize;

        private readonly bool verbose;

        private readonly ScriptRegistry scriptRegistry;

        private string firstInvalidId;

        private HtmlContainer html;

        private readonly List<HtmlContainer> scripts;

        #endregion


        #region Properties

        public HtmlContainer Html
        {
            get { return html; }
            protected set { html = value; }
        }

        public IReadOnlyList<HtmlContainer> Scripts
        {
            get { return scripts; }
        }

        #endregion


        #region Methods

        public void Visit(FormContent formContent, HtmlContainer htmlContainer)
        {
            var mi = visitorMethods.SingleOrDefault(m => m.GetParameters()[0].ParameterType.Equals(formContent.GetType()));

            if (mi != null)
                mi.Invoke(this, new object[] { formContent, htmlContainer });
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

        private void AddLabel(FormInput formInput, HtmlElement htmlElement, HtmlContainer htmlContainer)
        {
            if (formInput.IsHidden)
                return;

            if (string.IsNullOrWhiteSpace(formInput.Label))
                return;

            htmlContainer.Add(GetLabel(formInput, htmlElement));
        }

        private void AddMark(FormInput formInput, HtmlElement htmlElement, HtmlContainer htmlContainer)
        {
            if (!(formInput.IsMarkedRequired || formInput.IsMarkedOptional))
                return;

            HtmlLabel htmlLabel = new HtmlLabel();
            htmlLabel.Class.Add(GetMarkClass(formInput));
            htmlLabel.Add(GetMarkHtmlText(formInput));
            htmlLabel.For.Value = htmlElement.Id.Value;

            htmlContainer.Add(htmlLabel);
        }

        private void AddLabelMark(FormInput formInput, HtmlElement htmlElement, HtmlContainer htmlContainer, bool labelFirst)
        {
            if (formInput.IsHidden)
                return;

            if (string.IsNullOrWhiteSpace(formInput.Label))
                return;

            HtmlLabel htmlLabel = GetLabel(formInput, htmlElement);

            if (!(formInput.IsMarkedRequired || formInput.IsMarkedOptional))
            {
                htmlContainer.Add(htmlLabel);
                return;
            }

            HtmlSpan htmlSpan = new HtmlSpan();
            htmlSpan.Class.Add(GetMarkClass(formInput));
            htmlSpan.Add(GetMarkHtmlText(formInput));

            if (labelFirst)
                htmlLabel.Add(htmlSpan);
            else
                htmlLabel.Insert(0, htmlSpan);

            htmlContainer.Add(htmlLabel);
        }

        private void AddLabelMark(FormInput formInput, HtmlElement htmlElement, HtmlContainer htmlContainer)
        {
            AddLabelMark(formInput, htmlElement, htmlContainer, true);
        }

        private void AddMarkLabel(FormInput formInput, HtmlElement htmlElement, HtmlContainer htmlContainer)
        {
            AddLabelMark(formInput, htmlElement, htmlContainer, false);
        }

        #endregion


        #region Constructors

        static Form2HtmlMELOVisitor()
        {
            visitorMethods = (from mi in typeof(Form2HtmlMELOVisitor).GetMethods()
                              where
                              mi.Name == "Visit" &&
                              mi.ReturnType.Equals(typeof(void)) &&
                              mi.GetParameters().Length == 2 &&
                              mi.GetParameters()[0].ParameterType.Equals(typeof(FormContent)) == false &&
                              mi.GetParameters()[1].ParameterType.Equals(typeof(HtmlContainer))
                              select mi).ToArray();
        }

        public Form2HtmlMELOVisitor(FormModel formModel, bool verbose)
        {
            html = null;

            scripts = new List<HtmlContainer>();

            if (formModel == null)
                return;

            initialize = formModel.Submitted;

            this.verbose = verbose;

            scriptRegistry = new ScriptRegistry();

            Visit(formModel.FormGroup, null);

            AddScripts();
        }

        private void AddScripts()
        {
            if (firstInvalidId != null)
            {
                HtmlScript script = new HtmlScript();
                script.Add(new HtmlText(GetFocusFirstInvalidScript(firstInvalidId)));
                scripts.Add(script);
            }

            foreach (var s in scriptRegistry.SelectedScripts)
            {
                HtmlScript script = new HtmlScript();
                script.Add(new HtmlText(s));
                scripts.Add(script);
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

        private string GetFocusFirstInvalidScript(string id)
        {
            string script = @"
                function focusFirstInvalid() {{
                    document.getElementById('{0}').scrollIntoView({{ behavior: 'auto', block: 'center', inline: 'center' }});
                    //document.getElementById('{0}').focus();
                }}

                $(document).ready(focusFirstInvalid);

                //if (document.readyState === 'loading') {{
                //    document.addEventListener('DOMContentLoaded', focusFirstInvalid);
                //}} else {{
                //    focusFirstInvalid();
                //}}
                ";

            return string.Format(script, id);
        }

        #endregion


        #region ScriptRegistry

        private partial class ScriptRegistry
        {
            IEnumerable<MethodInfo> scriptMethods = typeof(ScriptRegistry)
                .GetMethods(BindingFlags.NonPublic | BindingFlags.Static)
                .Where(m => m.GetParameters().All(p => p.ParameterType == typeof(string)) && m.ReturnType == typeof(string));

            private readonly ISet<string> selectedScripts = new HashSet<string>();

            public IEnumerable<string> SelectedScripts
            {
                get { return selectedScripts; }
            }

            public void Include(string scriptName, params string[] parameters)
            {
                var sm = scriptMethods.SingleOrDefault(m => m.Name == scriptName);

                if (sm == null)
                    throw new ArgumentOutOfRangeException("scriptName");

                string scriptText = (string)sm.Invoke(null, parameters);

                selectedScripts.Add(scriptText);
            }
        }

        #endregion
    }
}
