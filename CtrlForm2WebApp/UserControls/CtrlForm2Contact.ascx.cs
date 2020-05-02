using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using UserControls.CtrlForm2;
using UserControls.CtrlForm2.FormElements;
using UserControls.CtrlForm2.FormElements.FormItems;
using UserControls.CtrlForm2.FormElements.FormItems.FormItemsInput;

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
                Label = "Form Title"
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
                }
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


            AddItem(new FormSubmit("Submit")
            {
                Text = "Submit"
            });


            CloseGroup();
        }
    }
}
