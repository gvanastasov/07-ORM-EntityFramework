using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DB_Apps_Introduction
{
    class Minions
    {
        static void Main()
        {
            SqlConnection connection = new SqlConnection(@"
data source=(localdb)\MSSQLLocalDB;
initial catalog=MinionsDB;
integrated security=True;");

            connection.Open();

            using (connection)
            {

                while (true)
                {
                    Console.Write("Command: ");
                    var input = Console.ReadLine();

                    switch (input)
                    {
                        case "get v-names":
                            GetVillainsNames(connection);
                            break;
                        case "get vm-info":
                            GetVillainInfo(connection);
                            break;
                        case "exit":
                            return;
                        case "help":
                        default:
                            HelpInfo();
                            break;
                    }
                    Console.WriteLine();
                }
            }
        }



        private static void GetVillainInfo(SqlConnection connection)
        {
            int id = 0;
            while (true)
            {
                Console.Write("Villain's ID: ");
                var idString = Console.ReadLine();
                if(int.TryParse(idString, out id))
                {
                    break;
                }
                else
                {
                    Console.WriteLine($"{idString} is not a number. Please try again");
                }
            }
            SqlParameter idParam = new SqlParameter("@id", id);

            var queryVName = @"
SELECT [Name] FROM Villains
where [Id] = @id";
            SqlCommand cmdGetName = new SqlCommand(queryVName, connection);
            cmdGetName.Parameters.Add(idParam);
            var villainName = cmdGetName.ExecuteScalar();
            cmdGetName.Dispose();
            cmdGetName.Parameters.Remove(idParam);

            Console.WriteLine();
            Console.WriteLine($"Villain: {villainName}");

            var queryMinionsNames = @"
SELECT m.[Name], m.[Age] from Minions as m
join VillainsMinions as vm on m.[Id] = vm.[MinionId]
join Villains as v on vm.[VillainsId] = v.[Id]
where v.[Id] = @id";
            SqlCommand cmd = new SqlCommand(queryMinionsNames, connection);
            cmd.Parameters.Add(idParam);
            using (var result = cmd.ExecuteReader())
            {
                var counter = 1;
                while (result.Read())
                {
                    Console.WriteLine($"{counter}. {result["Name"]} {result["Age"]}");
                    counter++;
                }
            }
        }

        private static void GetVillainsNames(SqlConnection connection)
        {
            var query = @"
SELECT [Name], Count(vm.[MinionId]) as [Minions Count] FROM Villains as v
right join VillainsMinions as vm on v.[Id] = vm.[VillainsId]
group by v.[Name]";

            SqlCommand cmd = new SqlCommand(query, connection);

            using (var result = cmd.ExecuteReader())
            {
                while (result.Read())
                {
                    Console.WriteLine($"{result["Name"]} {result["Minions Count"]}");
                }
            }
        }

        private static void HelpInfo()
        {
            Console.WriteLine();
            Console.WriteLine("Possible commands are:");
            Console.WriteLine("\t1.Get Villains' Names: get v-names");
            Console.WriteLine("\t2.Get Minions Names: get vm-info");
            Console.WriteLine();
            Console.WriteLine("\tQuit app: exit");
        }

    }
}
