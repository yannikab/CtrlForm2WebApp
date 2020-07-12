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
    [SuppressMessage("Style", "IDE0019:Use pattern matching", Justification = "<Pending>")]
    
    public class FormCommander
    {
        private readonly FormModel formModel;

        public FormCommander(FormModel formModel)
        {
            this.formModel = formModel;
        }

        public void HandleRequest(bool isPostBack, HttpRequest request)
        {
            if (!isPostBack)
                formModel.Initialize();

            string eventTarget = request["__EVENTTARGET"];
            string eventArgument = request["__EVENTARGUMENT"];

            if (eventTarget == null || eventArgument == null)
                return;

            FormItem formItem = formModel.GetItem(eventTarget);
            string argument = eventArgument;

            NameValueCollection form = new NameValueCollection();

            foreach (var key in request.Form.Keys)
            {
                if (key == null)
                    continue;

                if (key.ToString().StartsWith("__"))
                    continue;

                form.Add(key.ToString(), request.Form[key.ToString()]);
            }

            ISubmit iSubmit = formItem as ISubmit;

            if (iSubmit == null)
            {
                IPostBack iPostBack = formItem as IPostBack;

                if (iPostBack == null || !iPostBack.IsPostBack)
                    throw new ApplicationException();

                formModel.Update(formItem, argument, form);
            }
            else
            {
                if (!iSubmit.IsSubmit)
                    throw new ApplicationException();

                formModel.Submit(formItem, argument, form);
            }
        }
    }
}
