using System;
using System.Globalization;

namespace Employees.Core.Utils
{
    public static class Extensions
    {
        public static DateTime ToDate(this string obj, string format)
        {
            DateTime date;

            if (obj.Trim().ToLowerInvariant() != "null")
            {
                if (string.IsNullOrEmpty(format))
                {
                    if (!DateTime.TryParse(obj, out date))
                    {
                        CultureInfo usCulture = new CultureInfo("en-US");
                        if (!DateTime.TryParse(obj, usCulture, DateTimeStyles.None, out date))
                        {
                            CultureInfo ukCulture = new CultureInfo("en-GB");
                            if (!DateTime.TryParse(obj, ukCulture, DateTimeStyles.None, out date))
                            {
                                throw new Exception($"Failed to parse date - {obj}");
                            }
                        }
                    }
                }
                else
                {
                    date = DateTime.ParseExact(obj, format, CultureInfo.InvariantCulture);
                }
            }
            else
            {
                date = DateTime.Now;
            }

            return date;
        }
    }
}
