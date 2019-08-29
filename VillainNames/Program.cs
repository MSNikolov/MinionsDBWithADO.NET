using System;
using System.Data.SqlClient;

namespace VillainNames
{
    class Program
    {
        static void Main(string[] args)
        {
            var connection = new SqlConnection(@"Server=.\SQLEXPRESS;Database=MinionsDB;Integrated Security=true");

            connection.Open();

            using (connection)
            {
                var command = new SqlCommand(@"SELECT v.Name, COUNT(mv.MinionId) FROM " +
                    "Villains AS v " +
                    "JOIN MinionsVillains AS mv ON mv.VillainId = v.Id " +
                    "JOIN Minions AS m ON m.Id=mv.MinionId " +
                    "GROUP BY v.Name " +
                    "HAVING COUNT (mv.MinionId) > 3 " +
                    "ORDER BY COUNT (mv.MinionId) ASC", connection);


                var reader = command.ExecuteReader();

                while (reader.Read())
                {
                    Console.Write("Villain Name: " + (string)reader["Name"] +" ");
                    Console.WriteLine("Minions count: " + (int)reader[1]);
                }
            }
            connection.Close();
        }
    }
}
