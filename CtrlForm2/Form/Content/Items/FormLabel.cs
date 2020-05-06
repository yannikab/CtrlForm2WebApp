using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CtrlForm2.Form.Content.Items
{
    public class FormLabel : FormItem
    {
        #region Fields

        private string label;

        #endregion


        #region Properties

        public string Label
        {
            get { return label; }
            set { label = value; }
        }

        #endregion


        #region Constructors

        public FormLabel(string baseId, string formId)
            : base(baseId, formId)
        {
            label = "";
        }

        public FormLabel(string baseId)
            : this(baseId, baseId.ToLower())
        {
        }

        #endregion


        #region Object

        public override string ToString()
        {
            return string.Format("{0}: {1}, Label: {2}", GetType().Name, BaseId, Label);
        }

        #endregion
    }
}
