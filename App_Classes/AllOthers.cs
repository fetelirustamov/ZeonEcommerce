using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;

namespace ZeonEcommerce.App_Classes
{
    public static class AllOthers
    {
        public static string ToTitleCase(this string s) =>CultureInfo.InvariantCulture.TextInfo.ToTitleCase(s.ToLower());


    }
}