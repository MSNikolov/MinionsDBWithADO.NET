using System;
using System.Data.SqlClient;

namespace AddMinion
{
    class Program
    {
        static void Main(string[] args)
        {
            var minion = Console.ReadLine().Split();

            var minionName = minion[1];

            var minionAge = int.Parse(minion[2]);

            var minionTown = minion[3];

            var villain = Console.ReadLine().Split();

            var villainName = villain[1];

            var connection = new SqlConnection(@"Server=.\SQLEXPRESS;Database=MinionsDB;Integrated Security=true");

            connection.Open();

            using (connection)
            {
                var checkTown = new SqlCommand("SELECT Id FROM Towns WHERE Name=@minionTown", connection);

                checkTown.Parameters.AddWithValue("@minionTown", minionTown);

                if (checkTown.ExecuteScalar() == null)
                {
                    var addTown = new SqlCommand("INSERT INTO Towns (Name, CountryCode) " +
                        "VALUES (@minionTown, 'BG')", connection);

                    addTown.Parameters.AddWithValue("@minionTown", minionTown);

                    addTown.ExecuteNonQuery();

                    Console.WriteLine($"Town {minionTown} was added to the database.");
                }

                var checkVillain = new SqlCommand("SELECT Id FROM Villains WHERE Name = @villainName", connection);

                checkVillain.Parameters.AddWithValue("@villainName", villainName);

                if (checkVillain.ExecuteScalar() == null)
                {
                    var addVillain = new SqlCommand("INSERT INTO Villains (Name, EvilnessFactorId) " +
                        "VALUES (@villainName, 3)", connection);

                    addVillain.Parameters.AddWithValue("@villainName", villainName);

                    addVillain.ExecuteNonQuery();

                    Console.WriteLine($"Villain {villainName} was added to the database.");
                }

                int townId = (int)checkTown.ExecuteScalar();

                int villainId = (int)checkVillain.ExecuteScalar();

                var addMinion = new SqlCommand("INSERT INTO Minions (Name, Age, TownId) " +
                    "VALUES (@minionName, @minionAge, (SELECT Id FROM Towns WHERE Name=@townName)) " +
                    "INSERT INTO MinionsVillains (MinionId, VillainId) " +
                    "VALUES ((SELECT Id FROM Minions WHERE Name=@minionName), (SELECT Id FROM Villains WHERE Name=@villainName))", connection);

                addMinion.Parameters.AddWithValue("@minionName", minionName);

                addMinion.Parameters.AddWithValue("@minionAge", minionAge);

                addMinion.Parameters.AddWithValue("@townName", minionTown);

                addMinion.Parameters.AddWithValue("@villainName", villainName);

                addMinion.ExecuteNonQuery();

                Console.WriteLine($"Successfully added {minionName} to be minion of {villainName}.");
            }

            connection.Close();
        }
    }
}
