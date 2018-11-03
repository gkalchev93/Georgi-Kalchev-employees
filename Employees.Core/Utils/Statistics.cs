using Employees.Core.Model;
using System;
using System.Collections.Generic;

namespace Employees.Core.Utils
{
    public static class Statistics
    {
        public static Dictionary<string, TeamWork> GetTeamWorkPeriods(List<EmployeeExp> employeeExps)
        {
            Dictionary<string, TeamWork> result = new Dictionary<string, TeamWork>();

            for (int i = 0; i < employeeExps.Count; i++)
            {
                EmployeeExp empExp1 = employeeExps[i];
                for (int j = i; j < employeeExps.Count; j++)
                {
                    EmployeeExp empExp2 = employeeExps[j];
                    
                    if (empExp1.EmpID == empExp2.EmpID)
                    {
                        // Continue, if the experiance is for same person
                        continue;
                    }
                    else if (empExp1.ProjectID != empExp2.ProjectID)
                    {
                        // Continue, if the employee projects are different
                        continue;
                    }

                    Work work = CalculateTeamWork(empExp1, empExp2);

                    if (work.DateFrom != null)
                    {
                        TeamWork teamWork = new TeamWork(empExp1.EmpID, empExp2.EmpID);

                        if (result.ContainsKey(teamWork.TeamKey))
                        {
                            result[teamWork.TeamKey].WorkTogether.Add(work);
                        }
                        else
                        {
                            teamWork.WorkTogether.Add(work);
                            result.Add(teamWork.TeamKey, teamWork);
                        }
                    }
                }
            }

            return result;
        }

        private static Work CalculateTeamWork(EmployeeExp empExp1, EmployeeExp empExp2)
        {
            Work work = new Work()
            {
                ProjectId = empExp1.ProjectID
            };

            if (empExp1.DateFrom > empExp1.DateTo)
            {
                throw new Exception(string.Format(Constants.InvalidIntervalMsg, empExp1.EmpID, empExp1.ProjectID));
            }
            else if (empExp2.DateFrom > empExp2.DateTo)
            {
                throw new Exception(string.Format(Constants.InvalidIntervalMsg, empExp2.EmpID, empExp2.ProjectID));
            }

            if (empExp1.DateFrom == empExp2.DateFrom && empExp1.DateTo == empExp2.DateTo)
            {
                work.DateFrom = empExp1.DateFrom;
                work.DateTo = empExp1.DateTo;
            }
            else if (empExp1.DateFrom <= empExp2.DateFrom)
            {
                if (empExp1.DateTo >= empExp2.DateTo)
                {
                    work.DateFrom = empExp2.DateFrom;
                    work.DateTo = empExp2.DateTo;
                }
                else if (empExp1.DateTo < empExp2.DateTo)
                {
                    work.DateFrom = empExp2.DateFrom;
                    work.DateTo = empExp1.DateTo;
                }
            }
            else
            {
                if (empExp1.DateTo <= empExp2.DateTo)
                {
                    work.DateFrom = empExp1.DateFrom;
                    work.DateTo = empExp1.DateTo;
                }
                else if (empExp2.DateTo < empExp1.DateTo && empExp2.DateTo > empExp1.DateFrom)
                {
                    work.DateFrom = empExp1.DateFrom;
                    work.DateTo = empExp2.DateTo;
                }
            }

            return work;
        }
    }
}
