using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using Form2;
using Form2.Form.Content;
using Form2.Form.Content.Items;
using Form2.Form.Content.Items.Input;
using Form2.Form.Content.Items.Input.Selectors;
using Form2.Form.Enums;
using Form2.Form.Interfaces;
using Form2.Form.Selectables;
using Form2.Form.Visitors;

using NLog;

namespace Form2WebApp.UserControls
{
    [SuppressMessage("Style", "IDE0017:Simplify object initialization", Justification = "<Pending>")]
    [SuppressMessage("Code Quality", "IDE0052:Remove unread private members", Justification = "<Pending>")]

    public partial class CtrlFormMinetta : UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string formSessionKey = string.Format("{0}_{1}_{2}", Page.GetType().Name, GetType().Name, typeof(Form).Name);

            Form form = this.Page.SessionGet(formSessionKey, () => new Form());

            if (form == null)
                return;

            form.SetPage(Page);

            new FormCommander(form).HandleRequest(Request);

            FormRenderer formRenderer = new FormRenderer(form);

            ltrForm.Text = formRenderer.Html;

            int i = 0;

            foreach (var s in formRenderer.Scripts)
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), string.Format("FormScript{0}", i++), s, false);
        }

        private class Form : FormModel
        {
            private static readonly Logger log = LogManager.GetCurrentClassLogger();

            #region Fields

            private Page page;

            #endregion

            public void SetPage(Page page)
            {
                this.page = page;
            }

            public Form()
            {
                Initialize();
            }

            #region CreateForm()

            protected override void CreateForm()
            {
                OpenGroup("Container");


                #region Defaults

                OrderElements = OrderElements.LabelMarkInput;

                Required = true;

                RequiredMessage = "Το πεδίο είναι υποχρεωτικό";

                RequiredInLabel = false;

                OptionalMark = "(προαιρετικό)";

                #endregion


                #region Title

                AddItem(new FormTitle("Title") { Content = "Εκδήλωση Ενδιαφέροντος για εταιρικό Site", Hidden = true });

                #endregion


                OpenGroup("");


                #region PersonInCharge

                OpenGroup("PersonInCharge");


                AddItem(new FormTitle("Title") { Content = "Στοιχεία υπεύθυνου έργου" });


                OpenGroup("Name");

                AddItem(new FormTextBox("First") { Label = "Όνομα Υπευθύνου" });

                AddItem(new FormTextBox("Last") { Label = "Επώνυμο Υπευθύνου" });

                CloseGroup("Name");


                OpenGroup("Contact");

                AddItem(new FormTextBox("Phone") { Label = "Τηλέφωνο Επικοινωνίας", Icon = FormIcon.Phone });

                CloseGroup("Contact");


                CloseGroup("PersonInCharge");

                #endregion PersonInCharge


                #region Agency

                OpenGroup("Agency");


                AddItem(new FormTitle("Title") { Content = "Στοιχεία πρακτορείου" });


                OpenGroup("Info");

                AddItem(new FormTextBox("Code") { Label = "Κωδικός πρακτορείου" });

                AddItem(new FormTextBox("Name") { Label = "Επωνυμία Ασφαλιστικού Πρακτορείου" });

                CloseGroup("Info");


                OpenGroup("Tax");

                AddItem(new FormTextBox("TIN") { Label = "ΑΦΜ" });

                AddItem(new FormTextBox("Office") { Label = "ΔΟΥ" });

                CloseGroup("Tax");


                OpenGroup("Location1");

                AddItem(new FormTextBox("Address") { Label = "Διεύθυνση" });

                AddItem(new FormTextBox("ZipCode") { Label = "T.K." });

                CloseGroup("Location1");


                OpenGroup("Location2");

                AddItem(new FormTextBox("Area") { Label = "Περιοχή" });

                AddItem(new FormSelect("Prefecture", false)
                {
                    Label = "Νομός",

                    Header = new FormOption("Επιλέξτε..."),

                    Content = new FormOption[] {
                        new FormOption("Αιτωλοακαρνανίας"),
                        new FormOption("Αργολίδας"),
                        new FormOption("Αρκαδίας"),
                        new FormOption("Άρτας"),
                        new FormOption("Αττικής"),
                        new FormOption("Αχαϊας"),
                        new FormOption("Βοιωτίας"),
                        new FormOption("Γρεβενών"),
                        new FormOption("Δράμας"),
                        new FormOption("Δωδεκανήσου"),
                        new FormOption("Έβρου"),
                        new FormOption("Εύβοιας"),
                        new FormOption("Ευρυτανίας"),
                        new FormOption("Ζακύνθου"),
                        new FormOption("Ηλείας"),
                        new FormOption("Ημαθίας"),
                        new FormOption("Ηρακλείου"),
                        new FormOption("Θεσπρωτίας"),
                        new FormOption("Θεσσαλονίκης"),
                        new FormOption("Ιωαννίνων"),
                        new FormOption("Καβάλας"),
                        new FormOption("Καρδίτσας"),
                        new FormOption("Καστοριάς"),
                        new FormOption("Κέρκυρας"),
                        new FormOption("Κεφαλληνίας"),
                        new FormOption("Κιλκίς"),
                        new FormOption("Κοζάνης"),
                        new FormOption("Κορινθία"),
                        new FormOption("Κυκλάδες"),
                        new FormOption("Λακωνίας"),
                        new FormOption("Λαρίσης"),
                        new FormOption("Λασιθίου"),
                        new FormOption("Λέσβου"),
                        new FormOption("Λευκάδας"),
                        new FormOption("Μαγνησίας"),
                        new FormOption("Μεσσηνίας"),
                        new FormOption("Ξάνθης"),
                        new FormOption("Πέλλας"),
                        new FormOption("Πιερίας"),
                        new FormOption("Πρεβέζης"),
                        new FormOption("Ρεθύμνου"),
                        new FormOption("Ροδόπης"),
                        new FormOption("Σάμου"),
                        new FormOption("Σερρών"),
                        new FormOption("Τρικάλων"),
                        new FormOption("Φθιώτιδας"),
                        new FormOption("Φλώρινα"),
                        new FormOption("Φωκίδας"),
                        new FormOption("Χαλκιδικής"),
                        new FormOption("Χανίων"),
                        new FormOption("Χίου"),
                    }
                });

                CloseGroup("Location2");


                OpenGroup("Contact1");

                AddItem(new FormTextBox("Landline") { Label = "Τηλέφωνο επικοινωνίας σταθερό", Icon = FormIcon.Phone });

                AddItem(new FormTextBox("Mobile") { Label = "Τηλέφωνο επικοινωνίας κινητό", Icon = FormIcon.Mobile });

                CloseGroup("Contact1");


                OpenGroup("Contact2");

                AddItem(new FormTextBox("Fax") { Label = "FAX", Required = false, OptionalInLabel = true, Icon = FormIcon.Fax });

                AddItem(new FormTextBox("Email") { Label = "E-mail", Icon = FormIcon.Envelope });

                CloseGroup("Contact2");


                CloseGroup("Agency");

                #endregion Agency


                #region Social

                OpenGroup("Social");


                Required = false;

                OptionalInLabel = true;


                AddItem(new FormTitle("Title") { Content = "Λογαριασμοί Social Media (εφόσον υπάρχουν)" });


                OpenGroup("URL1");

                AddItem(new FormTextBox("Facebook") { Label = "Facebook Url", Icon = FormIcon.Facebook });

                AddItem(new FormTextBox("Instagram") { Label = "Instagram Url", Icon = FormIcon.Instagram });

                CloseGroup("URL1");


                OpenGroup("URL2");

                AddItem(new FormTextBox("LinkedIn") { Label = "LinkedIn Url", Icon = FormIcon.LinkedIn });

                AddItem(new FormTextBox("YouTube") { Label = "YouTube Url", Icon = FormIcon.YouTube });

                CloseGroup("URL2");


                CloseGroup("Social");

                #endregion Social


                CloseGroup("");


                #region Submit

                AddItem(new FormSubmit("Submit") { Content = "ΥΠΟΒΟΛΗ", Submit = true });

                #endregion


                CloseGroup("Container");
            }

            #endregion


            #region PerformAction()

            protected override void PerformAction()
            {
                //var emailVisitor = new MinettaEmailVisitor(FormGroup, "Ναι", "Όχι", true, false);

                //page.Response.Write(emailVisitor.Html);

                log.Info(new MinettaLogVisitor(FormGroup, "Ναι", "Όχι", true, false).Text);

                Initialize();
            }

            #endregion
        }

        public class MinettaEmailVisitor : FormEmailVisitor
        {
            public MinettaEmailVisitor(FormGroup formGroup, string yes, string no, bool showMarks, bool showRequired)
                : base(formGroup, yes, no, showMarks, showRequired)
            {
            }

            protected override string Mark(IRequired formItem)
            {
                if (showMarks && formItem.IsRequired && showRequired)
                    return formItem.RequiredMark;
                else if (showMarks && !formItem.IsRequired && !showRequired)
                    return string.Format(" {0}", formItem.OptionalMark);
                else
                    return "";
            }
        }

        public class MinettaLogVisitor : FormLogVisitor
        {
            public MinettaLogVisitor(FormGroup formGroup, string yes, string no, bool showMarks, bool showRequired)
                : base(formGroup, yes, no, showMarks, showRequired)
            {
            }

            protected override string Mark(IRequired formItem)
            {
                if (showMarks && formItem.IsRequired && showRequired)
                    return formItem.RequiredMark;
                else if (showMarks && !formItem.IsRequired && !showRequired)
                    return string.Format(" {0}", formItem.OptionalMark);
                else
                    return "";
            }
        }
    }
}
