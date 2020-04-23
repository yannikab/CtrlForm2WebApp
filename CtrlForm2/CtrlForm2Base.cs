using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

using UserControls.CtrlForm2.FormElements;
using UserControls.CtrlForm2.FormElements.FormItems;
using UserControls.CtrlForm2.HtmlElements.HtmlGroups;
using UserControls.CtrlForm2.Visitors;

namespace UserControls.CtrlForm2
{
    public abstract class CtrlForm2Base : UserControl
    {
        #region Fields

        protected Literal LtrContent { get; set; }

        private readonly Stack<FormGroup> groups = new Stack<FormGroup>();

        private FormGroup formContainer;

        private HtmlGroup htmlContainer;

        #endregion

        #region Properties

        private FormGroup FormContainer
        {
            get { return formContainer; }
            set { formContainer = value; }
        }

        private HtmlGroup HtmlContainer
        {
            get { return htmlContainer; }
            set { htmlContainer = value; }
        }

        #endregion

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            CreateForm();

            if (FormContainer == null)
                return;

            HtmlContainer = new Form2HtmlVisitor(FormContainer).Html;

            LtrContent.Text = new Html2TextVisitor(HtmlContainer).Text;
        }

        protected abstract void CreateForm();

        protected void OpenGroup(string baseId)
        {
            FormGroup g = new FormGroup(baseId);

            if (groups.Count == 0)
            {
                FormContainer = g;
                FormContainer.RequiredMark = "*";
                FormContainer.ElementOrder = ElementOrder.InputLabelMark;
            }
            else
            {
                groups.Peek().Add(g);
            }

            groups.Push(g);
        }

        protected void CloseGroup()
        {
            if (groups.Count == 0)
                throw new InvalidOperationException();

            groups.Pop();
        }

        protected void SetRequiredMark(string mark)
        {
            if (groups.Count == 0)
                throw new InvalidOperationException();

            groups.Peek().RequiredMark = mark;
        }

        protected void SetElementOrder(ElementOrder order)
        {
            if (groups.Count == 0)
                throw new InvalidOperationException();

            groups.Peek().ElementOrder = order;
        }

        protected void AddItem(FormItem formItem)
        {
            if (formItem is FormGroup)
                throw new ArgumentException();

            if (groups.Count == 0)
                throw new ApplicationException();

            groups.Peek().Add(formItem);
        }
    }
}
