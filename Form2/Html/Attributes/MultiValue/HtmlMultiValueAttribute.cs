using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Form2.Html.Interfaces;

namespace Form2.Html.Attributes.MultiValue
{
    public abstract class HtmlMultiValueAttribute<T> : List<T>, IHtmlAttribute
    {
        #region Properties

        public abstract string Name { get; }

        public bool IsSet
        {
            get { return Count > 0; }
        }

        #endregion


        #region Constructors

        public HtmlMultiValueAttribute(IEnumerable<T> values)
            : base(values)
        {
        }

        public HtmlMultiValueAttribute()
            : base()
        {
        }

        #endregion

        public override string ToString()
        {
            if (!IsSet)
                return "";

            StringBuilder sb = new StringBuilder();

            sb.Append(string.Format(@" {0}=""", Name));

            for (int i = 0; i < Count; i++)
            {
                sb.Append(this[i]);

                if (i == Count - 1)
                    continue;

                sb.Append(" ");
            }

            sb.Append(@"""");

            return sb.ToString();
        }
    }
}
