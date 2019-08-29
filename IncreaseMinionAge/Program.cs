using System;
using System.Data.SqlClient;
using System.Linq;

namespace IncreaseMinionAge
{
    class Program
    {
        static void Main(string[] args)
        {
            var connection = new SqlConnection(@"Server=.\SQLEXPRESS;Database=MinionsDB;Integrated Security=true");

            var ids = Console.ReadLine().Split().Select(int.Parse).ToList();

            connection.Open();

            using (connection)
            {
                for (int i = 0; i < ids.Count; i++)
                {
                    var command = new SqlCommand("UPDATE Minions " +
                        "SET Age+= 1 " +
                        "WHERE Id = @Id " +
                        "UPDATE Minions " +
                        "SET Name = UPPER(LEFT(Name, 1)) + LOWER(RIGHT(Name, LEN(Name)-1)) " +
                        "WHERE Id = @Id", connection);

                    command.Parameters.AddWithValue("@Id", ids[i]);

                    command.ExecuteNonQuery();
                }

                var readData = new SqlCommand("SELECT Name, Age FROM Minions", connection);

                var reader = readData.ExecuteReader();

                while (reader.Read())
                {
                    Console.WriteLine(reader["Name"].ToString() + " " + reader["Age"].ToString());
                }
            }
        }
    }
}
