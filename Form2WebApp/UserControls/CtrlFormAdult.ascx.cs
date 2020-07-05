using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.SessionState;
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
    [SuppressMessage("Style", "IDE0017:Simplify object initialization", Justification = "<Pending>")]

    public partial class CtrlFormAdult : UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Form form = this.SessionGet(GetType().Name, () => new Form());

            if (form == null)
                return;

            form.SetPage(Page);

            ltrForm.Text = form.GetText(IsPostBack, Request.Form, Session);
        }

        private class Form : Form2Base
        {
            private static readonly Logger log = LogManager.GetCurrentClassLogger();

            #region Fields

            private Page page;

            private FormDatePicker dtpDateOfBirth;
            private FormRadioGroup rdgEmploymentStatus;
            private FormSelect selEmploymentDuration;
            private FormSelect selEducationalLevel;

            private FormSelect selPopulation;
            private FormSelect selCity;

            private FormSelect selMunicipality;
            private FormTextBox txtMunicipality;

            #endregion


            public void SetPage(Page page)
            {
                this.page = page;
            }


            #region Resources

            private readonly string resChoose = "Choose";
            private readonly string resFieldRequired = "Field Required";
            private readonly string resDateInvalid = "Date Invalid";
            private readonly string resYes = "Yes";
            private readonly string resNo = "No";

            private readonly string resAdults = "Adults";
            private readonly string resDateOfBirth = "Birth Date";
            private readonly string resEmploymentStatus = "Employment Status";
            //private readonly string resEmployed = "EmploymentStatusEmployed";
            //private readonly string resUnemployed = "EmploymentStatusUnemployed";
            //private readonly string resStudent = "EmploymentStatusStudent";
            private readonly string resEmploymentDuration = "Employment Duration";

            private readonly string resEducationalLevel = "Educational Level";

            private readonly string resCity = "City";
            private readonly string resAreaOfResidence = "Area Of Residence";
            private readonly string resMunicipality = "Municipality";

            private readonly string resSend = "Send";

            #endregion


            #region CreateForm()

            protected override void CreateForm()
            {
                OpenSection("Container");

                #region Defaults

                ElementOrder = ElementOrder.LabelMarkInput;

                Required = true;

                RequiredMessage = resFieldRequired;

                #endregion


                #region Title

                AddItem(new FormTitle("Title")
                {
                    Content = resAdults,
                });

                #endregion


                #region DateOfBirth

                AddItem(dtpDateOfBirth = new FormDatePicker("DateOfBirth", "dd/mm/yyyy")
                {
                    Label = resDateOfBirth,

                    PlaceHolder = resDateOfBirth,

                    Validator = (v) =>
                    {
                        if (v > DateTime.Now)
                            return resDateInvalid;

                        return "";
                    }
                });

                #endregion


                #region EmploymentStatus

                AddItem(rdgEmploymentStatus = new FormRadioGroup("EmploymentStatus")
                {
                    Label = resEmploymentStatus,

                    Required = true,

                    IsPostBack = true,

                    Content = tblEmploymentStatus.ListAll().Select(s => new FormRadioButton(s.id, s.descr))
                });

                #endregion


                #region EmploymentDuration

                AddItem(selEmploymentDuration = new FormSelect("EmploymentDuration", false)
                {
                    Label = resEmploymentDuration,

                    Header = new FormOption(resChoose),

                    Hidden = true,

                    Content = tblEmploymentDuration.ListAll().Select(d => new FormOption(d.id, d.descr))
                });

                #endregion


                #region EducationalLevel

                AddItem(selEducationalLevel = new FormSelect("EducationalLevel", false)
                {
                    Label = resEducationalLevel,

                    Header = new FormOption(resChoose),

                    Content = tblEducationalLevel.ListAll().Select(l => new FormOption(l.id, l.name))
                });

                #endregion


                #region Population

                AddItem(selPopulation = new FormSelect("Population", false)
                {
                    Label = resCity,

                    Header = new FormOption(resChoose),

                    Content = tblPopulation.ListAll().Select(p => new FormOption(p.id, p.descr))
                });

                #endregion


                #region City

                AddItem(selCity = new FormSelect("City", false)
                {
                    Label = resAreaOfResidence,

                    Header = new FormOption(resChoose),

                    IsPostBack = true,

                    Content = tblCity.ListAll().OrderBy(c => c.name).Select(c => new FormOption(c.id, c.name))
                });

                #endregion


                #region Municipality

                AddItem(selMunicipality = new FormSelect("MunicipalitySelect", false)
                {
                    Label = resMunicipality,

                    Header = new FormOption(resChoose),

                    Hidden = true,
                });


                AddItem(txtMunicipality = new FormTextBox("MunicipalityTextBox")
                {
                    Label = resMunicipality,

                    Hidden = true,
                });

                #endregion


                #region Submit

                AddItem(new FormButton("Submit")
                {
                    Content = resSend,

                    IsSubmit = true
                });

                #endregion

                CloseSection();
            }

            #endregion


            #region AddRules()

            protected override void AddRules(List<FormRule> rules)
            {
                rules.Add((isPostBack, eventTarget, eventArgument) =>
                {
                    if (!isPostBack)
                        return;

                    if (eventTarget != selCity.BaseId)
                        return;

                    selMunicipality.Hidden = true;
                    txtMunicipality.Hidden = true;

                    if (!selCity.Value.Any())
                        return;

                    long cityId = Convert.ToInt64(selCity.Value.Single().Value);

                    var municipalities = tblMunicipality.ListForcityId(cityId).OrderBy(m => m.name).ToList();

                    if (municipalities.Any())
                    {
                        selMunicipality.Content = municipalities.Select(m => new FormOption(m.id, m.name));
                        selMunicipality.Hidden = false;
                    }
                    else
                    {
                        txtMunicipality.Content = "";
                        txtMunicipality.Hidden = false;
                    }
                });

                rules.Add((isPostBack, eventTarget, eventArgument) =>
                {
                    if (!isPostBack)
                        return;

                    if (eventTarget != rdgEmploymentStatus.BaseId)
                        return;

                    selEmploymentDuration.Hidden = true;

                    if (rdgEmploymentStatus.Value == null)
                        return;

                    if (rdgEmploymentStatus.Value.Value != "1")
                        return;

                    foreach (var c in selEmploymentDuration.Content)
                        c.IsSelected = false;

                    selEmploymentDuration.Hidden = false;

                });
            }

            #endregion


            #region PerformAction()

            protected override void PerformAction()
            {
                log.Info(new FormLogVisitor(FormSection, resYes, resNo).Text);

                tblRegisterAdult tra = new tblRegisterAdult()
                {
                    dateOfBirth = dtpDateOfBirth.Value,
                    employmentStatusId = rdgEmploymentStatus.Value.Numeric,
                    employmentDurationId = rdgEmploymentStatus.Value.Numeric == 1 ? (long?)selEmploymentDuration.Value.Single().Numeric : null,
                    educationalLevelId = selEducationalLevel.Value.Single().Numeric,
                    populationId = selPopulation.Value.Single().Numeric,
                    cityId = selCity.Value.Single().Numeric,
                    municipality = selMunicipality.IsRequired ? selMunicipality.Value.Single().Text : txtMunicipality.IsRequired ? txtMunicipality.Value : null,
                    userId = 1,
                };

                if (tra.Insert() == 1)
                {
                    string script = "bootbox.alert({message: 'Registration Success'});";
                    ScriptManager.RegisterStartupScript(page, page.GetType(), "RegistrationSuccess", script, true);
                }
                else
                {
                    string script = "bootbox.alert({message: 'Registration Failure'});";
                    ScriptManager.RegisterStartupScript(page, page.GetType(), "RegistrationFailure", script, true);
                }
            }

            #endregion
        }
    }
}
