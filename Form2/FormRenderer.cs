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

        private readonly bool verbose;

        public FormRenderer(FormModel formModel, bool verbose)
        {
            this.formModel = formModel;
            this.verbose = verbose;
        }

        public FormRenderer(FormModel formModel)
            : this(formModel, false)
        {
            this.formModel = formModel;
        }

        public string Render()
        {
            HtmlContainer htmlContainer = new Form2HtmlVisitor(formModel, verbose).Html;

            return new Html2TextVisitor(htmlContainer).Text;
        }
    }
}
