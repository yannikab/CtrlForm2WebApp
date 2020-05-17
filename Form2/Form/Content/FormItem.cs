using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Form2.Form.Content.Items;

namespace Form2.Form.Content
{
    public abstract class FormItem : FormContent
    {
        public FormItem(string baseId, string formId)
            : base(baseId, formId)
        {
        }
    }
}
