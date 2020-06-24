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
using Form2.Form.Visitors;

using Form2WebApp.Data;

namespace Form2WebApp.UserControls
{
    public partial class Form2SessionContact : UserControl
    {
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            ltrContent.Text = new Form(Page).GetText(IsPostBack, Request.Form, Session);
        }

        class Form : Form2Base
        {
            private readonly Page page;

            public Form(Page page)
            {
                this.page = page;
            }

            protected override void CreateForm()
            {
                OpenGroup("Container");


                IsRequired = true;

                RequiredMessage = "Το πεδίο είναι υποχρεωτικό.";

                ElementOrder = ElementOrder.LabelMarkInput;


                AddItem(new FormTitle("")
                {
                    Content = "Επιλογή Πόλης - Δήμου/Κοινότητας",
                });


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


                AddRule((isPostBack, eventTarget, eventArgument) =>
                {
                    FormSelect selCity = GetItem<FormSelect>("City");
                    FormSelect selMunicipality = GetItem<FormSelect>("MunicipalitySelect");
                    FormTextBox txtMunicipality = GetItem<FormTextBox>("MunicipalityTextBox");

                    selMunicipality.IsHidden = true;
                    txtMunicipality.IsHidden = true;

                    if (!selCity.Value.Any())
                        return;

                    long cityId = Convert.ToInt64(selCity.Value.Single().Value);

                    tblMunicipality[] municipalities = tblMunicipality.ListForcityId(cityId).ToArray();

                    if (municipalities.Length == 0)
                    {
                        txtMunicipality.IsHidden = false;
                        return;
                    }

                    selMunicipality.IsHidden = false;

                    FormOption prevMunicipality = selMunicipality.Value.SingleOrDefault();

                    selMunicipality.Content = municipalities.Select(m => new FormOption((int)m.id, m.name));

                    if (prevMunicipality == null)
                        return;

                    foreach (var o in selMunicipality.Content)
                        o.IsSelected = o.Equals(prevMunicipality);
                });
            }

            protected override void PerformAction()
            {
                var emailVisitor = new FormEmailVisitor(FormGroup, "Ναι", "Όχι");

                page.Response.Write(emailVisitor.Subject);
                page.Response.Write("<br /><br />");
                page.Response.Write(emailVisitor.Body);
                page.Response.Write("<br /><br />");
            }
        }
    }
}
