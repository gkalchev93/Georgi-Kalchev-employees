using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Employees.Core.Model
{
    public class Work
    {
        public int ProjectId { get; set; }
        public DateTime DateFrom { get; set; }
        public DateTime DateTo { get; set; }

        public int GetDays()
        {
            return (int)(DateTo - DateFrom).TotalDays;
        }
    }
}
