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

        public void Visit(FormContent formItem, HtmlContainer htmlContainer)
        {
            var mi = (from m in GetType().GetMethods()
                      where
                      m.ReturnType.Equals(typeof(void)) &&
                      m.GetParameters().Length == 2 &&
                      m.GetParameters()[0].ParameterType.Equals(formItem.GetType()) &&
                      m.GetParameters()[1].ParameterType.Equals(typeof(HtmlContainer))
                      select m).SingleOrDefault();

            if (mi != null)
                mi.Invoke(this, new object[] { formItem, htmlContainer });
            else
                throw new NotImplementedException();
        }

        void AddMark(IRequired formItem, HtmlContainer htmlContainer)
        {
            if (formItem is IHidden && (formItem as IHidden).IsHidden)
                return;

            if (formItem is IDisabled && (formItem as IDisabled).IsDisabled)
                return;

            if (formItem is IReadOnly && (formItem as IReadOnly).IsReadOnly)
                return;

            if (formItem.IsRequired)
            {
                if (formItem.IsRequiredInLabel && !string.IsNullOrWhiteSpace(formItem.RequiredMark))
                {
                    HtmlLabel htmlLabel = new HtmlLabel("");
                    htmlLabel.Class.Add("formMarkRequired");
                    htmlLabel.Add(new HtmlText(formItem.RequiredMark.Replace(" ", "&nbsp;")));
                    htmlContainer.Add(htmlLabel);
                }
            }
            else
            {
                if (formItem.IsOptionalInLabel && !string.IsNullOrWhiteSpace(formItem.OptionalMark))
                {
                    HtmlLabel htmlLabel = new HtmlLabel("");
                    htmlLabel.Class.Add("formMarkOptional");
                    htmlLabel.Add(new HtmlText(formItem.OptionalMark.Replace(" ", "&nbsp;")));
                    htmlContainer.Add(htmlLabel);
                }
            }
        }

        #endregion


        #region Constructors

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
