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

//using NLog;

namespace Form2WebApp.UserControls
{
    public partial class CtrlFormStudent : UserControl
    {
        //private static readonly Logger log = LogManager.GetCurrentClassLogger();

        protected void Page_Load(object sender, EventArgs e)
        {
            ltrForm.Text = new Form(Page).GetText(IsPostBack, Request.Form, Session);
        }

        private class Form : Form2Base
        {
            #region Fields

            private readonly Page page;

            private FormDatePicker dtpDateOfBirth;
            private FormSelect selCity;
            private FormSelect selArea;

            private FormSelect selMunicipality;
            private FormTextBox txtMunicipality;

            private FormTextBox txtEmail;

            private FormSelect selEducationalStage;
            private FormSelect selEducationalGrade;

            private FormSelect selOrientationGroup;

            private FormTextBox txtCoachingSchool;
            private FormRadioGroup rdgPrivateLessons;

            #endregion


            #region Constructors

            public Form(Page page)
            {
                this.page = page;
            }

            #endregion


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


            protected override void CreateForm()
            {
                OpenGroup("Container");

                #region Defaults

                ElementOrder = ElementOrder.LabelMarkInput;

                IsRequired = true;

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

                    Validator = (f) =>
                    {
                        if (f.Value > DateTime.Now)
                            return resDateInvalid;

                        return "";
                    }
                });

                #endregion


                #region Population

                AddItem(selCity = new FormSelect("Population", false)
                {
                    Label = resCity,

                    Header = new FormOption(resChoose),

                    Content = tblPopulation.ListAll().Select(p => new FormOption(p.id, p.descr))
                });

                #endregion


                #region City

                AddItem(selArea = new FormSelect("City", false)
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

                    IsHidden = true,
                });


                AddItem(txtMunicipality = new FormTextBox("MunicipalityTextBox")
                {
                    Label = resMunicipality,

                    IsHidden = true,
                });

                #endregion


                #region Email

                AddItem(txtEmail = new FormTextBox("Email")
                {
                    Label = resEmail.ToLower(),

                    Validator = (f) =>
                    {
                        if (!new Regex(@"^[a-zA-Z0-9._-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]*$").IsMatch(f.Value.Trim()))
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

                    IsHidden = true,

                    IsPostBack = true,
                });

                #endregion


                #region OrientationGroup

                AddItem(selOrientationGroup = new FormSelect("OrientationGroup", false)
                {
                    Label = resOrientationGroup,

                    Header = new FormOption(resChoose),

                    IsHidden = true,

                    Content = tblOrientationGroup.ListAll().Select(o => new FormOption(o.id, o.name))
                });

                #endregion


                #region CoachingSchool

                AddItem(txtCoachingSchool = new FormTextBox("CoachingSchool")
                {
                    Label = resCoachingSchool,

                    IsRequired = false,
                });

                #endregion


                #region PrivateLessons

                AddItem(rdgPrivateLessons = new FormRadioGroup("PrivateLessons")
                {
                    Label = resPrivateLessons,

                    IsRequired = false,

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

                CloseGroup();

                #region Rules

                AddRule((isPostBack, eventTarget, eventArgument) =>
                {
                    if (!isPostBack)
                        return;

                    if (eventTarget != selArea.BaseId)
                        return;

                    selMunicipality.IsHidden = true;
                    txtMunicipality.IsHidden = true;

                    if (!selArea.Value.Any())
                        return;

                    long cityId = Convert.ToInt64(selArea.Value.Single().Value);

                    var municipalities = tblMunicipality.ListForcityId(cityId).OrderBy(m => m.name).ToList();

                    if (municipalities.Any())
                    {
                        selMunicipality.Content = municipalities.Select(m => new FormOption(m.id, m.name));
                        selMunicipality.IsHidden = false;
                    }
                    else
                    {
                        txtMunicipality.Content = "";
                        txtMunicipality.IsHidden = false;
                    }
                });

                AddRule((isPostBack, eventTarget, eventArgument) =>
                {
                    if (!isPostBack)
                        return;

                    if (eventTarget != selEducationalStage.BaseId)
                        return;

                    selEducationalGrade.IsHidden = true;
                    selOrientationGroup.IsHidden = true;

                    if (!selEducationalStage.Value.Any())
                        return;

                    long stageId = Convert.ToInt64(selEducationalStage.Value.Single().Value);

                    var grades = tblEducationalGrade.ListForstageId(stageId).ToList();

                    if (!grades.Any())
                        return;

                    selEducationalGrade.Content = grades.Select(g => new FormOption(g.id, g.name));
                    selEducationalGrade.IsHidden = false;
                });

                AddRule((isPostBack, eventTarget, eventArgument) =>
                {
                    if (!isPostBack)
                        return;

                    if (eventTarget != selEducationalGrade.BaseId)
                        return;

                    selOrientationGroup.IsHidden = true;

                    if (selEducationalStage.IsHidden ?? false)
                        return;

                    if (!selEducationalStage.Value.Any())
                        return;

                    if (selEducationalStage.Value.Single().Value != "3")
                        return;

                    if (selEducationalGrade.IsHidden ?? false)
                        return;

                    if (!selEducationalGrade.Value.Any())
                        return;

                    if (selEducationalGrade.Value.Single().Value != "12")
                        return;

                    foreach (var o in selOrientationGroup.Content)
                        o.IsSelected = false;

                    selOrientationGroup.IsHidden = false;
                });

                #endregion
            }

            #region PerformAction()

            protected override void PerformAction()
            {
                //log.Info(new FormLogVisitor(FormGroup, resYes, resNo).Text);

                //tblRegisterStudent trs = new tblRegisterStudent();
                //trs.dateOfBirth = GetItem<FormDatePicker>("DateOfBirth").Value.Value;
                //trs.populationId = GetItem<FormSelect>("Population").Value.Single().Numeric;
                //trs.cityId = GetItem<FormSelect>("City").Value.Single().Numeric;
                //if (!(GetItem<FormSelect>("MunicipalitySelect").IsHidden ?? false))
                //    trs.municipality = GetItem<FormSelect>("MunicipalitySelect").Value.Single().Text;
                //else if (!(GetItem<FormTextBox>("MunicipalityTextBox").IsHidden ?? false))
                //    trs.municipality = GetItem<FormTextBox>("MunicipalityTextBox").Value;
                //trs.email = GetItem<FormTextBox>("Email").Value;
                //trs.educationalGradeId = GetItem<FormSelect>("EducationalStage").Value.Single().Numeric;
                //if (!(GetItem<FormSelect>("OrientationGroup").IsHidden ?? false))
                //    trs.orientationGroupId = GetItem<FormSelect>("OrientationGroup").Value.Single().Numeric;
                //trs.coachingSchool = GetItem<FormTextBox>("CoachingSchool").Value;
                //if (GetItem<FormRadioGroup>("PrivateLessons").Value != null)
                //    trs.privateLessons = GetItem<FormRadioGroup>("PrivateLessons").Value.Value == "0";
                //trs.userId = 0;

                //if (trs.Insert() == 1)
                //{
                //    string script = "bootbox.alert({message: 'Registration Success'});";
                //    ScriptManager.RegisterStartupScript(page, page.GetType(), "RegistrationSuccess", script, true);
                //}
                //else
                //{
                //    string script = "bootbox.alert({message: 'Registration Failure'});";
                //    ScriptManager.RegisterStartupScript(page, page.GetType(), "RegistrationFailure", script, true);
                //}
            }

            #endregion
        }
    }
}