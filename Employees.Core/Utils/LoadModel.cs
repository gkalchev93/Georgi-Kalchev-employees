using Employees.Core.Model;
using System;
using System.Collections.Generic;

namespace Employees.Core.Utils
{
    public static class LoadModel
    {
        public static List<EmployeeExp> ParseEmployeeExp(string[] lines, string delimiter, string dateFormat)
        {
            List<EmployeeExp> retCollection = new List<EmployeeExp>();
            if (string.IsNullOrEmpty(delimiter))
                delimiter = ",";

            foreach (string line in lines)
            {
                if (string.IsNullOrEmpty(line))
                    continue;

                string[] values = line.Split(new string[] { delimiter }, StringSplitOptions.None);
                if (values.Length != 4)
                    continue;

                EmployeeExp tmpObj = new EmployeeExp
                {
                    EmpID = int.Parse(values[0]),
                    ProjectID = int.Parse(values[1]),
                    DateFrom = values[2].ToDate(dateFormat),
                    DateTo = values[3].ToDate(dateFormat)
                };

                retCollection.Add(tmpObj);
            }

            return retCollection;
        }
    }
}
