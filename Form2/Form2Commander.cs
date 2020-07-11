using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.SessionState;

using Form2.Form.Content;
using Form2.Form.Interfaces;

namespace Form2
{
    [SuppressMessage("Style", "IDE0019:Use pattern matching", Justification = "<Pending>")]

    public class Form2Commander
    {
        private readonly Form2Model form;

        public Form2Commander(Form2Model form)
        {
            this.form = form;
        }

        public void HandleRequest(bool isPostBack, HttpRequest request, HttpSessionState session)
        {
            if (!isPostBack)
                this.form.Initialize(session);

            string eventTarget = request["__EVENTTARGET"];
            string eventArgument = request["__EVENTARGUMENT"];

            if (eventTarget == null || eventArgument == null)
                return;

            FormItem formItem = this.form.GetItem(eventTarget);
            string argument = eventArgument;

            NameValueCollection form = new NameValueCollection();

            foreach (var key in request.Form.Keys)
            {
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

                this.form.Update(formItem, argument, form, session);
            }
            else
            {
                if (!iSubmit.IsSubmit)
                    throw new ApplicationException();

                this.form.Submit(formItem, argument, form, session);
            }
        }
    }
}
