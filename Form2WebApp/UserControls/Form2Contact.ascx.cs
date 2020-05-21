using System;
using System.Collections.Generic;
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

namespace Form2WebApp.UserControls
{
    public partial class CtrlForm2Contact : UserControl
    {
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            ltrContent.Text = new Form().GetText(IsPostBack, Request.Form, Session);
        }

        class Form : Form2Base
        {
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

                AddItem(new FormDatePicker("DateOfBirth")
                {
                    Label = "Date of birth",

                    Content = "1999-03-12",

                    IsRequired = true,

                    Validator = (f) =>
                    {
                        if (f.Value > DateTime.Now)
                            return "Date of birth can not be in the future";

                        if (DateTime.Now - f.Value < TimeSpan.FromDays(18 * 365.25))
                            return "You must be at least 18 to use this site";

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

                    Validator = (t) =>
                    {
                        return !new Regex(@"^[a-zA-Z0-9._-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,4}$").IsMatch(t.Value) ? "Invalid Email" : "";
                    },
                });

                AddItem(new FormTextBox("Phone")
                {
                    Label = "Phone",

                    Content = "2105149035",

                    IsRequired = true,

                    PlaceHolder = "Enter your phone number",

                    Icon = FormIcon.Phone,

                    Validator = (t) =>
                    {
                        int digits = t.Value.Where(c => char.IsDigit(c)).Count();

                        if (!new Regex(@"^[0-9\(\)\+\ -]+$").IsMatch(t.Value) || digits < 10 || digits > 15)
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

                    Validator = (p) =>
                    {
                        if (GetItem<FormPasswordBox>("ConfirmPassword").Value != p.Value)
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

                    Validator = (p) =>
                    {
                        if (GetItem<FormPasswordBox>("Password").Value != p.Value)
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

                    Validator = (f) =>
                    {
                        return "";
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


                AddRule(() =>
                {
                    GetItem<FormSelect>("Orientation").IsDisabled =
                    GetItem<FormSelect>("Grade").Value.Any(o => o.Text == "Β' Λυκείου" || o.Text == "Γ' Λυκείου") == false;
                });
            }

            protected override void PerformAction()
            {
            }
        }
    }
}
