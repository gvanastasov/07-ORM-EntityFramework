using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOP_intro
{
    class Program
    {
        static void Main(string[] args)
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("Use 'task <int>' to initialize the corresponding task or the 'exit' keyword to terminate the program.");
                Console.WriteLine();

                var cmd = Console.ReadLine();

                if(cmd == "exit")
                {
                    break;
                }

                var tokens = cmd.Split(' ');
                var taskIndex = 0;

                if (tokens.Length < 2 || tokens[0] != "task" || int.TryParse(tokens[1], out taskIndex) == false)
                {
                    Console.WriteLine("Wrong input. Use 'task <int>' to fire the corresponding task.");
                    continue;
                }

                switch (taskIndex)
                {
                    case 1:
                        {
                            DefinePersonClass();
                        }
                        break;
                    case 2:
                        {
                            CreatePersonsConstructors();
                        }
                        break;
                    case 3:
                        {
                            OldestFamilyMember();
                        }
                        break;
                    case 4:
                        {
                            StudentsCounter();
                        }
                        break;
                    case 5:
                        {
                            PlanckConstant();
                        }
                        break;
                    case 6:
                        {

                        }
                        break;
                }

            }


        }

        private static void PlanckConstant()
        {
            Console.Clear();
            Console.WriteLine("Static Class definition for Planck reduced constant: " + Calculation.ReducedPlanckConstant());
            Console.WriteLine();

            Console.WriteLine("Press any key to return...");
            Console.ReadLine();
        }

        private static void StudentsCounter()
        {
            Console.Clear();
            Console.WriteLine("Count Students: create instances of the student class and keep a static count of them. Use keyword 'end' to return to task menu.");
            Console.WriteLine();

            while (true)
            {
                var cmd = Console.ReadLine();

                if (cmd == "end") break;

                var student = new Student() { Name = cmd };

            }

            Console.WriteLine("Instances count: " + Student.Count);
            Console.WriteLine("Press any key to return...");
            Console.ReadLine();
        }

        private static void OldestFamilyMember()
        {
            Console.Clear();
            Console.WriteLine("Create Family: use the proper ctors");
            Console.WriteLine();

            var family = new Family();

            Console.Write("Family members: ");
            var familyCount = int.Parse(Console.ReadLine());

            while (family.people.Count != familyCount)
            {
                Console.WriteLine("member: ");
                var input = Console.ReadLine();

                var tokens = input.Split(' ');

                var person = new Person(tokens[0], int.Parse(tokens[1]));

                family.AddMember(person);
            }

            Console.WriteLine("Oldest member: " + family.GetOldestMember());
            Console.WriteLine("Press any key to return...");
            Console.ReadLine();
        }
        private static void CreatePersonsConstructors()
        {
            Console.Clear();
            Console.WriteLine("Create Persons Constructors: use the proper ctors or the 'back' keyword to return to task selection meny.");
            Console.WriteLine();

            while (true)
            {
                var input = Console.ReadLine();

                if(input == "back")
                {
                    break;
                }

                var tokens = input.Split(',');

                try
                {
                    if (tokens.Length == 0 || (tokens.Length == 1 && string.IsNullOrEmpty(tokens[0])))
                    {
                        var person = new Person();
                        Console.WriteLine("Created instance of person without any parameters: " + person);
                    }
                    else if (tokens.Length == 2)
                    {
                        var person = new Person(tokens[0], int.Parse(tokens[1]));
                        Console.WriteLine("Created instance of person with both parameters: " + person);
                    }
                    else
                    {
                        var age = 0;
                        if (int.TryParse(tokens[0], out age))
                        {
                            var person = new Person(age);
                            Console.WriteLine("Created instance of person with age: " + person);
                        }
                        else
                        {
                            var person = new Person(tokens[0]);
                            Console.WriteLine("Created instance of person with name: " + person);
                        }
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine("Something went wrong: " + e.Message + "... Use one of the task ctor-s.");
                }
               
            }
        }
        private static void DefinePersonClass()
        {
            try
            {
                var p = new Person() { Name = "Pesho", Age = 20 };
                var g = new Person() { Name = "Gosho", Age = 18 };
                var s = new Person() { Name = "Stamat", Age = 43 };
                Console.WriteLine("Succesfully created 3 instances of: " + typeof(Person).Name);
                Console.WriteLine("Press any key to return...");
                Console.ReadLine();
            }
            catch (Exception e)
            {
                throw;
            }
        }
    }
}
