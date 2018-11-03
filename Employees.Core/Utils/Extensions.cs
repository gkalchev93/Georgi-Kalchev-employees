using System;
using System.Globalization;

namespace Employees.Core.Utils
{
    public static class Extensions
    {
        public static DateTime ToDate(this string obj)
        {
            DateTime date;

            if(obj.Trim().ToLowerInvariant() != "null")
            {
                try
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
                catch
                {
                    throw;
                }
            }
            else
            {
                date = DateTime.Now;
            }
            
            return date;
        }

        private static DateTime ToDateExact(string obj)
        {
            CultureInfo usCulture = new CultureInfo("en-US");
            CultureInfo ukCulture = new CultureInfo("en-GB");

            if (!DateTime.TryParse(obj, usCulture, DateTimeStyles.None, out DateTime date)
                || !DateTime.TryParse(obj, ukCulture, DateTimeStyles.None, out date))
            {
                throw new Exception($"Failed to parse date - {obj}");
            }

            return date;
        }
    }
}
