using System;
using System.Collections.Generic;

namespace Employees.Core.Model
{
    public class TeamWork
    {
        public int Emp1Id { get; set; }
        public int Emp2Id { get; set; }
        public Tuple<int, int> TeamKey { get; set; }
        public List<Work> WorkTogether { get; set; }

        public TeamWork(int empID1, int empID2)
        {
            Emp1Id = empID1;
            Emp2Id = empID2;
            TeamKey = GetTeamKey();
            WorkTogether = new List<Work>();
        }

        private Tuple<int, int> GetTeamKey()
        {
            if (Emp1Id > Emp2Id)
            {
                return new Tuple<int, int>(Emp1Id, Emp2Id);
            }
            else
            {
                return new Tuple<int, int>(Emp2Id, Emp1Id);
            }
        }

        public int GetTotalDays()
        {
            int res = 0;

            foreach (Work w in WorkTogether)
                res += w.GetDays();

            return res;
        }
    }
}
