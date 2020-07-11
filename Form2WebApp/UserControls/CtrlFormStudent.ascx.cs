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

    public partial class CtrlFormStudent : UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string formSessionKey = string.Format("{0}_{1}_{2}", Page.GetType().Name, GetType().Name, typeof(Form).Name);

            Form form = this.SessionGet(formSessionKey, () => new Form());

            if (form == null)
                return;

            form.SetPage(Page);

            Form2Commander form2Commander = new Form2Commander(form);

            form2Commander.HandleRequest(IsPostBack, Request, Session);

            Form2Renderer form2Renderer = new Form2Renderer(form);

            ltrForm.Text = form2Renderer.Render();
        }

        private class Form : Form2Model
        {
            private static readonly Logger log = LogManager.GetCurrentClassLogger();

            #region Fields

            private Page page;

            private FormDatePicker dtpDateOfBirth;
            private FormSelect selPopulation;
            private FormSelect selCity;

            private FormSelect selMunicipality;
            private FormTextBox txtMunicipality;

            private FormTextBox txtEmail;

            private FormSelect selEducationalStage;
            private FormSelect selEducationalGrade;

            private FormSelect selOrientationGroup;

            private FormTextBox txtCoachingSchool;
            private FormRadioGroup rdgPrivateLessons;

            #endregion


            public void SetPage(Page page)
            {
                this.page = page;
            }


            #region Resources

            private readonly string resChoose = "Choose";
            private readonly string resFieldRequired = "Field Required";
            private readonly string resDateInvalid = "Date Invalid";
            private readonly string resEmailInvalid = "Email Invalid";
            private readonly string resYes = "Yes";
            private readonly string resNo = "No";

            private readonly string resStudents = "Students";
            private readonly string resDateOfBirth = "Birth Date";
            private readonly string resCity = "City";
            private readonly string resAreaOfResidence = "Area Of Residence";
            private readonly string resMunicipality = "Municipality";
            private readonly string resEmail = "Email";

            private readonly string resEducationalStage = "School";
            //private readonly string resElementarySchool = "ElementarySchool";
            //private readonly string resMiddleSchool = "MiddleSchool";
            //private readonly string resHighSchool = "HighSchool";
            private readonly string resEducationalGrade = "School Grade";

            private readonly string resOrientationGroup = "Orientation Group";

            private readonly string resCoachingSchool = "Coaching School";
            private readonly string resPrivateLessons = "Private Lessons";

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
                    Content = resStudents,
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


                #region Email

                AddItem(txtEmail = new FormTextBox("Email")
                {
                    Label = resEmail.ToLower(),

                    Validator = (v) =>
                    {
                        if (!new Regex(@"^[a-zA-Z0-9._-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]*$").IsMatch(v.Trim()))
                            return resEmailInvalid;

                        return "";
                    },
                });

                #endregion


                #region EducationalStage

                AddItem(selEducationalStage = new FormSelect("EducationalStage", false)
                {
                    Label = resEducationalStage,

                    Header = new FormOption(resChoose),

                    IsPostBack = true,

                    Content = tblEducationalStage.ListAll().Select(s => new FormOption(s.id, s.name))
                });

                #endregion


                #region EducationalGrade

                AddItem(selEducationalGrade = new FormSelect("EducationalGrade", false)
                {
                    Label = resEducationalGrade,

                    Header = new FormOption(resChoose),

                    Hidden = true,

                    IsPostBack = true,
                });

                #endregion


                #region OrientationGroup

                AddItem(selOrientationGroup = new FormSelect("OrientationGroup", false)
                {
                    Label = resOrientationGroup,

                    Header = new FormOption(resChoose),

                    Hidden = true,

                    Content = tblOrientationGroup.ListAll().Select(o => new FormOption(o.id, o.name))
                });

                #endregion


                #region CoachingSchool

                AddItem(txtCoachingSchool = new FormTextBox("CoachingSchool")
                {
                    Label = resCoachingSchool,

                    Required = false,
                });

                #endregion


                #region PrivateLessons

                AddItem(rdgPrivateLessons = new FormRadioGroup("PrivateLessons")
                {
                    Label = resPrivateLessons,

                    Required = false,

                    Content = new FormRadioButton[]
                    {
                        new FormRadioButton(0, resYes) { IsSelected = false },
                        new FormRadioButton(1, resNo) { IsSelected = false },
                    }
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
                rules.Add((isPostBack, formItem, argument) =>
                {
                    if (!isPostBack)
                        return;

                    if (formItem != selCity)
                        return;

                    selMunicipality.Hidden = true;
                    txtMunicipality.Hidden = true;

                    if (!selCity.HasValue)
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

                rules.Add((isPostBack, formItem, argument) =>
                {
                    if (!isPostBack)
                        return;

                    if (formItem != selEducationalStage)
                        return;

                    selEducationalGrade.Hidden = true;
                    selOrientationGroup.Hidden = true;

                    if (!selEducationalStage.HasValue)
                        return;

                    long stageId = Convert.ToInt64(selEducationalStage.Value.Single().Value);

                    var grades = tblEducationalGrade.ListForstageId(stageId).ToList();

                    if (!grades.Any())
                        return;

                    selEducationalGrade.Content = grades.Select(g => new FormOption(g.id, g.name));
                    selEducationalGrade.Hidden = false;
                });

                rules.Add((isPostBack, formItem, argument) =>
                {
                    if (!isPostBack)
                        return;

                    if (formItem != selEducationalGrade)
                        return;

                    selOrientationGroup.Hidden = true;

                    if (selEducationalStage.IsHidden)
                        return;

                    if (!selEducationalStage.HasValue)
                        return;

                    if (selEducationalStage.Value.Single().Value != "3")
                        return;

                    if (selEducationalGrade.IsHidden)
                        return;

                    if (!selEducationalGrade.HasValue)
                        return;

                    if (selEducationalGrade.Value.Single().Value != "12")
                        return;

                    foreach (var o in selOrientationGroup.Content)
                        o.IsSelected = false;

                    selOrientationGroup.Hidden = false;
                });
            }

            #endregion


            #region PerformAction()

            protected override void PerformAction()
            {
                log.Info(new FormLogVisitor(FormSection, resYes, resNo).Text);

                tblRegisterStudent trs = new tblRegisterStudent()
                {
                    dateOfBirth = dtpDateOfBirth.Value,
                    populationId = selPopulation.Value.Single().Numeric,
                    cityId = selCity.Value.Single().Numeric,
                    municipality = selMunicipality.IsRequired ? selMunicipality.Value.Single().Text : txtMunicipality.IsRequired ? txtMunicipality.Value : null,
                    email = txtEmail.Value,
                    educationalGradeId = selEducationalGrade.Value.Single().Numeric,
                    orientationGroupId = selOrientationGroup.IsRequired ? (long?)selOrientationGroup.Value.Single().Numeric : null,
                    coachingSchool = txtCoachingSchool.Value,
                    privateLessons = rdgPrivateLessons.HasValue ? (bool?)(rdgPrivateLessons.Value.Numeric == 0) : null,
                    userId = 1,
                };

                if (trs.Insert() == 1)
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
