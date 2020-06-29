using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using Form2;
using Form2.Form.Content.Items;
using Form2.Form.Content.Items.Input;
using Form2.Form.Content.Items.Input.Selectors;
using Form2.Form.Enums;
using Form2.Form.Selectables;
using Form2.Form.Visitors;

using NLog;

namespace Form2WebApp.UserControls
{
    public partial class CtrlForm2Contact : UserControl
    {
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            Form form = this.SessionGet(GetType().Name, () => new Form());

            if (form == null)
                return;

            form.SetPage(Page);

            ltrContent.Text = form.GetText(IsPostBack, Request.Form, Session);
        }

        class Form : Form2Base
        {
            private static readonly Logger log = LogManager.GetCurrentClassLogger();

            private Page page;

            public void SetPage(Page page)
            {
                this.page = page;
            }

            protected override void CreateForm()
            {
                OpenGroup("Container");

                IsReadOnly = false;

                IsRequired = false;

                //RequiredMark = "(required)";

                RequiredMessage = "Field is required";

                ElementOrder = ElementOrder.LabelMarkInput;


                AddItem(new FormTitle("Title")
                {
                    Content = "Form Title",
                });


                OpenGroup("FirstName-LastName");

                AddItem(new FormTextBox("FirstName")
                {
                    Label = "First Name",

                    Content = "John",

                    IsRequired = true,

                    PlaceHolder = "Enter your first name",
                });

                AddItem(new FormTextBox("LastName")
                {
                    Label = "Last Name",

                    Content = "Doe",

                    IsRequired = true,

                    PlaceHolder = "Enter your last name",
                });

                CloseGroup();

                OpenGroup("DateOfBirth-DateOfMembership");

                AddItem(new FormDateBox("DateOfBirth")
                {
                    Label = "Date of birth",

                    Content = "1999-03-12",

                    IsRequired = true,

                    Validator = (f) =>
                    {
                        if (f.Value == null)
                            return "";

                        if (f.Value > DateTime.Now)
                            return "Date of birth can not be in the future";

                        if (DateTime.Now - f.Value < TimeSpan.FromDays(18 * 365.25))
                            return "You must be at least 18 to use this site";

                        return "";
                    },
                });

                AddItem(new FormDatePicker("DateOfMembership", "dd/mm/yyyy")
                {
                    Label = "Membership renewal",

                    Content = string.Format("{0:00}/{1:00}/{2:0000}", DateTime.Now.Day, DateTime.Now.Month, DateTime.Now.Year - 1),

                    PlaceHolder = "dd/mm/yyyy",

                    IsRequired = false,

                    Validator = (f) =>
                    {
                        if (f.Value == null)
                            return "";

                        if (f.Value > DateTime.Now)
                            return "Membership renewal date can not be in the future";

                        if (DateTime.Now - f.Value > TimeSpan.FromDays(365))
                            return "Only members that have renewed their memberships in the last year can participate";

                        return "";
                    },
                });

                CloseGroup();


                OpenGroup("Email-Phone");

                AddItem(new FormTextBox("Email")
                {
                    Label = "Email",

                    Content = "yanni.kab@gmail.com",

                    IsRequired = true,

                    PlaceHolder = "Enter your email",

                    Icon = FormIcon.Envelope,

                    Validator = (f) =>
                    {
                        if (f.Value == "")
                            return "";

                        return !new Regex(@"^[a-zA-Z0-9._-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,4}$").IsMatch(f.Value) ? "Invalid Email" : "";
                    },
                });

                AddItem(new FormTextBox("Phone")
                {
                    Label = "Phone",

                    Content = "2105149035",

                    IsRequired = true,

                    PlaceHolder = "Enter your phone number",

                    Icon = FormIcon.Phone,

                    Validator = (f) =>
                    {
                        if (f.Value == "")
                            return "";

                        int digits = f.Value.Where(c => char.IsDigit(c)).Count();

                        if (!new Regex(@"^[0-9\(\)\+\ -]+$").IsMatch(f.Value) || digits < 10 || digits > 15)
                            return "Invalid Phone";

                        return "";
                    },
                });

                CloseGroup();


                OpenGroup("Password-ConfirmPassword");

                AddItem(new FormPasswordBox("Password")
                {
                    Label = "Password",

                    Content = "123",

                    IsRequired = true,

                    PlaceHolder = "Enter your password",

                    Icon = FormIcon.Lock,

                    Validator = (f) =>
                    {
                        if (f.Value == "")
                            return "";

                        if (GetItem<FormPasswordBox>("ConfirmPassword").Value != f.Value)
                            return "Passwords do not match";

                        return "";
                    },
                });


                AddItem(new FormPasswordBox("ConfirmPassword")
                {
                    Label = "Confirm Password",

                    Content = "123",

                    IsRequired = true,

                    PlaceHolder = "Confirm your password",

                    Icon = FormIcon.Lock,

                    Validator = (f) =>
                    {
                        if (f.Value == "")
                            return "";

                        if (GetItem<FormPasswordBox>("Password").Value != f.Value)
                            return "Passwords do not match";

                        return "";
                    },
                });

                CloseGroup();


                OpenGroup("Selects");

                AddItem(new FormSelect("Grade", false)
                {
                    IsRequired = true,

                    Label = "Τάξη",

                    Header = new FormOption("Επιλέξτε..."),

                    Content = new FormOption[]
                    {
                        new FormOption("Δημοτικό"),
                        new FormOption("Α' Γυμνασίου"),
                        new FormOption("Β' Γυμνασίου"),
                        new FormOption("Γ' Γυμνασίου"),
                        new FormOption("Α' Λυκείου"),
                        new FormOption("Β' Λυκείου"),
                        new FormOption("Γ' Λυκείου")
                    },

                    IsPostBack = true,
                });

                AddItem(new FormSelect("Orientation", false)
                {
                    IsRequired = true,

                    Label = "Κατεύθυνση",

                    Header = new FormOption("Επιλέξτε..."),

                    Content = new FormOption[]
                    {
                        new FormOption("Ανθρωπιστικών Σπουδών"),
                        new FormOption("Θετικών Σπουδών"),
                        new FormOption("Σπουδών Υγείας"),
                        new FormOption("Σπουδών Οικόνομίας και Πληροφορικής"),
                    },
                });

                CloseGroup();


                OpenGroup("RadioGroups");

                AddItem(new FormRadioGroup("Contact")
                {
                    IsRequired = true,

                    Label = "Contact method",

                    Content = new FormRadioButton[]
                    {
                        new FormRadioButton(0, "Phone") { IsSelected = false },
                        new FormRadioButton(1, "Mobile") { IsHidden = true },
                        new FormRadioButton(2, "Email") { IsDisabled = true },
                        new FormRadioButton(3, "Post"),
                    },

                    Validator = (f) =>
                    {
                        if (f.Value == null)
                            return "";

                        if (f.Value.Text == "Post")
                            return "Contact by post can not be used at the moment";

                        return "";
                    },

                    ElementOrder = ElementOrder.LabelMarkInput
                });

                CloseGroup();


                AddItem(new FormTextArea("Message")
                {
                    Label = "Message",

                    Content = "My Message",

                    IsRequired = true,

                    PlaceHolder = "Enter your message",

                    Rows = 4,

                    Columns = 50,
                });


                AddItem(new FormCheckBox("AcceptTerms")
                {
                    Label = "Accept Terms",

                    Content = CheckBoxState.On,

                    IsRequired = true,

                    RequiredMessage = "You must agree with the terms of use",
                });


                AddItem(new FormButton("Submit")
                {
                    Content = "Submit",

                    IsDisabled = false,

                    IsSubmit = true,
                });


                CloseGroup();
            }

            protected override void AddRules(List<FormRule> rules)
            {
                rules.Add((isPostBack, eventTarget, eventArgument) =>
                {
                    GetItem<FormSelect>("Orientation").IsDisabled =
                    GetItem<FormSelect>("Grade").Value.Any(o => o.Text == "Β' Λυκείου" || o.Text == "Γ' Λυκείου") == false;
                });
            }

            protected override void PerformAction()
            {
                log.Info(new FormLogVisitor(FormGroup, "Ναι", "Όχι").Text);

                var emailVisitor = new FormEmailVisitor(FormGroup, "Ναι", "Όχι");

                page.Response.Write(emailVisitor.Subject);
                page.Response.Write("<br /><br />");
                page.Response.Write(emailVisitor.Body);
                page.Response.Write("<br /><br />");
            }
        }
    }
}
