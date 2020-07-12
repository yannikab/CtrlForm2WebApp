using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Form2.Form.Visitors;
using Form2.Html.Content.Elements;
using Form2.Html.Visitors;

namespace Form2
{
    public class FormRenderer
    {
        private readonly FormModel formModel;

        public FormRenderer(FormModel formModel)
        {
            this.formModel = formModel;
        }

        public string Render()
        {
            HtmlContainer htmlContainer = new Form2HtmlVisitor(formModel.FormSection, formModel.Submitted).Html;

            return new Html2TextVisitor(htmlContainer).Text;
        }
    }
}
