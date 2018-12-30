using System;

namespace WMIP.Extensions
{
    public static class DoubleExtensions
    {
        public static string ToButtonClass(this double score)
        {
            var buttonClass = score >= 7 ? "btn-success" : score >= 4 ? "btn-warning" : "btn-danger";
            return buttonClass;
        }
    }
}
