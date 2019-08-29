using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace ChangeTownNameCasing
{
    class Program
    {
        static void Main(string[] args)
        {
            var country = Console.ReadLine();

            var connection = new SqlConnection(@"Server=.\SQLEXPRESS;Database=MinionsDB;Integrated Security=true");

            connection.Open();

            var towns = new List<string>();

            using(connection)
            {
                var renameTowns = new SqlCommand("UPDATE Towns " +
                    "SET Name = UPPER(Name) " +
                    "WHERE CountryCode = " +
                    "(SELECT Id FROM Countries WHERE Name = @country)", connection);

                renameTowns.Parameters.AddWithValue("@country", country);

                renameTowns.ExecuteNonQuery();

                var showTowns = new SqlCommand("SELECT Name FROM Towns WHERE " +
                    "CountryCode = (SELECT Id FROM Countries WHERE Name = @country)", connection);

                showTowns.Parameters.AddWithValue("@country", country);

                var townsReader = showTowns.ExecuteReader();

                while (townsReader.Read())
                {
                    towns.Add(townsReader["Name"].ToString());
                }

                townsReader.Close();
            }
            connection.Close();

            if (towns.Count == 0)
            {
                Console.WriteLine("No town names were affected.");
            }

            else
            {
                Console.WriteLine($"{towns.Count} town names were affected.");

                Console.WriteLine($"[{string.Join(", ", towns)}]");
            }
        }
    }
}
