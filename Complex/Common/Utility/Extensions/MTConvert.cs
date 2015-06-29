using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Complex.Common.Utility.Extensions
{
    public static class MTConvert
    {
        public static T ReferenceFromType<TK, T>(this TK text) where TK : class   
        { 
            try
            {
                return (T) Convert.ChangeType(text, typeof(T), System.Globalization.CultureInfo.InvariantCulture);
            }
            catch
            {
                return default(T);
            }
        }



        public static T ValueFromType<TK, T>(this TK text) where TK : struct
        {

            try
            {
                return (T) Convert.ChangeType(text, typeof(T), System.Globalization.CultureInfo.InvariantCulture);
            }
            catch
            {
                return default(T);
            }
        }
    }
}
