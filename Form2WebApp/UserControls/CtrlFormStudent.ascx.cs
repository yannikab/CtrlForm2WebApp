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

//using rdc;

namespace Form2WebApp.UserControls
{
    public partial class CtrlFormStudent : UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            ltrForm.Text = new Form(Page).GetText(IsPostBack, Request.Form, Session);

            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "DatePicker", "$(document).ready(function () { $('.date-picker').datepicker(); });", true);
        }

        private class Form : Form2Base
        {
            #region Fields

            private readonly Page page;

            private FormDatePicker txtDateOfBirth;
            private FormSelect selCity;
            private FormSelect selArea;

            private FormSelect selMunicipalityAthens;
            private FormTextBox txtMunicipalityOther;

            private FormTextBox txtEmail;

            private FormSelect selSchool;
            private FormSelect selElementaryGrade;
            private FormSelect selMiddleSchoolGrade;
            private FormSelect selHighSchoolGrade;

            private FormSelect selOrientation;

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

            private readonly string resChoose = "Επιλέξτε...";
            private readonly string resFieldRequired = "Το πεδίο είναι υποχρεωτικό";
            private readonly string resEmailInvalid = "Το email δεν είναι έγκυρο";
            private readonly string resDateOfBirthInvalid = "Η ημερομηνία γέννησης δεν μπορεί να είναι στο μέλλον"; 
            private readonly string resYes = "Ναι";
            private readonly string resNo = "Όχι";

            private readonly string resDateOfBirth = "Ημερομηνία γέννησης";
            private readonly string resCity = "Πόλη";
            private readonly string resAreaOfResidence = "Περιοχή κατοικίας";
            private readonly string resMunicipality = "Δήμος";
            private readonly string resEmail = "Email";

            private readonly string resSchool = "Σχολείο";
            private readonly string resElementarySchool = "Δημοτικό";
            private readonly string resMiddleSchool = "Γυμνάσιο";
            private readonly string resHighSchool = "Λύκειο";
            private readonly string resGrade = "Τάξη";

            private readonly string resOrientationGroup = "Ομάδα προσανατολισμού";

            private readonly string resCoachingSchool = "Φροντιστήριο";
            private readonly string resPrivateLessons = "Ιδιαίτερα μαθήματα";

            private readonly string resSend = "Αποστολή";

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

                AddItem(new FormTitle("Students")
                {
                    Content = "Μαθητές",
                });

                #endregion


                #region DateOfBirth

                AddItem(txtDateOfBirth = new FormDatePicker("DateOfBirth", "dd/mm/yyyy")
                {
                    Label = resDateOfBirth,

                    IsRequired = true,

                    PlaceHolder = resDateOfBirth,

                    Validator = (f) =>
                    {
                        if (f.Value == null)
                            return "";

                        if (f.Value > DateTime.Now)
                            return resDateOfBirthInvalid;

                        return "";
                    }
                });

                #endregion


                #region City

                AddItem(selCity = new FormSelect("City", false)
                {
                    Label = resCity,

                    Header = new FormOption(resChoose),

                    IsPostBack = false,

                    Content = new FormOption[]
                    {
                    new FormOption(0, "Χωριό (<3000 κατοίκων)"),
                    new FormOption(1, "Κωμόπολη (3.000 - 10.000 κατ.)"),
                    new FormOption(2, "Πόλη (10.000 - 100.000 κατ.)"),
                    new FormOption(3, "Μεγάλη Πόλη (100.000 - 1.000.000 κατ.)"),
                    new FormOption(4, "Θεσσαλονίκη (1.000.000 - 3.000.000 κατ.)"),
                    new FormOption(5, "Αθήνα"),
                    new FormOption(6, "Πάνω από 1.000.000 κάτοικοι"),
                    },
                });

                #endregion


                #region Area

                AddItem(selArea = new FormSelect("Area", false)
                {
                    Label = resAreaOfResidence,

                    Header = new FormOption(resChoose),

                    IsPostBack = true,

                    Content = new FormOption[] {
                    new FormOption(60, "(ΚΥΠΡΟΣ) ΑΜΜΟΧΩΣΤΟΣ"),
                    new FormOption(56, "(ΚΥΠΡΟΣ) ΛΑΡΝΑΚΑ"),
                    new FormOption(58, "(ΚΥΠΡΟΣ) ΛΕΜΕΣΟΣ"),
                    new FormOption(57, "(ΚΥΠΡΟΣ) ΛΕΥΚΩΣΙΑ"),
                    new FormOption(59, "(ΚΥΠΡΟΣ) ΠΑΦΟΣ"),
                    new FormOption(13, "ΑΓ.ΝΙΚΟΛΑΟΣ"),
                    new FormOption(1, "ΑΘΗΝΑ"),
                    new FormOption(24, "ΑΛΕΞΑΝΔΡΟΥΠΟΛΗ"),
                    new FormOption(53, "ΑΜΦΙΣΣΑ"),
                    new FormOption(5, "ΑΡΓΟΣ"),
                    new FormOption(37, "ΑΡΓΟΣΤΟΛΙ"),
                    new FormOption(6, "ΑΡΤΑ"),
                    new FormOption(29, "ΒΕΡΟΙΑ"),
                    new FormOption(44, "ΒΟΛΟΣ"),
                    new FormOption(20, "ΓΡΕΒΕΝΑ"),
                    new FormOption(21, "ΔΡΑΜΑ"),
                    new FormOption(55, "ΕΔΕΣΣΑ"),
                    new FormOption(40, "ΕΡΜΟΥΠΟΛΗ"),
                    new FormOption(27, "ΖΑΚΥΝΘΟΣ"),
                    new FormOption(30, "ΗΓΟΥΜΕΝΙΤΣΑ"),
                    new FormOption(14, "ΗΡΑΚΛΕΙΟ"),
                    new FormOption(2, "ΘΕΣΣΑΛΟΝΙΚΗ"),
                    new FormOption(18, "ΘΗΒΑ"),
                    new FormOption(31, "ΙΩΑΝΝΙΝΑ"),
                    new FormOption(33, "ΚΑΒΑΛΑ"),
                    new FormOption(12, "ΚΑΛΑΜΑΤΑ"),
                    new FormOption(34, "ΚΑΡΔΙΤΣΑ"),
                    new FormOption(26, "ΚΑΡΠΕΝΗΣΙ"),
                    new FormOption(35, "ΚΑΣΤΟΡΙΑ"),
                    new FormOption(46, "ΚΑΤΕΡΙΝΗ"),
                    new FormOption(36, "ΚΕΡΚΥΡΑ"),
                    new FormOption(38, "ΚΙΛΚΙΣ"),
                    new FormOption(39, "ΚΟΖΑΝΗ"),
                    new FormOption(48, "ΚΟΜΟΤΗΝΗ"),
                    new FormOption(10, "ΚΟΡΙΝΘΟΣ"),
                    new FormOption(19, "ΛΑΜΙΑ"),
                    new FormOption(41, "ΛΑΡΙΣΑ"),
                    new FormOption(43, "ΛΕΥΚΑΔΑ"),
                    new FormOption(17, "ΛΙΒΑΔΕΙΑ"),
                    new FormOption(3, "ΜΕΣΟΛΟΓΓΙ"),
                    new FormOption(42, "ΜΥΤΙΛΗΝΗ"),
                    new FormOption(61, "ΝΕΑ ΜΟΥΔΑΝΙΑ"),
                    new FormOption(45, "ΞΑΝΘΗ"),
                    new FormOption(7, "ΠΑΤΡΑ"),
                    new FormOption(47, "ΠΡΕΒΕΖΑ"),
                    new FormOption(28, "ΠΥΡΓΟΣ"),
                    new FormOption(15, "ΡΕΘΥΜΝΟ"),
                    new FormOption(22, "ΡΟΔΟΣ"),
                    new FormOption(49, "ΣΑΜΟΣ"),
                    new FormOption(50, "ΣΕΡΡΕΣ"),
                    new FormOption(11, "ΣΠΑΡΤΗ"),
                    new FormOption(51, "ΤΡΙΚΑΛΑ"),
                    new FormOption(9, "ΤΡΙΠΟΛΗ"),
                    new FormOption(52, "ΦΛΩΡΙΝΑ"),
                    new FormOption(25, "ΧΑΛΚΙΔΑ"),
                    new FormOption(16, "ΧΑΝΙΑ"),
                    new FormOption(54, "ΧΙΟΣ"),
                }
                });

                #endregion


                #region Municipality

                AddItem(selMunicipalityAthens = new FormSelect("MunicipalityAthens", false)
                {
                    Label = resMunicipality,

                    Header = new FormOption(resChoose),

                    IsHidden = true,

                    Content = new FormOption[] {
                    new FormOption("Δήμος Αγίας Βαρβάρας"),
                    new FormOption("Δήμος Αγίας Παρασκευής"),
                    new FormOption("Δήμος Αγίου Δημητρίου"),
                    new FormOption("Δήμος Αγίου Ιωάννου Ρέντη"),
                    new FormOption("Δήμος Αγίου Στεφάνου"),
                    new FormOption("Δήμος Αγίων Αναργύρων"),
                    new FormOption("Δήμος Αθηναίων"),
                    new FormOption("Δήμος Αιγάλεω"),
                    new FormOption("Δήμος Αίγινας"),
                    new FormOption("Δήμος Αλίμου"),
                    new FormOption("Δήμος Αμαρουσίου"),
                    new FormOption("Δήμος Αμπελακίων"),
                    new FormOption("Δήμος Αναβύσσου"),
                    new FormOption("Δήμος Ανοίξεως"),
                    new FormOption("Δήμος Άνω Λιοσίων"),
                    new FormOption("Δήμος Αργυρουπόλεως"),
                    new FormOption("Δήμος Αρτέμιδος (τ. Λούτσας)"),
                    new FormOption("Δήμος Ασπροπύργου"),
                    new FormOption("Δήμος Αυλώνος"),
                    new FormOption("Δήμος Αχαρνών"),
                    new FormOption("Δήμος Βάρης"),
                    new FormOption("Δήμος Βιλίων"),
                    new FormOption("Δήμος Βούλας"),
                    new FormOption("Δήμος Βουλιαγμένης"),
                    new FormOption("Δήμος Βριλησσίων"),
                    new FormOption("Δήμος Βύρωνος"),
                    new FormOption("Δήμος Γαλατσίου"),
                    new FormOption("Δήμος Γέρακα"),
                    new FormOption("Δήμος Γλυκών Νερών"),
                    new FormOption("Δήμος Γλυφάδας"),
                    new FormOption("Δήμος Δάφνης"),
                    new FormOption("Δήμος Διονύσου"),
                    new FormOption("Δήμος Δραπετσώνας"),
                    new FormOption("Δήμος Εκάλης"),
                    new FormOption("Δήμος Ελευσίνας"),
                    new FormOption("Δήμος Ελληνικού"),
                    new FormOption("Δήμος Ερυθρών"),
                    new FormOption("Δήμος Ζαφυρίου"),
                    new FormOption("Δήμος Ζωγράφου"),
                    new FormOption("Δήμος Ηλιουπόλεως"),
                    new FormOption("Δήμος Ηρακλείου"),
                    new FormOption("Δήμος Θρακομακεδόνων"),
                    new FormOption("Δήμος Ιλίου (τ.Νέων Λιοσίων)"),
                    new FormOption("Δήμος Καισαριανής"),
                    new FormOption("Δήμος Καλάμου"),
                    new FormOption("Δήμος Καλλιθέας"),
                    new FormOption("Δήμος Καλυβίων Θορικού"),
                    new FormOption("Δήμος Καματερού"),
                    new FormOption("Δήμος Κερατέας"),
                    new FormOption("Δήμος Κερατσινίου"),
                    new FormOption("Δήμος Κηφισίας"),
                    new FormOption("Δήμος Κορυδαλλού"),
                    new FormOption("Δήμος Κρωπίας"),
                    new FormOption("Δήμος Κυθήρων"),
                    new FormOption("Δήμος Λαυρεωτικής"),
                    new FormOption("Δήμος Λυκόβρυσης"),
                    new FormOption("Δήμος Μαγούλας"),
                    new FormOption("Δήμος Μάνδρας"),
                    new FormOption("Δήμος Μαραθώνα"),
                    new FormOption("Δήμος Μαρκόπουλου Μεσογαίας"),
                    new FormOption("Δήμος Μεγαρέων"),
                    new FormOption("Δήμος Μεθάνων"),
                    new FormOption("Δήμος Μελισσίων"),
                    new FormOption("Δήμος Μεταμόρφωσης"),
                    new FormOption("Δήμος Μοσχάτου"),
                    new FormOption("Δήμος Νέα Χαλκηδόνος"),
                    new FormOption("Δήμος Νέας Ερυθραίας"),
                    new FormOption("Δήμος Νέας Ιωνίας"),
                    new FormOption("Δήμος Νέας Μάκρης"),
                    new FormOption("Δήμος Νέας Πεντέλης"),
                    new FormOption("Δήμος Νέας Περάμου"),
                    new FormOption("Δήμος Νέας Σμύρνης"),
                    new FormOption("Δήμος Νέας Φιλαδέλφειας"),
                    new FormOption("Δήμος Νέου Ψυχικού"),
                    new FormOption("Δήμος Νικαίας"),
                    new FormOption("Δήμος Παιανίας"),
                    new FormOption("Δήμος Παλαιού Φαλήρου"),
                    new FormOption("Δήμος Παλλήνης"),
                    new FormOption("Δήμος Παπάγου"),
                    new FormOption("Δήμος Πειραιώς"),
                    new FormOption("Δήμος Πεντέλης"),
                    new FormOption("Δήμος Περάματος"),
                    new FormOption("Δήμος Περιστερίου"),
                    new FormOption("Δήμος Πετρουπόλεως"),
                    new FormOption("Δήμος Πεύκης"),
                    new FormOption("Δήμος Πόρου"),
                    new FormOption("Δήμος Ραφήνας"),
                    new FormOption("Δήμος Σαλαμίνας"),
                    new FormOption("Δήμος Σπάτων"),
                    new FormOption("Δήμος Σπετσών"),
                    new FormOption("Δήμος Ταύρου"),
                    new FormOption("Δήμος Τροιζήνας"),
                    new FormOption("Δήμος Ύδρας"),
                    new FormOption("Δήμος Υμηττού"),
                    new FormOption("Δήμος Φιλοθέης"),
                    new FormOption("Δήμος Φυλής"),
                    new FormOption("Δήμος Χαϊδαρίου"),
                    new FormOption("Δήμος Χαλανδρίου"),
                    new FormOption("Δήμος Χολαργού"),
                    new FormOption("Δήμος Ψυχικού"),
                    new FormOption("Δήμος Ωρωπίων"),
                    new FormOption("Κοινότητα Αγίου Κωνσταντίνου"),
                    new FormOption("Κοινότητα Αγκιστρίου"),
                    new FormOption("Κοινότητα Ανθούσης"),
                    new FormOption("Κοινότητα Αντικυθήρων"),
                    new FormOption("Κοινότητα Αφιδνών"),
                    new FormOption("Κοινότητα Βαρνάβα"),
                    new FormOption("Κοινότητα Γραμματικού"),
                    new FormOption("Κοινότητα Δροσιάς"),
                    new FormOption("Κοινότητα Καπανδριτίου"),
                    new FormOption("Κοινότητα Κουβαρά"),
                    new FormOption("Κοινότητα Κρυονερίου"),
                    new FormOption("Κοινότητα Μαλακάσας"),
                    new FormOption("Κοινότητα Μαρκόπουλου Ωρωπου"),
                    new FormOption("Κοινότητα Νέων Παλατίων"),
                    new FormOption("Κοινότητα Οινόης"),
                    new FormOption("Κοινότητα Παλαιάς Φώκαιας"),
                    new FormOption("Κοινότητα Πικερμίου"),
                    new FormOption("Κοινότητα Πολυδενδρίου"),
                    new FormOption("Κοινότητα Ροδοπόλεως"),
                    new FormOption("Κοινότητα Σαρωνίδος"),
                    new FormOption("Κοινότητα Σκάλας Ωρωπού"),
                    new FormOption("Κοινότητα Σταμάτας"),
                    new FormOption("Κοινότητα Συκαμίνου"),
                }
                });


                AddItem(txtMunicipalityOther = new FormTextBox("MunicipalityOther")
                {
                    Label = resMunicipality,

                    IsHidden = true
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


                #region School

                AddItem(selSchool = new FormSelect("School", false)
                {
                    Label = resSchool,

                    Header = new FormOption(resChoose),

                    IsPostBack = true,

                    Content = new FormOption[]
                    {
                    new FormOption(0, resElementarySchool),
                    new FormOption(1, resMiddleSchool),
                    new FormOption(2, resHighSchool),
                    },
                });

                #endregion


                #region ElementaryGrade

                AddItem(selElementaryGrade = new FormSelect("ElementaryGrade", false)
                {
                    Label = resGrade,

                    Header = new FormOption(resChoose),

                    Content = new FormOption[]
                    {
                    new FormOption(0, "Α' ΔΗΜΟΤΙΚΟΥ"),
                    new FormOption(1, "Β' ΔΗΜΟΤΙΚΟΥ"),
                    new FormOption(2, "Γ' ΔΗΜΟΤΙΚΟΥ"),
                    new FormOption(3, "Δ' ΔΗΜΟΤΙΚΟΥ"),
                    new FormOption(4, "Ε' ΔΗΜΟΤΙΚΟΥ"),
                    new FormOption(5, "ΣΤ' ΔΗΜΟΤΙΚΟΥ"),
                    }
                });

                #endregion


                #region MiddleSchoolGrade

                AddItem(selMiddleSchoolGrade = new FormSelect("MiddleSchoolGrade", false)
                {
                    Label = resGrade,

                    Header = new FormOption(resChoose),

                    Content = new FormOption[]
                    {
                    new FormOption(0, "Α' ΓΥΜΝΑΣΙΟΥ"),
                    new FormOption(1, "Β' ΓΥΜΝΑΣΙΟΥ"),
                    new FormOption(2, "Γ' ΓΥΜΝΑΣΙΟΥ"),
                    }
                });

                #endregion


                #region HighSchoolGrade

                AddItem(selHighSchoolGrade = new FormSelect("HighSchoolGrade", false)
                {
                    Label = resGrade,

                    Header = new FormOption(resChoose),

                    IsPostBack = true,

                    Content = new FormOption[]
                    {
                    new FormOption(0, "Α' ΛΥΚΕΙΟΥ"),
                    new FormOption(1, "Β' ΛΥΚΕΙΟΥ"),
                    new FormOption(2, "Γ' ΛΥΚΕΙΟΥ"),
                    }
                });

                #endregion


                #region Orientation

                AddItem(selOrientation = new FormSelect("Orientation", false)
                {
                    Label = resOrientationGroup,

                    Header = new FormOption(resChoose),

                    Content = new FormOption[]
                    {
                    new FormOption(0, "Ανθρωπιστικών Σπουδών"),
                    new FormOption(1, "Θετικών Σπουδών & Σπουδών Υγείας"),
                    new FormOption(2, "Σπουδών Οικονομίας και Πληροφορικής"),
                    new FormOption(3, "Δεν έχω αποφασίσει ακόμα"),
                    },
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

                AddRule(() =>
                {
                    selMunicipalityAthens.IsHidden = true;
                    txtMunicipalityOther.IsHidden = true;

                    if (!selArea.Value.Any())
                        return;

                    if (selArea.Value.Single().Value == "1") // Αθήνα
                        selMunicipalityAthens.IsHidden = false;
                    else
                        txtMunicipalityOther.IsHidden = false;
                });

                AddRule(() =>
                {
                    selElementaryGrade.IsHidden = true;
                    selMiddleSchoolGrade.IsHidden = true;
                    selHighSchoolGrade.IsHidden = true;

                    if (!selSchool.Value.Any())
                        return;

                    if (selSchool.Value.Single().Value == "0")
                        selElementaryGrade.IsHidden = false;
                    else if (selSchool.Value.Single().Value == "1")
                        selMiddleSchoolGrade.IsHidden = false;
                    else if (selSchool.Value.Single().Value == "2")
                        selHighSchoolGrade.IsHidden = false;
                });

                AddRule(() =>
                {
                    selOrientation.IsHidden = true;

                    if (selHighSchoolGrade.IsHidden ?? false)
                        return;

                    if (!selHighSchoolGrade.Value.Any() || selHighSchoolGrade.Value.Single().Value != "2") // Γ' Λυκείου
                        return;

                    selOrientation.IsHidden = false;
                });

                #endregion
            }

            #region PerformAction()

            protected override void PerformAction()
            {
                var emailVisitor = new FormEmailVisitor(formGroup, "Ναι", "Όχι");

                page.Response.Write(emailVisitor.Subject);
                page.Response.Write("<br /><br />");
                page.Response.Write(emailVisitor.Body);
                page.Response.Write("<br /><br />");

                //try
                //{
                //    //tblGates tg = new tblGates(Common.GetGate());

                //    //string emailBody = "";

                //    //new Common().SendAppEmail("Εκδήλωση ενδιαφέροντος", emailBody, tg.sendermail, "y.kabilafkas@rdc.gr", "d.athanassiadis@rdc.gr", "");
                //    //new Common().SendAppEmail("Εκδήλωση ενδιαφέροντος", emailBody, tg.sendermail, tg.ccmail, ConfigurationManager.AppSettings["CCMailAddress"], "");

                //    string script = "bootbox.alert({message: 'Το email σας απεστάλη με επιτυχία.'});";
                //    ScriptManager.RegisterStartupScript(page, page.GetType(), "MailSentSuccess", script, true);
                //}
                //catch (Exception)
                //{
                //    string script = "bootbox.alert({message: 'Παρουσιάστηκε κάποιο σφάλμα κατά την αποστολή του email σας.'});";
                //    ScriptManager.RegisterStartupScript(page, page.GetType(), "MailSentError", script, true);
                //}
            }

            #endregion
        }
    }
}
