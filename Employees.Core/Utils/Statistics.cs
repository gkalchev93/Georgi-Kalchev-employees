using Employees.Core.Model;
using System;
using System.Collections.Generic;

namespace Employees.Core.Utils
{
    public static class Statistics
    {
        public static Dictionary<Tuple<int, int>, int> GetTeamWorkDurations(List<EmployeeExp> employeeExps)
        {
            Dictionary<Tuple<int, int>, int> result = new Dictionary<Tuple<int, int>, int>();

            for (int i = 0; i < employeeExps.Count; i++)
            {
                EmployeeExp empExp1 = employeeExps[i];
                for (int j = i; j < employeeExps.Count; j++)
                {
                    EmployeeExp empExp2 = employeeExps[j];

                    // Continue, if the experiance is for same person
                    if (empExp1.EmpID == empExp2.EmpID || empExp1.ProjectID != empExp2.ProjectID)
                    {
                        continue;
                    }

                    int teamWorkDuration = CalculateTeamWork(empExp1, empExp2);

                    if (teamWorkDuration > 0)
                    {
                        Tuple<int, int> teamKey = GetTeamKey(empExp1, empExp2);
                        if (result.ContainsKey(teamKey))
                        {
                            result[teamKey] += teamWorkDuration;
                        }
                        else
                        {
                            result.Add(teamKey, teamWorkDuration);
                        }
                    }
                }
            }

            return result;
        }

        private static Tuple<int, int> GetTeamKey(EmployeeExp empExp1, EmployeeExp empExp2)
        {
            if (empExp1.EmpID > empExp2.EmpID)
            {
                return new Tuple<int, int>(empExp1.EmpID, empExp2.EmpID);
            }
            else
            {
                return new Tuple<int, int>(empExp2.EmpID, empExp1.EmpID);
            }
        }

        private static int CalculateTeamWork(EmployeeExp empExp1, EmployeeExp empExp2)
        {
            int workDuration = 0;

            if (empExp1.DateFrom > empExp1.DateTo)
            {
                throw new Exception(string.Format(Constants.InvalidIntervalMsg, empExp1.EmpID, empExp1.ProjectID));
            }
            else if (empExp2.DateFrom > empExp2.DateTo)
            {
                throw new Exception(string.Format(Constants.InvalidIntervalMsg, empExp2.EmpID, empExp2.ProjectID));
            }

            if (empExp1.DateFrom == empExp2.DateFrom && empExp1.DateTo == empExp1.DateTo)
            {
                workDuration = GetDays(empExp1.DateFrom, empExp1.DateTo);
            }
            else if (empExp1.DateFrom <= empExp2.DateFrom)
            {
                if (empExp1.DateTo >= empExp2.DateTo)
                {
                    workDuration = GetDays(empExp2.DateFrom, empExp2.DateTo);
                }
                else if (empExp1.DateTo < empExp2.DateTo)
                {
                    workDuration = GetDays(empExp2.DateFrom, empExp1.DateTo);
                }
            }
            else
            {
                if (empExp1.DateTo <= empExp2.DateTo)
                {
                    workDuration = GetDays(empExp1.DateFrom, empExp1.DateTo);
                }
                else if (empExp2.DateTo < empExp1.DateTo && empExp2.DateTo > empExp1.DateFrom)
                {
                    workDuration = GetDays(empExp1.DateFrom, empExp2.DateTo);
                }
            }

            return workDuration;
        }

        private static int GetDays(DateTime dateFrom, DateTime dateTo)
        {
            return (int)(dateTo - dateFrom).TotalDays;
        }
    }
}
