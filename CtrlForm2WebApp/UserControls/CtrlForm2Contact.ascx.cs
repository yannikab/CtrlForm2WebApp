using System;
using System.Collections.Generic;
using System.Linq;
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
        protected override void OnInit(EventArgs e)
        {
            LtrContent = ltrContent;

            base.OnInit(e);
        }

        protected void Page_Load(object sender, EventArgs e)
        {
        }

        protected override void CreateForm()
        {
            OpenGroup("Container");

            SetElementOrder(ElementOrder.LabelMarkInput);

            //SetRequiredMark("(required)");


            AddItem(new FormTitle("Title")
            {
                Label = "Form Title"
            });


            OpenGroup("FirstName-LastName");

            AddItem(new FormTextBox("FirstName")
            {
                Label = "First Name",

                InitialText = "John",

                IsRequired = true,

                PlaceHolder = "Enter your first name",

                Icon = FormIcon.Phone,
            });

            AddItem(new FormTextBox("LastName")
            {
                Label = "Last Name",

                InitialText = "Doe",

                IsRequired = true,

                PlaceHolder = "Enter your last name",

                Icon = FormIcon.Envelope,
            });

            CloseGroup();


            OpenGroup("Message");

            AddItem(new FormTextArea("Message")
            {
                Label = "Message",

                InitialText = "",

                IsRequired = false,

                PlaceHolder = "Message",

                Rows = 4,

                Columns = 50,
            });

            CloseGroup();


            AddItem(new FormSubmit("Submit")
            {
                Text = "Submit"
            });


            CloseGroup();
        }
    }
}
