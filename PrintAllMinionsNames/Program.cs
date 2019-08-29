using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace PrintAllMinionsNames
{
    class Program
    {
        static void Main(string[] args)
        {
            var connection = new SqlConnection(@"Server=.\SQLEXPRESS;Database=MinionsDB;Integrated Security=true");

            connection.Open();

            var names = new List<string>();

            using(connection)
            {
                var command = new SqlCommand("SELECT Name FROM Minions", connection);

                var reader = command.ExecuteReader();

                while (reader.Read())
                {
                    names.Add(reader["Name"].ToString());
                }

                reader.Close();
            }

            connection.Close();

            var j = 0;

            for (int i = 0; i < names.Count; i++)
            {
                if (i % 2 == 0)
                {
                    Console.WriteLine(names[j]);
                }

                else
                {
                    Console.WriteLine(names[names.Count - 1 - j]);

                    j++;
                }
            }
        }
    }
}
