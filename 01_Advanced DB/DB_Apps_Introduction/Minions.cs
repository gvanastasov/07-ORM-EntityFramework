using System;
using System.Collections.Generic;
using System.Data;
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
                        case "t casing":
                            ChangeTownNamesCasing(connection);
                            break;
                        case "v del":
                            DeleteVillain(connection);
                            break;
                        case "fl order":
                            PrintAllMinionNames(connection);
                            break;
                        case "m age inc":
                            IncreaseMinionsAge(connection);
                            break;
                        case "m age proc":
                            IncreaseAgeProcedure(connection);
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

        private static void IncreaseAgeProcedure(SqlConnection connection)
        {
            Console.WriteLine();

            try
            {
                Console.Write("Minions Id: ");
                var targetId = int.Parse(Console.ReadLine());

                using (SqlCommand cmd = new SqlCommand("usp_GetOlder", connection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@MinionId", targetId);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Console.WriteLine($"{reader["Name"]} got old and now is - {reader["Age"]}");
                        }
                    }
                }

            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        private static void IncreaseMinionsAge(SqlConnection connection)
        {
            Console.WriteLine();
            Console.Write("Minions Id`s: ");

            try
            {
                var targets = Console.ReadLine().Split(' ').Select(int.Parse).ToArray();

                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = connection;

                    // todo: solve this table type
                    var parameters = new string[targets.Length];
                    for (int i = 0; i < targets.Length; i++)
                    {
                        parameters[i] = $"@Id{i}";
                        cmd.Parameters.AddWithValue(parameters[i], targets[i]);
                    }

                    cmd.CommandText = $@"
update Minions set [Age] += 1
where [Id] in ({string.Join(", ", parameters)})";

                    cmd.ExecuteNonQuery();

                    cmd.CommandText = @"select [Id], [Name], [Age] from Minions";

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var name = reader["Name"].ToString();
                            if(targets.Contains((int)reader["Id"]))
                            {
                                name = string.Join(" ",name
                                    .Split(' ')
                                    .ToList()
                                    .Select(s => s.First().ToString().ToUpper() + s.Substring(1))
                                );
                            }
                            else
                            {
                                name = name.ToLower();
                            }

                            Console.WriteLine($"{name} {reader["Age"]}");
                        }
                    }
                }


            }
            catch(Exception e)
            {
                Console.WriteLine("Invalid input. Please use {int} values separated by space : " + e);
            }


        }

        private static void PrintAllMinionNames(SqlConnection connection)
        {
            Console.WriteLine();

            var query = "select [Name] from Minions";
            using (SqlCommand cmd = new SqlCommand(query, connection))
            using (SqlDataReader reader = cmd.ExecuteReader())
            {
                List<string> names = new List<string>();

                while (reader.Read())
                {
                    var value = reader["Name"].ToString();
                    names.Add(value);
                }

                bool fromStart = true;
                while (names.Count > 0)
                {
                    string value;
                    if(fromStart)
                    {
                        value = names[0];
                        names.RemoveAt(0);
                    }
                    else
                    {
                        value = names[names.Count - 1];
                        names.RemoveAt(names.Count - 1);
                    }
                    Console.WriteLine(value);
                    fromStart = !fromStart;
                }
            }
        }

        private static void DeleteVillain(SqlConnection connection)
        {
            Console.WriteLine();

            Console.Write("Villain Id: ");
            var villainIdString = Console.ReadLine();

            try
            {
                var vId = new SqlParameter("@vid", int.Parse(villainIdString));

                SqlCommand cmd = connection.CreateCommand();
                cmd.Parameters.Add(vId);

                SqlTransaction transaction = connection.BeginTransaction("Deleting_Villain");
                cmd.Transaction = transaction;

                try
                {
                    cmd.CommandText = "SELECT [Name] FROM Villains WHERE [Id]=@vid";
                    var vName = cmd.ExecuteScalar();

                    if(vName != null)
                    {
                        // release minions first, because of the FK constraint
                        cmd.CommandText = "Delete from VillainsMinions where [VillainsId] = @vid";
                        var affectedCount = cmd.ExecuteNonQuery();

                        // delete actual villain
                        cmd.CommandText = "delete from Villains where [Id] = @vid";

                        Console.WriteLine($"{vName} was deleted");
                        Console.WriteLine($"{affectedCount} minions released");

                        // release program resources
                        transaction.Commit();

                        transaction.Dispose();

                        cmd.Parameters.Clear();
                        cmd.Dispose();
                    }
                    else
                    {
                        Console.WriteLine("No such villain was found");
                    }

                }
                catch (Exception e)
                {
                    Console.WriteLine($"Something went wrong, reverting DB. {e.GetType()}: {e.Message}");
                    transaction.Rollback();
                }
            }
            catch 
            {
                Console.WriteLine("Wrong input data format. Please use: {int}");
            }
        }

        private static void ChangeTownNamesCasing(SqlConnection connection)
        {
            Console.WriteLine();

            Console.Write("Country: ");
            var countryName = Console.ReadLine();

            Console.WriteLine();

            SqlParameter countryNameParam = new SqlParameter("@cname", countryName.ToString());
            var query = @"
update Towns
set [Name]=UPPER([Name])
OUTPUT INSERTED.[Name]
where [ContryCode] = (select [Code] 
					from Countries 
					where [Name] = @cname)
  AND [Name] <> UPPER([Name])";

            using (SqlCommand updateCmd = new SqlCommand(query, connection))
            {
                updateCmd.Parameters.Add(countryNameParam);
                SqlDataReader result = updateCmd.ExecuteReader();

                using (result)
                {
                    var affected = new List<string>();
                    while (result.Read())
                    {
                        affected.Add(result["Name"].ToString());
                    }

                    if(affected.Count > 0)
                    {
                        Console.WriteLine($"{affected.Count} town name{((affected.Count > 1) ? "s were" : " was")} affected.");
                        Console.WriteLine($"[{string.Join(", ", affected)}]");
                    }
                    else
                    {
                        Console.WriteLine("No town names were affected.");
                    }
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
            Console.WriteLine("\t4.Change Town Names Casing: t casing");
            Console.WriteLine("\t5.Delete Villain: v del");
            Console.WriteLine();
            Console.WriteLine("\tQuit app: exit");
        }

    }
}
