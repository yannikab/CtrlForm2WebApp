using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Form2.Form.Content.Items
{
    public class FormLabel : FormItem
    {
        #region Fields

        private string content;

        #endregion


        #region Properties

        public string Content
        {
            get { return content; }
            set { content = value; }
        }

        public string Value
        {
            get { return content ?? ""; }
        }

        #endregion


        #region Constructors

        public FormLabel(string name)
            : base(name)
        {
            Content = "";
        }

        #endregion


        #region Object

        public override string ToString()
        {
            return string.Format("{0} (Name: '{1}', Value: '{2}')", GetType().Name, Name, Value);
        }

        #endregion
    }
}
