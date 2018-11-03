using Employees.Core.Model;
using Employees.Core.Utils;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Employees.ConsoleTest
{
    class Program
    {
        static void Main(string[] args)
        {
            string filePath = "..//..//testData";

            //Console.WriteLine("Filepath or file name, if it is in the application folder:");
            //do
            //{
            //    filePath = Console.ReadLine();
            //} while (!File.Exists(filePath));

            string[] fileLines = File.ReadAllLines(filePath);
            if (fileLines.Length > 1)
            {
                List<EmployeeExp> records = LoadModel.EmpExperianceFromLines(fileLines);
                Console.WriteLine("Records count:" + records.Count);
                Console.WriteLine();

                var teamWork = Statistics.GetTeamWorkPeriods(records);
                foreach(var work in teamWork)
                {
                    Console.WriteLine($"Team {work.TeamKey} has work experiance together {work.TotalDays}");

                    foreach(var w in work.WorkTogether)
                        Console.WriteLine($"\t ProjectId - {w.ProjectId} From: {w.DateFrom.ToShortDateString()} To: {w.DateTo.ToShortDateString()}");

                    Console.WriteLine();
                }

                Console.WriteLine("Max days in team:" + teamWork.Max(x=>x.TotalDays));
            }

            Console.ReadLine();
        }
    }
}
