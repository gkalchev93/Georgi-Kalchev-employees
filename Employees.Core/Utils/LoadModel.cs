using Employees.Core.Model;
using System;
using System.Collections.Generic;

namespace Employees.Core.Utils
{
    public static class LoadModel
    {
        public static List<EmployeeExp> EmpExperianceFromLines(string[] lines, char delimiter = ',')
        {
            List<EmployeeExp> retCollection = new List<EmployeeExp>();

            foreach (string line in lines)
            {
                if (string.IsNullOrEmpty(line))
                    continue;

                string[] values = line.Split(delimiter);
                if (values.Length != 4)
                    continue;

                EmployeeExp tmpObj = new EmployeeExp
                {
                    EmpID = int.Parse(values[0]),
                    ProjectID = int.Parse(values[1]),
                    DateFrom = DateTime.Parse(values[2])
                };

                if (DateTime.TryParse(values[3], out DateTime dateToValue))
                {
                    tmpObj.DateTo = dateToValue;
                }
                else
                {
                    tmpObj.DateTo = DateTime.Now;
                }

                retCollection.Add(tmpObj);
            }

            return retCollection;
        }
    }
}
