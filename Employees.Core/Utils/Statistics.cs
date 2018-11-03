using Employees.Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Employees.Core.Utils
{
    public static class Statistics
    {
        public static List<TeamWork> GetTeamWorkPeriods(List<EmployeeExp> employeeExps)
        {
            List<TeamWork> result = new List<TeamWork>();

            for (int i = 0; i < employeeExps.Count; i++)
            {
                EmployeeExp empExp1 = employeeExps[i];
                for (int j = i; j < employeeExps.Count; j++)
                {
                    EmployeeExp empExp2 = employeeExps[j];

                    if (empExp1.EmpID == empExp2.EmpID || empExp1.ProjectID != empExp2.ProjectID)
                    {
                        // Continue, if the experiance is for same person or the project is differents
                        continue;
                    }

                    Work work = CalculateTeamWork(empExp1, empExp2);

                    if (work.DateFrom != null)
                    {
                        TeamWork teamWork = new TeamWork(empExp1.EmpID, empExp2.EmpID);

                        if (result.Any(x => x.TeamKey == teamWork.TeamKey))
                        {
                            result
                                .First(x => x.TeamKey == teamWork.TeamKey)
                                .WorkTogether.Add(work);
                        }
                        else
                        {
                            teamWork.WorkTogether.Add(work);
                            result.Add(teamWork);
                        }
                    }
                }
            }

            return result;
        }

        private static Work CalculateTeamWork(EmployeeExp empExp1, EmployeeExp empExp2)
        {
            if (empExp1.DateFrom > empExp1.DateTo)
            {
                throw new Exception(string.Format(Constants.InvalidIntervalMsg, empExp1.EmpID, empExp1.ProjectID));
            }
            else if (empExp2.DateFrom > empExp2.DateTo)
            {
                throw new Exception(string.Format(Constants.InvalidIntervalMsg, empExp2.EmpID, empExp2.ProjectID));
            }

            (DateTime, DateTime) dateIntersection =
                GetDateRangesIntersection(empExp1.DateFrom, empExp1.DateTo, empExp2.DateFrom, empExp2.DateTo);

            return
                new Work()
                {
                    ProjectId = empExp1.ProjectID,
                    DateFrom = dateIntersection.Item1,
                    DateTo = dateIntersection.Item2
                };
        }

        private static (DateTime, DateTime) GetDateRangesIntersection(DateTime dateFrom1, DateTime dateTo1, DateTime dateFrom2, DateTime dateTo2)
        {
            if (dateFrom1 == dateFrom2 && dateTo1 == dateTo2)
            {
                return (dateFrom1, dateTo1);
            }
            else if (dateFrom1 <= dateFrom2)
            {
                if (dateTo1 >= dateTo2)
                {
                    return (dateFrom2, dateTo2);
                }
                else if (dateTo1 < dateTo2)
                {
                    return (dateFrom2, dateTo1);
                }
            }
            else
            {
                if (dateTo1 <= dateTo2)
                {
                    return (dateFrom1, dateTo1);
                }
                else if (dateTo2 < dateTo1 && dateTo2 > dateFrom1)
                {
                    return (dateFrom1, dateTo2);
                }
            }

            return (DateTime.MinValue, DateTime.MinValue);
        }
    }
}
