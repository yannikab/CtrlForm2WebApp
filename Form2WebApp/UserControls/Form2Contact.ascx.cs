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

            string formSessionKey = string.Format("{0}_{1}_{2}", Page.GetType().Name, GetType().Name, typeof(Form).Name);

            Form form = this.SessionGet(formSessionKey, () => new Form());

            if (form == null)
                return;

            form.SetPage(Page);

            FormCommander formCommander = new FormCommander(form);

            FormRenderer formRenderer = new FormRenderer(form);

            formCommander.HandleRequest(IsPostBack, Request);

            ltrContent.Text = formRenderer.Render();
        }

        class Form : FormModel
        {
            private static readonly Logger log = LogManager.GetCurrentClassLogger();

            private Page page;

            public void SetPage(Page page)
            {
                this.page = page;
            }

            protected override void CreateForm()
            {
                OpenSection("Container");

                ReadOnly = false;

                Required = false;

                //RequiredMark = "(required)";

                RequiredMessage = "Field is required";

                OrderElements = OrderElements.LabelMarkInput;


                AddItem(new FormTitle("Title")
                {
                    Content = "Form Title",
                });


                OpenSection("FirstName-LastName");

                AddItem(new FormTextBox("FirstName")
                {
                    Label = "First Name",

                    Content = "John",

                    Required = true,

                    PlaceHolder = "Enter your first name",
                });

                AddItem(new FormTextBox("LastName")
                {
                    Label = "Last Name",

                    Content = "Doe",

                    Required = true,

                    PlaceHolder = "Enter your last name",
                });

                CloseSection();


                OpenSection("DateOfBirth-DateOfMembership");

                AddItem(new FormDateBox("DateOfBirth")
                {
                    Label = "Date of birth",

                    Content = "1999-03-12",

                    Required = true,

                    Validator = (v) =>
                    {
                        if (v > DateTime.Now)
                            return "Date of birth can not be in the future";

                        if (DateTime.Now - v < TimeSpan.FromDays(18 * 365.25))
                            return "You must be at least 18 to use this site";

                        return "";
                    },
                });

                AddItem(new FormDatePicker("DateOfMembership", "dd/mm/yyyy")
                {
                    Label = "Membership renewal",

                    Content = string.Format("{0:00}/{1:00}/{2:0000}", DateTime.Now.Day, DateTime.Now.Month, DateTime.Now.Year - 1),

                    PlaceHolder = "dd/mm/yyyy",

                    Required = false,

                    Validator = (v) =>
                    {
                        if (v > DateTime.Now)
                            return "Membership renewal date can not be in the future";

                        if (DateTime.Now - v > TimeSpan.FromDays(365))
                            return "Only members that have renewed their memberships in the last year can participate";

                        return "";
                    },
                });

                CloseSection();


                OpenSection("Email-Phone");

                AddItem(new FormTextBox("Email")
                {
                    Label = "Email",

                    Content = "yanni.kab@gmail.com",

                    Required = true,

                    PlaceHolder = "Enter your email",

                    Icon = FormIcon.Envelope,

                    Validator = (v) =>
                    {
                        return !new Regex(@"^[a-zA-Z0-9._-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,4}$").IsMatch(v) ? "Invalid Email" : "";
                    },
                });

                AddItem(new FormTextBox("Phone")
                {
                    Label = "Phone",

                    Content = "2105149035",

                    Required = true,

                    PlaceHolder = "Enter your phone number",

                    Icon = FormIcon.Phone,

                    Validator = (v) =>
                    {
                        int digits = v.Where(c => char.IsDigit(c)).Count();

                        if (!new Regex(@"^[0-9\(\)\+\ -]+$").IsMatch(v) || digits < 10 || digits > 15)
                            return "Invalid Phone";

                        return "";
                    },
                });

                CloseSection();


                OpenSection("Password-ConfirmPassword");

                AddItem(new FormPasswordBox("Password")
                {
                    Label = "Password",

                    Content = "123",

                    Required = true,

                    PlaceHolder = "Enter your password",

                    Icon = FormIcon.Lock,

                    Validator = (v) =>
                    {
                        if (GetItem<FormPasswordBox>("ConfirmPassword").Value != v)
                            return "Passwords do not match";

                        return "";
                    },
                });


                AddItem(new FormPasswordBox("ConfirmPassword")
                {
                    Label = "Confirm Password",

                    Content = "123",

                    Required = true,

                    PlaceHolder = "Confirm your password",

                    Icon = FormIcon.Lock,

                    Validator = (v) =>
                    {
                        if (GetItem<FormPasswordBox>("Password").Value != v)
                            return "Passwords do not match";

                        return "";
                    },
                });

                CloseSection();


                OpenSection("Selects");

                AddItem(new FormSelect("Grade", false)
                {
                    Required = true,

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
                    Required = true,

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

                CloseSection();


                OpenSection("RadioGroups");

                AddItem(new FormRadioGroup("Contact")
                {
                    Required = true,

                    Label = "Contact method",

                    Content = new FormRadioButton[]
                    {
                        new FormRadioButton(0, "Phone") { IsSelected = false },
                        new FormRadioButton(1, "Mobile") { Hidden = false },
                        new FormRadioButton(2, "Email") { Disabled = true },
                        new FormRadioButton(3, "Post"),
                    },

                    Validator = (v) =>
                    {
                        if (v.Text == "Post")
                            return "Contact by post can not be used at the moment";

                        return "";
                    },

                    OrderElements = OrderElements.LabelMarkInput
                });

                CloseSection();

                AddItem(new FormNumberBox("YearsInService")
                {
                    Label = "Years in service",

                    Content = "1.2",

                    Min = 0,

                    Max = 10,

                    OrderNumberBox = OrderNumberBox.NumberDecrIncr,

                    Validator = (v) =>
                    {
                        if (Math.Truncate(v) != v)
                            return "Please enter an integer number";

                        return "";
                    }
                }) ;

                AddItem(new FormTextArea("Message")
                {
                    Label = "Message",

                    Content = "My Message",

                    Required = true,

                    PlaceHolder = "Enter your message",

                    Rows = 4,

                    Columns = 50,
                });


                AddItem(new FormCheckBox("AcceptTerms")
                {
                    Label = "Accept Terms",

                    Content = true,

                    Required = true,

                    RequiredMessage = "You must agree with the terms of use",
                });


                AddItem(new FormButton("Submit")
                {
                    Content = "Submit",

                    Disabled = false,

                    IsSubmit = true,
                });


                CloseSection();
            }

            protected override void AddRules(List<FormRule> rules)
            {
                rules.Add((isPostBack, formItem, argument) =>
                {
                    GetItem<FormSelect>("Orientation").Disabled =
                    GetItem<FormSelect>("Grade").Value.Any(o => o.Text == "Β' Λυκείου" || o.Text == "Γ' Λυκείου") == false;
                });
            }

            protected override void PerformAction()
            {
                log.Info(new FormLogVisitor(FormSection, "Ναι", "Όχι").Text);

                var emailVisitor = new FormEmailVisitor(FormSection, "Ναι", "Όχι");

                page.Response.Write(emailVisitor.Subject);
                page.Response.Write("<br><br>");
                page.Response.Write(emailVisitor.Body);
                page.Response.Write("<br><br>");
            }
        }
    }
}
