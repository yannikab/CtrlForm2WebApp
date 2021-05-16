using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Form2.Form.Visitors;
using Form2.Form.Visitors.Interfaces;
using Form2.Html.Visitors;

namespace Form2
{
    public class FormRenderer
    {
        private readonly FormModel formModel;

        private readonly IForm2HtmlVisitor iForm2HtmlVisitor;

        public FormRenderer(FormModel formModel, bool verbose)
        {
            this.formModel = formModel;

            iForm2HtmlVisitor = new Form2HtmlVisitor(formModel, verbose);
            //iForm2HtmlVisitor = new Form2HtmlMELOVisitor(formModel, verbose);
        }

        public FormRenderer(FormModel formModel)
            : this(formModel, false)
        {
        }

        public string Html
        {
            get
            {
                new FormIconVisitor(formModel.FormGroup, iForm2HtmlVisitor.Html, false);

                return new Html2TextVisitor(iForm2HtmlVisitor.Html).Text;
            }
        }

        public IEnumerable<string> Scripts
        {
            get
            {
                foreach (var s in iForm2HtmlVisitor.Scripts)
                    yield return new Html2TextVisitor(s).Text;
            }
        }
    }
}
