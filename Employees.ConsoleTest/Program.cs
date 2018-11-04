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
            try
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
                    List<EmployeeExp> records = LoadModel.ParseEmployeeExp(fileLines, ",", null);
                    Console.WriteLine("Records count:" + records.Count);
                    Console.WriteLine();

                    if (records.Count > 0)
                    {
                        List<TeamWork> teamWorkExp = Statistics.GetTeamWorkPeriods(records);
                        foreach (TeamWork work in teamWorkExp)
                        {
                            Console.WriteLine($"Team {work.TeamKey} has work experiance together {work.TotalDays}");

                            foreach (Work w in work.WorkTogether)
                                Console.WriteLine($"\t ProjectId - {w.ProjectId} From: {w.DateFrom.ToShortDateString()} To: {w.DateTo.ToShortDateString()}");

                            Console.WriteLine();
                        }

                        TeamWork maxDays = teamWorkExp.OrderBy(x => x.TotalDays).Last();
                        Console.WriteLine($"Team {maxDays.TeamKey} has the longest experiance together {maxDays.TotalDays} days.");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            Console.ReadLine();
        }
    }
}
