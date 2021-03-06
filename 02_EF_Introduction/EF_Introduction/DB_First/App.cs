﻿using DB_First.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DB_First
{
    class App
    {
        static void Main()
        {
            while (true)
            {
                Console.Write("Command: ");
                var cmd = Console.ReadLine();

                if (cmd == "exit") return;

                switch (cmd)
                {
                    case "task 3": EmployessById(); break;
                    case "task 4": EmployeesWithSalaryOver50(); break;
                    case "task 5": EmployeesFromResearchAndDev(); break;
                    case "task 6": AddingNewAddressUpdatingEmployee(); break;
                    case "task 7": FindEmployeesInPeriod(); break;
                    case "task 8": AddressesByTownName(); break;
                    case "task 9": EmployeeWithId(); break;
                    case "task 10": DepartmentsWithMoreThan(); break;
                    case "task 11": FindLatestTenProjects(); break;
                    case "task 12": IncreaseSalaries(); break;
                    default:
                        Console.WriteLine("Use only 'task <int>' syntax to speciify task from homework. 'exit' command to return.");
                        break;
                }

                Console.WriteLine();
            }
        }

        private static void IncreaseSalaries()
        {
            var targetDepartments = new string[] { "Engineering", "Tool Design", "Marketing", "Information Services" };
            using (var ctx = new SoftuniContext())
            {
                var filtered = ctx.Employees.Where(e => targetDepartments.Contains(e.Department.Name)).ToList();
                filtered.ForEach(emp =>
                {
                    emp.Salary += 0.12m * emp.Salary;
                    Console.WriteLine($"{emp.FirstName} {emp.LastName} (${emp.Salary:F6})");
                });

                //ctx.SaveChanges();

            }
        }

        private static void FindLatestTenProjects()
        {
            using (var ctx = new SoftuniContext())
            {
                var lastTen = ctx.Projects.OrderByDescending(p => p.StartDate).Take(10).OrderBy(p => p.Name).ToList();

                foreach (var proj in lastTen)
                {
                    Console.WriteLine($"{proj.Name} {proj.Description} {proj.StartDate.ToString()} {proj.EndDate.ToString()}");
                }
            }
        }

        private static void DepartmentsWithMoreThan()
        {
            using (var ctx = new SoftuniContext())
            {
                var filtered = ctx.Departments.Where(d => d.Employees.Count > 5).OrderBy(d => d.Employees.Count).ToList();

                foreach (var dep in filtered)
                {
                    Console.WriteLine($"{dep.Name} {ctx.Employees.FirstOrDefault(e =>e.EmployeeID == dep.ManagerID).FirstName}");
                    dep.Employees.ToList().ForEach(e => Console.WriteLine($"{e.FirstName} {e.LastName} {e.JobTitle}"));
                }
            }
        }

        private static void EmployeeWithId()
        {
            using (var ctx = new SoftuniContext())
            {
                var e = ctx.Employees.FirstOrDefault(emp => emp.EmployeeID == 147);

                Console.WriteLine($"{e.FirstName} {e.LastName} {e.JobTitle}");

                e.Projects.OrderBy(p => p.Name).ToList().ForEach(p => Console.WriteLine($"{p.Name}"));
            }
        }

        private static void AddressesByTownName()
        {
            using (var ctx = new SoftuniContext())
            {
                var filtered = ctx.Addresses.OrderByDescending(a => a.Employees.Count).ThenBy(a => a.Town.Name).Take(10).ToList();

                foreach (var a in filtered)
                {
                    Console.WriteLine($"{a.AddressText}, {a.Town.Name} - {a.Employees.Count} employees");
                }
            }
        }

        private static void FindEmployeesInPeriod()
        {
            using (var ctx =  new SoftuniContext())
            {
                var filtered = ctx.Employees.Where(e => e.Projects.Any(p => p.StartDate.Year >= 2001 && p.StartDate.Year <= 2003)).Take(30).ToList();

                foreach (var emp in filtered)
                {
                    Console.WriteLine($"{emp.FirstName} {emp.LastName} {emp.Manager.FirstName}");
                    emp.Projects.ToList().ForEach(p => Console.WriteLine($"--{p.Name} {p.StartDate.ToString()} {p.EndDate.ToString()}"));
                }

            }
        }

        private static void AddingNewAddressUpdatingEmployee()
        {

            using (var ctx = new SoftuniContext())
            {
                var newAddress = new Address()
                {
                    AddressText = "Vitoska 15",
                    TownID = 4
                };

                var emp = ctx.Employees.FirstOrDefault(e => e.LastName == "Nakov");

                if(emp != null)
                {
                    emp.Address = newAddress;
                }

                ctx.SaveChanges();

                var adds = ctx.Employees.OrderByDescending(e => e.AddressID).Take(10).Select(e => e.Address.AddressText);
                foreach (var address in adds.ToList())
                {
                    Console.WriteLine(address.ToString());
                }
            }
        }

        private static void EmployeesFromResearchAndDev()
        {
            using (var ctx = new SoftuniContext())
            {
                var filtered = ctx.Employees
                    .Where(e => e.Department == ctx.Departments.FirstOrDefault(d => d.Name == "Research and Development"))
                    .OrderBy(e => e.Salary)
                    .ThenByDescending(e => e.FirstName);

                foreach (var e in filtered)
                {
                    Console.WriteLine($"{e.FirstName} {e.LastName} from {e.Department.Name} - ${e.Salary:F2}");
                }
            }
        }

        private static void EmployeesWithSalaryOver50()
        {
            using (var ctx = new SoftuniContext())
            {
                var filtered = ctx.Employees
                    .Where(e => e.Salary > 50000)
                    .Select(e => e.FirstName)
                    .ToList();

                foreach (var emp in filtered)
                {
                    Console.WriteLine(emp);
                }
            }
        }

        private static void EmployessById()
        {
            using (var ctx = new SoftuniContext())
            {
                var employees = ctx.Employees.OrderBy(e => e.EmployeeID).Select(e => new
                {
                    e.FirstName,
                    e.LastName,
                    e.MiddleName,
                    e.JobTitle,
                    e.Salary
                }).ToList();

                foreach (var emp in employees)
                {
                    Console.WriteLine($"{emp.FirstName} {emp.LastName} {emp.MiddleName} {emp.JobTitle} {emp.Salary}");
                }
            }
        }
    }
}
