using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Form2.Form.Enums;

namespace Form2.Form.Content.Items
{
    public class FormHeader : FormLabel
    {
        #region Fields

        private FormIcon icon;

        #endregion


        #region Properties

        public FormIcon Icon
        {
            get { return icon; }
            set { icon = value; }
        }

        #endregion


        #region Constructors

        public FormHeader(string name)
            : base(name)
        {
        }

        #endregion
    }
}
