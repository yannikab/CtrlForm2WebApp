using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using CtrlForm2.Form.Enums;
using CtrlForm2.Form.Items;
using CtrlForm2.Form.Items.Input;
using CtrlForm2.Form.Items.Input.Selectors;
using CtrlForm2.Form.Selectables;
using CtrlForm2.UserControls;

namespace CtrlForm2WebApp.UserControls
{
    public partial class CtrlForm2Contact : CtrlForm2Base
    {
        protected override void OnLoad(EventArgs e)
        {
            LtrContent = ltrContent;

            base.OnLoad(e);
        }

        protected override void CreateForm()
        {
            OpenGroup("Container");

            IsReadOnly = false;

            IsRequired = false;

            //RequiredMark = "(required)";

            RequiredMessage = "Field is required";

            //ElementOrder = ElementOrder.LabelMarkInput;


            AddItem(new FormTitle("Title")
            {
                Label = "Form Title",
            });


            OpenGroup("FirstName-LastName");

            AddItem(new FormTextBox("FirstName")
            {
                Label = "First Name",

                Text = "John",

                IsRequired = false,

                PlaceHolder = "Enter your first name",
            });

            AddItem(new FormTextBox("LastName")
            {
                Label = "Last Name",

                Text = "Doe",

                IsRequired = true,

                PlaceHolder = "Enter your last name",

                IsDisabled = false,
            });

            AddItem(new FormDatePicker("DateOfBirth")
            {
                Label = "Date of birth",

                IsRequired = true,

                Validator = (f) =>
                {
                    if (f.Date > DateTime.Now)
                        return "Date of birth can not be in the future";

                    if (DateTime.Now - f.Date < TimeSpan.FromDays(18 * 365.25))
                        return "You must be at least 18 to use this site";

                    return "";
                },
            });

            CloseGroup();


            OpenGroup("Email-Phone");

            AddItem(new FormTextBox("Email")
            {
                Label = "Email",

                IsRequired = false,

                PlaceHolder = "Enter your email",

                Icon = FormIcon.Envelope,

                Validator = (t) =>
                {
                    return !new Regex(@"^[a-zA-Z0-9._-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,4}$").IsMatch(t.Text) ? "Invalid Email" : "";
                },
            });

            AddItem(new FormTextBox("Phone")
            {
                Label = "Phone",

                IsRequired = true,

                PlaceHolder = "Enter your phone number",


                Icon = FormIcon.Phone,

                Validator = (t) =>
                {
                    int digits = t.Text.Where(c => char.IsDigit(c)).Count();

                    if (!new Regex(@"^[0-9\(\)\+\ -]+$").IsMatch(t.Text) || digits < 10 || digits > 15)
                        return "Invalid Phone";

                    return "";
                },
            });

            CloseGroup();


            OpenGroup("Password-ConfirmPassword");

            AddItem(new FormPasswordBox("Password")
            {
                Label = "Password",

                IsRequired = true,

                PlaceHolder = "Enter your password",

                Icon = FormIcon.Lock,

                Validator = (p) =>
                {
                    if (GetItem<FormPasswordBox>("ConfirmPassword").Text != p.Text)
                        return "Passwords do not match";

                    return "";
                },
            });


            AddItem(new FormPasswordBox("ConfirmPassword")
            {
                Label = "Confirm Password",

                IsRequired = true,

                PlaceHolder = "Confirm your password",

                Icon = FormIcon.Lock,

                Validator = (p) =>
                {
                    if (GetItem<FormPasswordBox>("Password").Text != p.Text)
                        return "Passwords do not match";

                    return "";
                },
            });

            CloseGroup();


            OpenGroup("Select");

            AddItem(new FormSelect("Colors", false)
            {
                IsRequired = true,

                Label = "Favorite color",

                Options = new FormOption[] {
                    new FormOption(0, "Red"),
                    new FormOption(1, "Green") { IsSelected = true },
                    new FormOption(2, "Blue"),
                    new FormOption(2, "Brown") { IsSelected = true },
                    new FormOption(2, "Grey"),
                    },
            });

            CloseGroup();


            AddItem(new FormTextArea("Message")
            {
                Label = "Message",

                Text = "My Message",

                IsRequired = false,

                PlaceHolder = "Enter your message",

                Rows = 4,

                Columns = 50,
            });


            AddItem(new FormCheckBox("AcceptTerms")
            {
                Label = "Accept Terms",

                TextChecked = "Yes",

                TextNotChecked = "No",

                IsRequired = true,

                RequiredMessage = "You must agree with the terms of use",
            });


            AddItem(new FormSubmit("Submit")
            {
                Text = "Submit",

                IsDisabled = false,
            });


            CloseGroup();
        }
    }
}
