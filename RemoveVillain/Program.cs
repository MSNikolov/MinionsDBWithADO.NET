using System;
using System.Data.SqlClient;

namespace RemoveVillain
{
    class Program
    {
        static void Main(string[] args)
        {
            var connection = new SqlConnection(@"Server=.\SQLEXPRESS;Database=MinionsDB;Integrated Security=true");

            var villainId = int.Parse(Console.ReadLine());

            connection.Open();

            using (connection)
            {
                var checkVillain = new SqlCommand("SELECT Id FROM Villains WHERE Id=@villainId", connection);

                checkVillain.Parameters.AddWithValue("@villainId", villainId);

                if (checkVillain.ExecuteScalar() == null)
                {
                    Console.WriteLine("No such villain was found.");
                }

                else
                {
                    var villainNameGetter = new SqlCommand("SELECT Name FROM Villains WHERE Id = @villainId", connection);

                    villainNameGetter.Parameters.AddWithValue("@villainId", villainId);

                    var villainName = villainNameGetter.ExecuteScalar().ToString();

                    var minionsCountGetter = new SqlCommand("SELECT COUNT (*) FROM MinionsVillains WHERE VillainId=@villainId", connection);

                    minionsCountGetter.Parameters.AddWithValue("@villainId", villainId);

                    var minionsCount = (int)minionsCountGetter.ExecuteScalar();

                    var deleteMinionsVillains = new SqlCommand("DELETE FROM MinionsVillains " +
                        "WHERE VillainId = @villainId", connection);

                    deleteMinionsVillains.Parameters.AddWithValue("@villainId", villainId);

                    deleteMinionsVillains.ExecuteNonQuery();

                    var deleteVillain = new SqlCommand("DELETE FROM Villains " +
                        "WHERE Id = @villainId", connection);

                    deleteVillain.Parameters.AddWithValue("@villainId", villainId);

                    deleteVillain.ExecuteNonQuery();

                    Console.WriteLine($"{villainName} was deleted.");

                    Console.WriteLine($"{minionsCount} minions were released.");
                }
            }

            connection.Close();
        }
    }
}
