using System;

namespace Employees.Core.Model
{
    public class Work
    {
        public int ProjectId { get; set; }
        public DateTime DateFrom { get; set; }
        public DateTime DateTo { get; set; }
        public int TotalDays
        {
            get
            {
                return GetDays();
            }
        }

        private int GetDays()
        {
            return (int)(DateTo - DateFrom).TotalDays;
        }
    }
}
