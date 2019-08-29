using System;
using System.Data.SqlClient;

namespace IncreaseAgeStoredProcedure
{
    class Program
    {
        static void Main(string[] args)
        {
            var id = int.Parse(Console.ReadLine());

            var connection = new SqlConnection(@"Server=.\SQLEXPRESS;Database=MinionsDB;Integrated Security=true");

            connection.Open();

            using (connection)
            {
                var command = new SqlCommand("EXEC usp_GetOlder @Id", connection);

                command.Parameters.AddWithValue("@Id", id);

                command.ExecuteNonQuery();

                var print = new SqlCommand("SELECT Name, Age FROM Minions WHERE Id = @Id", connection);

                print.Parameters.AddWithValue("@Id", id);

                var reader = print.ExecuteReader();

                while (reader.Read())
                {
                    var name = reader["Name"].ToString();

                    var age = (int)reader["Age"];

                    Console.WriteLine($"{name} – {age} years old");
                }
            }

            connection.Close();
        }
    }
}
