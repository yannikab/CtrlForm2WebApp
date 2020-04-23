using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserControls.CtrlForm2.FormElements.FormItems.FormItemsInput
{
    public class FormTextBox : FormItemInput
    {
        #region Fields

        private string text;

        private string initialText;

        private string placeHolder;

        private FormIcon icon;

        #endregion


        #region Properties

        public string Text
        {
            get { return text; }
            set { text = value; }
        }

        public string InitialText
        {
            get { return initialText; }
            set { initialText = value; }
        }

        public string PlaceHolder
        {
            get { return placeHolder; }
            set { placeHolder = value; }
        }

        public FormIcon Icon
        {
            get { return icon; }
            set { icon = value; }
        }

        #endregion


        #region Constructors

        public FormTextBox(string baseId, string formId)
            : base(baseId, formId)
        {
            text = "";
            initialText = "";
            placeHolder = "";
            icon = FormIcon.NotSet;
        }

        public FormTextBox(string baseId)
            : this(baseId, baseId.ToLower())
        {
        }

        #endregion

        public override string ToString()
        {
            return string.Format("{0}: {1}, Label: {2}, Text: {3}", GetType().Name, BaseId, Label, Text);
        }
    }
}
