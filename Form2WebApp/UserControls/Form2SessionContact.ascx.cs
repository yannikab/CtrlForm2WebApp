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

using Form2WebApp.Data;

namespace Form2WebApp.UserControls
{
    public partial class Form2SessionContact : UserControl
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


                IsRequired = true;

                RequiredMessage = "Το πεδίο είναι υποχρεωτικό.";

                ElementOrder = ElementOrder.LabelMarkInput;


                AddItem(new FormSelect("City", false)
                {
                    Label = "Πόλη",

                    Header = new FormOption("Επιλέξτε..."),

                    Content = tblCity.ListAll().Select(c => new FormOption((int)c.id, c.name)),

                    IsPostBack = true,
                });


                AddItem(new FormSelect("MunicipalitySelect", false)
                {
                    Label = "Δήμος",

                    Header = new FormOption("Επιλέξτε..."),

                    Content = new FormOption[]
                    {
                        new FormOption("1"),
                        new FormOption("2"),
                        new FormOption("3"),
                        new FormOption("3"),
                    },
                });


                AddItem(new FormTextBox("MunicipalityTextBox")
                {
                    Label = "Δήμος",
                });


                AddItem(new FormButton("Submit")
                {
                    Content = "Υποβολή",

                    IsSubmit = true
                });


                CloseGroup();


                AddRule(() =>
                {
                    //GetItem<FormSelect>("Orientation").IsDisabled =
                    //GetItem<FormSelect>("Grade").Value.Any(o => o.Text == "Β' Λυκείου" || o.Text == "Γ' Λυκείου") == false;
                });
            }

            protected override void PerformAction()
            {
            }
        }
    }
}
