using System;
using System.Collections.Generic;
using System.Linq;
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

using NLog;

namespace Form2WebApp.UserControls
{
    public partial class Form2SessionContact : UserControl
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
                OpenGroup("Container");


                Required = true;

                RequiredMessage = "Το πεδίο είναι υποχρεωτικό.";

                OrderElements = OrderElements.LabelMarkInput;


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


                CloseGroup("Container");
            }

            protected override void AddRules(List<FormRule> rules)
            {
                rules.Add((isPostBack, formItem, argument) =>
                {
                    FormSelect selCity = GetItem<FormSelect>("City");
                    FormSelect selMunicipality = GetItem<FormSelect>("MunicipalitySelect");
                    FormTextBox txtMunicipality = GetItem<FormTextBox>("MunicipalityTextBox");

                    selMunicipality.Hidden = true;
                    txtMunicipality.Hidden = true;

                    if (!selCity.Value.Any())
                        return;

                    long cityId = Convert.ToInt64(selCity.Value.Single().Value);

                    tblMunicipality[] municipalities = tblMunicipality.ListForcityId(cityId).ToArray();

                    if (municipalities.Length == 0)
                    {
                        txtMunicipality.Hidden = false;
                        return;
                    }

                    selMunicipality.Hidden = false;

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
                log.Info(new FormLogVisitor(FormGroup, "Ναι", "Όχι", true, true).Text);

                var emailVisitor = new FormEmailVisitor(FormGroup, "Ναι", "Όχι", true, false);

                page.Response.Write(emailVisitor.Subject);
                page.Response.Write("<br /><br />");
                page.Response.Write(emailVisitor.Body);
                page.Response.Write("<br /><br />");
            }
        }
    }
}
