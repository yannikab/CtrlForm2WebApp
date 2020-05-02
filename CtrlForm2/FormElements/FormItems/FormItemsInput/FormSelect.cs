using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserControls.CtrlForm2.FormElements.FormItems.FormItemsInput
{
    public class FormSelect : FormItemInput
    {
        #region Fields

        readonly private List<string> options;

        #endregion


        #region Properties

        public IEnumerable<string> Options
        {
            get { return options; }
            set
            {
                options.Clear();
                options.AddRange(value);
            }
        }

        public int Count
        {
            get { return options.Count; }
        }

        public string this[int index]
        {
            get { return options[index]; }
        }

        #endregion


        #region IRequired
        
        public override bool IsEntered
        {
            get;
        }

        #endregion


        #region Constructors

        public FormSelect(string baseId, string formId)
            : base(baseId, formId)
        {
            options = new List<string>();
        }

        public FormSelect(string baseId)
            : this(baseId, baseId.ToLower())
        {
        }

        #endregion
    }
}
