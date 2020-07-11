using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Form2.Html.Visitors;

namespace Form2
{
    public class Form2Renderer
    {
        private readonly Form2Model form;

        public Form2Renderer(Form2Model form)
        {
            this.form = form;
        }

        public string Render()
        {
            return new Html2TextVisitor(form.Html).Text;
        }
    }
}
