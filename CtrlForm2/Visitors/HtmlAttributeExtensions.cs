using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//using UserControls.CtrlForm2.HtmlElements.HtmlGroups;
//using UserControls.CtrlForm2.HtmlElements.HtmlItems;

namespace UserControls.CtrlForm2.Visitors
{
    //[SuppressMessage("Style", "IDE0060:Remove unused parameter", Justification = "<Pending>")]

    public static class HtmlAttributeExtensions
    {
        //public static string IdAttribute(this HtmlElement e)
        //{
        //    if (e.Id == null || e.Id.Trim().Length != e.Id.Length)
        //        throw new ArgumentException();

        //    return e.Id != "" ? string.Format(@" id=""{0}""", e.Id) : "";
        //}

        //public static string ClassAttibute(this HtmlElement e)
        //{
        //    if (e.ClassNames.Count == 0)
        //        return "";

        //    StringBuilder sb = new StringBuilder();

        //    sb.Append(@" class=""");

        //    for (int i = 0; i < e.ClassNames.Count; i++)
        //    {
        //        sb.Append(e.ClassNames[i]);

        //        if (i < e.ClassNames.Count - 1)
        //            sb.Append(" ");
        //    }

        //    sb.Append(@"""");

        //    return sb.ToString();
        //}

        //public static string HiddenAttribute(this HtmlElement e)
        //{
        //    return e.IsHidden ? " hidden" : "";
        //}

        //public static string ReadOnlyAttribute(this HtmlInput e)
        //{
        //    return e.IsReadOnly ? " readonly" : "";
        //}

        //public static string NameAttribute(this HtmlInput e)
        //{
        //    if (string.IsNullOrEmpty(e.IdAttribute.Value) || e.IdAttribute.Value.Trim().Length != e.IdAttribute.Value.Length)
        //        throw new ArgumentException();

        //    return string.Format(@" name=""{0}""", e.IdAttribute.Value);
        //}

        //public static string ValueAttribute(this HtmlInput e)
        //{
        //    if (e.Value == null)
        //        throw new ArgumentNullException();

        //    if (e.Value == "")
        //        return "";

        //    return string.Format(@" value=""{0}""", e.Value);
        //}

        //public static string ForAttribute(this HtmlLabel e)
        //{
        //    if (e.For == null)
        //        throw new ArgumentNullException();

        //    if (e.For == "")
        //        return "";

        //    return string.Format(@" for=""{0}""", e.For);
        //}

        //public static string TypeAttribute(this HtmlTextBox e)
        //{
        //    return string.Format(@" type=""{0}""", "text");
        //}

        //public static string PlaceHolderAttribute(this HtmlTextBox e)
        //{
        //    if (e.PlaceHolder == null)
        //        throw new ArgumentNullException();

        //    if (e.PlaceHolder == "")
        //        return "";

        //    return string.Format(@" placeholder=""{0}""", e.PlaceHolder);
        //}

        //public static string PlaceHolderAttribute(this HtmlTextArea e)
        //{
        //    if (e.PlaceHolder == null)
        //        throw new ArgumentNullException();

        //    if (e.PlaceHolder == "")
        //        return "";

        //    return string.Format(@" placeholder=""{0}""", e.PlaceHolder);
        //}

        //public static string RowsAttribute(this HtmlTextArea e)
        //{
        //    return string.Format(@" rows=""{0}""", e.Rows);
        //}

        //public static string ColsAttribute(this HtmlTextArea e)
        //{
        //    return string.Format(@" cols=""{0}""", e.Cols);
        //}

        //public static string ButtonTypeAttribute(this HtmlSubmit button)
        //{
        //    return @" type=""submit""";
        //}
    }
}
