using System.Collections.Generic;

namespace Employees.Core.Model
{
    public class TeamWork
    {
        public int Emp1Id { get; set; }
        public int Emp2Id { get; set; }
        public string TeamKey { get; set; }
        public List<Work> WorkTogether { get; set; }

        public TeamWork(int empID1, int empID2)
        {
            Emp1Id = empID1;
            Emp2Id = empID2;
            TeamKey = GetTeamKey();
            WorkTogether = new List<Work>();
        }

        private string GetTeamKey()
        {
            string template = "{0}-{1}";
            if (Emp1Id > Emp2Id)
                return string.Format(template, Emp1Id, Emp2Id);
            else
                return string.Format(template, Emp2Id, Emp1Id);
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
