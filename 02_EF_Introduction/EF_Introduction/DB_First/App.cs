using DB_First.Models;
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
                    default:
                        Console.WriteLine("Use only 'task <int>' syntax to speciify task from homework. 'exit' command to return.");
                        break;
                }

                Console.WriteLine();
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
                    Console.WriteLine($"{e.FirstName} {e.LastName} from {e.Department.Name} - ${e.Salary}");
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
