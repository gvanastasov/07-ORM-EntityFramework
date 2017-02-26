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
                        case "add m":
                            CreateNewMinion(connection);
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

        private static void CreateNewMinion(SqlConnection connection)
        {
            Console.WriteLine();

            Console.Write("Minion: ");
            var minionData = Console.ReadLine().Split(' ');

            Console.Write("Villain: ");
            var villainName = Console.ReadLine();

            Console.WriteLine();

            try
            {
                SqlParameter[] queryParams = new SqlParameter[] {
                    new SqlParameter("@mname", minionData[0]),
                    new SqlParameter("@mage", int.Parse(minionData[1])),
                    new SqlParameter("@mtown", minionData[2]),
                    new SqlParameter("@vname", villainName),
                };

                SqlCommand cmd = connection.CreateCommand();
                cmd.Parameters.AddRange(queryParams);

                SqlTransaction transaction = connection.BeginTransaction("Adding Minion");
                cmd.Transaction = transaction;

                try
                {
                    // get town id, if not existing add new one and select it
                    cmd.CommandText = "SELECT [Id] FROM Towns Where [Name]=@mtown";
                    var townId = cmd.ExecuteScalar();
                    if (townId == null)
                    {
                        cmd.CommandText = "INSERT INTO Towns OUTPUT Inserted.[Id] VALUES (@mtown, 'BGR')";
                        townId = cmd.ExecuteScalar();
                        Console.WriteLine($"Town {minionData[2]} was added to the database.");
                    }

                    // add new minion
                    cmd.CommandText = $"INSERT INTO Minions OUTPUT Inserted.[Id] VALUES (@mname, @mage, {(int)townId})";
                    var mId = cmd.ExecuteScalar();

                    // get villain id, if not existing add new one and select it
                    cmd.CommandText = $"SELECT [Id] FROM Villains WHERE [Name]=@vname";
                    var vId = cmd.ExecuteScalar();
                    if(vId == null)
                    {
                        cmd.CommandText = "INSERT INTO Villains OUTPUT Inserted.[Id] VALUES (@vname, 3)";
                        vId = cmd.ExecuteScalar();
                        Console.WriteLine($"Villain {villainName} was added to the database.");
                    }

                    // add reference between new minion and (new) villain
                    cmd.CommandText = $"INSERT INTO VillainsMinions VALUES ({(int)mId}, {(int)vId})";
                    cmd.ExecuteNonQuery();

                    // dispose all unneeded resources
                    transaction.Commit();
                    transaction.Dispose();

                    cmd.Parameters.Clear();
                    cmd.Dispose();

                    Console.WriteLine($"Successfully added {minionData[0]} to be minion of {villainName}.");
                }
                catch (Exception e)
                {
                    Console.WriteLine($"Something went wrong, reverting DB. {e.GetType()}: {e.Message}");
                    transaction.Rollback();
                }
            }
            catch
            {
                Console.WriteLine("Wrong input data format. Please use: {string} {int} {string}");
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
            Console.WriteLine("\t3.Add minion: add m");
            Console.WriteLine();
            Console.WriteLine("\tQuit app: exit");
        }

    }
}
