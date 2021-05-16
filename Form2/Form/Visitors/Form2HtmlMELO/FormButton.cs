using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Form2.Form.Content.Items;
using Form2.Form.Enums;
using Form2.Html.Content;
using Form2.Html.Content.Elements;
using Form2.Html.Content.Elements.Containers;

namespace Form2.Form.Visitors
{
    public partial class Form2HtmlMELOVisitor
    {
        public virtual void Visit(FormButton formButton, HtmlContainer htmlContainer)
        {
            HtmlDiv htmlDiv = verbose ? new HtmlDiv(formButton.Path) : new HtmlDiv();

            htmlDiv.Class.Add("formButton");

            if (!string.IsNullOrWhiteSpace(formButton.CssClass))
                htmlDiv.Class.AddRange(formButton.CssClass.Split(' ').Where(s => s != string.Empty));

            if (!string.IsNullOrWhiteSpace(formButton.Path))
                htmlDiv.Class.Add(string.Format("{0}{1}", "formId", formButton.Path));

            htmlDiv.Hidden.Value = formButton.IsHidden;

            htmlContainer.Add(htmlDiv);

            HtmlButton htmlButton;

            string proceed = "Proceed";
            string cancel = "Cancel";

            switch (formButton.ConfirmationType)
            {
                case ConfirmationType.BootBox:
                    htmlButton = new HtmlButton(formButton.Path, string.Format("ButtonConfirmBootbox('{0}', '{1}', '{2}');", formButton.ConfirmationMessage, formButton.Path, formButton.Parameter));
                    scriptRegistry.Include("ButtonConfirmBootbox", proceed, cancel);
                    break;

                case ConfirmationType.Gritter:
                    htmlButton = new HtmlButton(formButton.Path);
                    scriptRegistry.Include("ButtonConfirmGritter", proceed, cancel);
                    break;

                default:
                case ConfirmationType.NotSet:
                    htmlButton = new HtmlButton(formButton.Path);
                    break;
            }

            htmlButton.Class.AddRange(new string[] { "btnFix", "btn", "mt-2" });

            switch (formButton.Type)
            {
                case ButtonType.Primary:
                case ButtonType.Secondary:
                case ButtonType.Success:
                case ButtonType.Danger:
                    htmlButton.Class.Add(string.Format("btn-{0}", formButton.Type.ToString().ToLower()));
                    break;
                default:
                case ButtonType.NotSet:
                    break;
            }

            htmlButton.Disabled.Value = formButton.IsDisabled;
            htmlDiv.Add(htmlButton);

            htmlButton.Value.Value = formButton.Content;
        }


        #region ScriptRegistry

        [SuppressMessage("Code Quality", "IDE0051:Remove unused private members", Justification = "<Pending>")]

        private partial class ScriptRegistry
        {
            private static string ButtonConfirmBootbox(string proceed, string cancel)
            {
                return string.Format(@"

                    function ButtonConfirmBootbox(message, path, parameter) {{

                        bootbox.dialog({{
                            message: message,
                            buttons:
                            {{
                                ""success"":
                                {{
                                    ""label"": '{0}',
                                    ""className"": 'btn btn-primary',
                                    ""callback"": function() {{
                                        __doPostBack(path, parameter);
                                    }}
                                }},
                                ""danger"":
                                {{
                                    ""label"": '{1}',
                                    ""className"": 'btn btn-secondary',
                                    ""callback"": function() {{
                                    }}
                                }}
                            }}
                        }});
                    }}
                    ", proceed, cancel);
            }
        }

        #endregion
    }
}
