using Employees.Core.Model;
using Employees.Core.Utils;
using System;
using System.Collections.Generic;
using System.IO;

namespace Employees.ConsoleTest
{
    class Program
    {
        static void Main(string[] args)
        {
            string filePath = "test";

            //Console.WriteLine("Filepath or file name, if it is in the application folder:");
            //do
            //{
            //    filePath = Console.ReadLine();
            //} while (!File.Exists(filePath));

            string[] fileLines = File.ReadAllLines(filePath);
            if (fileLines.Length > 1)
            {
                List<EmployeeExp> records = Load.ExpFromLines(fileLines);
                Console.WriteLine("Records count:" + records.Count);
                Console.WriteLine();

                var teamWork = Statistics.GetTeamWorkPeriods(records);
                foreach(var work in teamWork)
                {
                    Console.WriteLine($"Team {work.Key.Item1}-{work.Key.Item2} has work experiance together {work.Value.GetTotalDays()}");

                    foreach(var w in work.Value.WorkTogether)
                        Console.WriteLine($"\t ProjectId - {w.ProjectId} From: {w.DateFrom.ToShortDateString()} To: {w.DateTo.ToShortDateString()}");

                    Console.WriteLine();
                }
            }

            Console.ReadLine();
        }
    }
}
