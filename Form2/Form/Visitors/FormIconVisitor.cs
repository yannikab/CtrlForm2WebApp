using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

using Form2.Form.Content;
using Form2.Form.Content.Items;
using Form2.Form.Content.Items.Input;
using Form2.Form.Content.Items.Input.Selectors;
using Form2.Form.Enums;
using Form2.Html.Content.Elements;
using Form2.Html.Content.Elements.Containers;
using Form2.Html.Content.Elements.Input;

namespace Form2.Form.Visitors
{
    [SuppressMessage("Style", "IDE0019:Use pattern matching", Justification = "<Pending>")]
    [SuppressMessage("Style", "IDE0028:Simplify collection initialization", Justification = "<Pending>")]
    [SuppressMessage("Style", "IDE0029:Use coalesce expression", Justification = "<Pending>")]

    public class FormIconVisitor
    {
        #region Fields

        private static readonly MethodInfo[] visitorMethods;

        private readonly bool prepend;

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

        private IEnumerable<string> IconClass(FormIcon formIcon)
        {
            List<string> iconClass = new List<string>();

            iconClass.Add("formIcon");
            iconClass.Add("fa");

            switch (formIcon)
            {
                default:
                    iconClass.Add(string.Format("fa-{0}", formIcon.ToString().ToLower()));
                    break;

                case FormIcon.IdCard:
                    iconClass.Add("fa-id-card-o");
                    //iconClass.Add("fa-id-card");
                    break;

                case FormIcon.Facebook:
                    iconClass.Add("fa-facebook-square");
                    //iconClass.Add("fa-facebook-official");
                    break;

                case FormIcon.Twitter:
                    iconClass.Add("fa-twitter-square");
                    //iconClass.Add("fa-twitter");
                    break;

                case FormIcon.LinkedIn:
                    iconClass.Add("fa-linkedin-square");
                    //iconClass.Add("fa-linkedin");
                    break;

                case FormIcon.YouTube:
                    iconClass.Add("fa-youtube");
                    //iconClass.Add("fa-youtube-play");
                    break;

                case FormIcon.NotSet:
                    iconClass.Clear();
                    break;
            }

            return iconClass;
        }

        public virtual void Visit(FormTitle formTitle, HtmlContainer htmlContainer)
        {
        }

        public virtual void Visit(FormLabel formLabel, HtmlContainer htmlContainer)
        {
        }

        public virtual void Visit(FormButton formButton, HtmlContainer htmlContainer)
        {
        }

        public virtual void Visit(FormGroup formGroup, HtmlContainer htmlContainer)
        {
            for (int i = 0; i < formGroup.Contents.Count; i++)
            {
                if (formGroup.Contents[i].IsHidden)
                    continue;

                Visit(formGroup.Contents[i], (HtmlContainer)htmlContainer.Contents[i]);
            }
        }

        public virtual void Visit(FormTextBox formTextBox, HtmlContainer htmlContainer)
        {
            if (formTextBox.Icon == FormIcon.NotSet)
                return;

            HtmlTextBox htmlTextBox = null;
            int i;

            for (i = 0; i < htmlContainer.Contents.Count; i++)
            {
                if (htmlContainer.Contents[i] is HtmlTextBox)
                {
                    htmlTextBox = (HtmlTextBox)htmlContainer.Contents[i];
                    break;
                }
            }

            if (htmlTextBox == null)
                return;

            htmlContainer.Remove(htmlTextBox);

            HtmlItalic htmlItalic = new HtmlItalic();
            htmlItalic.Class.AddRange(IconClass(formTextBox.Icon));

            HtmlDiv htmlDiv = new HtmlDiv();
            htmlDiv.Add(htmlTextBox);
            htmlDiv.Insert(prepend ? 0 : 1, htmlItalic);

            htmlContainer.Insert(i, htmlDiv);
        }

        public virtual void Visit(FormTextArea formTextArea, HtmlContainer htmlContainer)
        {
        }

        public virtual void Visit(FormPasswordBox formPasswordBox, HtmlContainer htmlContainer)
        {
            if (formPasswordBox.Icon == FormIcon.NotSet)
                return;

            HtmlTextBox htmlTextBox = null;
            int i;

            for (i = 0; i < htmlContainer.Contents.Count; i++)
            {
                if (htmlContainer.Contents[i] is HtmlTextBox)
                {
                    htmlTextBox = (HtmlTextBox)htmlContainer.Contents[i];
                    break;
                }
            }

            if (htmlTextBox == null)
                return;

            htmlContainer.Remove(htmlTextBox);

            HtmlItalic htmlItalic = new HtmlItalic();
            htmlItalic.Class.AddRange(IconClass(formPasswordBox.Icon));

            HtmlDiv htmlDiv = new HtmlDiv();
            htmlDiv.Add(htmlTextBox);
            htmlDiv.Insert(prepend ? 0 : 1, htmlItalic);

            htmlContainer.Insert(i, htmlDiv);
        }

        public virtual void Visit(FormDateBox formDateBox, HtmlContainer htmlContainer)
        {
        }

        public virtual void Visit(FormDatePicker formDatePicker, HtmlContainer htmlContainer)
        {
            if (formDatePicker.Icon == FormIcon.NotSet)
                return;

            HtmlTextBox htmlTextBox = null;
            int i;

            for (i = 0; i < htmlContainer.Contents.Count; i++)
            {
                if (htmlContainer.Contents[i] is HtmlTextBox)
                {
                    htmlTextBox = (HtmlTextBox)htmlContainer.Contents[i];
                    break;
                }
            }

            if (htmlTextBox == null)
                return;

            htmlContainer.Remove(htmlTextBox);

            HtmlItalic htmlItalic = new HtmlItalic();
            htmlItalic.Class.AddRange(IconClass(formDatePicker.Icon));

            HtmlDiv htmlDiv = new HtmlDiv();
            htmlDiv.Add(htmlTextBox);
            htmlDiv.Insert(prepend ? 0 : 1, htmlItalic);

            htmlContainer.Insert(i, htmlDiv);
        }

        public virtual void Visit(FormCheckBox formCheckBox, HtmlContainer htmlContainer)
        {
        }

        public virtual void Visit(FormNumberBox formNumberBox, HtmlContainer htmlContainer)
        {
        }

        public virtual void Visit(FormNumberSpinner formNumberSpinner, HtmlContainer htmlContainer)
        {
        }

        public virtual void Visit(FormSelect formSelect, HtmlContainer htmlContainer)
        {
        }

        public virtual void Visit(FormRadioGroup formRadioGroup, HtmlContainer htmlContainer)
        {
        }

        #endregion


        #region Constructors

        static FormIconVisitor()
        {
            visitorMethods = (from mi in typeof(FormIconVisitor).GetMethods()
                              where
                              mi.Name == "Visit" &&
                              mi.ReturnType.Equals(typeof(void)) &&
                              mi.GetParameters().Length == 2 &&
                              mi.GetParameters()[0].ParameterType.Equals(typeof(FormContent)) == false &&
                              mi.GetParameters()[1].ParameterType.Equals(typeof(HtmlContainer))
                              select mi).ToArray();
        }

        public FormIconVisitor(FormGroup formGroup, HtmlContainer htmlContainer, bool prepend)
        {
            this.prepend = prepend;

            if (!formGroup.IsHidden)
                Visit(formGroup, htmlContainer);
        }

        #endregion
    }
}
