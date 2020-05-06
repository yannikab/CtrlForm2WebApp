using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CtrlForm2.Html.Content
{
    public class HtmlText : HtmlContent
    {
        #region Fields

        private readonly string text;

        #endregion


        #region Properties

        public string Text
        {
            get { return text; }
        }

        #endregion


        #region Constructors

        public HtmlText(string text)
        {
            this.text = text ?? "";
        }

        #endregion


        #region Object

        public override string ToString()
        {
            return string.Format(@"{0}: ""{1}""", GetType().Name, Text);
        }

        #endregion
    }
}
