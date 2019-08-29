using System;
using System.Data.SqlClient;

namespace MinionNames
{
    class Program
    {
        static void Main(string[] args)
        {
            var villId = int.Parse(Console.ReadLine());

            var connection = new SqlConnection(@"Server=.\SQLEXPRESS;Database=MinionsDB;Integrated Security=true");

            connection.Open();

            using(connection)
            {
                var command = new SqlCommand("SELECT m.Name AS [MinionName], m.Age AS [Age] FROM MinionsVillains AS mv " +
                    "JOIN Minions AS m ON mv.MinionId=m.Id " +
                    "WHERE mv.VillainId=@VillId", connection);

                command.Parameters.AddWithValue("@VillId", villId);

                var commandT = new SqlCommand("SELECT Name FROM Villains WHERE Id=@VillId", connection);

                commandT.Parameters.AddWithValue("@VillId", villId);

                var villainReader = commandT.ExecuteReader();

                while(villainReader.Read())
                {
                    Console.WriteLine($"Villain: {villainReader["Name"]}");
                }

                villainReader.Close();

                var minionReader = command.ExecuteReader();

                var count = 0;

                while (minionReader.Read())
                {
                    count++;

                    Console.WriteLine($"{count}. {minionReader["MinionName"]} {minionReader["Age"]}");
                }

                minionReader.Close();

                if (count == 0)
                {
                    Console.WriteLine($"No villain with ID {villId} exists in the database.");
                }
            }

            connection.Close();
        }
    }
}
