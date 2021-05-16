using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

using Form2.Form.Content;
using Form2.Form.Interfaces;

namespace Form2
{
    public class FormCommander
    {
        private readonly FormModel formModel;

        public FormCommander(FormModel formModel)
        {
            this.formModel = formModel;
        }

        public void HandleRequest(HttpRequest request)
        {
            if (formModel == null)
                return;

            string eventTarget = request["__EVENTTARGET"];
            string eventArgument = request["__EVENTARGUMENT"];

            if (eventTarget == null || eventArgument == null)
                return;

            FormItem source = formModel.GetItem(eventTarget);
            string argument = eventArgument;

            NameValueCollection values = new NameValueCollection();

            foreach (var key in request.Form.Keys)
            {
                if (key == null)
                    continue;

                if (key.ToString().StartsWith("__"))
                    continue;

                values.Add(key.ToString(), request.Form[key.ToString()]);
            }

            IUpdate iUpdate = source as IUpdate;
            ISubmit iSubmit = source as ISubmit;

            if (iUpdate == null && iSubmit == null)
                return;

            if (iSubmit != null && iSubmit.Submit)
            {
                formModel.Submit(values, source, argument);
            }
            else if (iUpdate != null && iUpdate.Update)
            {
                formModel.Update(values, source, argument);
            }
        }
    }
}
