using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using UserControls.CtrlForm2.FormElements;

namespace UserControls.CtrlForm2.Visitors
{
    [SuppressMessage("Style", "IDE0028:Simplify collection initialization", Justification = "<Pending>")]

    static class HtmlIconExtensions
    {
        public static IEnumerable<string> GetClassNames(this FormIcon icon)
        {
            List<string> classNames = new List<string>();

            classNames.Add("fa");

            switch (icon)
            {
                case FormIcon.Envelope:
                case FormIcon.Lock:
                case FormIcon.Calendar:
                case FormIcon.Phone:
                case FormIcon.Mobile:

                    classNames.Add(string.Format("{0}-{1}", "fa", icon.ToString().ToLower()));
                    break;

                default:
                case FormIcon.NotSet:

                    classNames.Clear();
                    break;
            }

            return classNames;
        }
    }
}
