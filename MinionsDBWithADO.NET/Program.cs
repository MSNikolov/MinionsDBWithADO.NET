using System;
using System.Data.SqlClient;

namespace MinionsDBWithADO.NET
{
    class Program
    {
        static void Main(string[] args)
        {
            var connection = new SqlConnection(@"Server=.\SQLEXPRESS;Database=master;Integrated Security=true");

            connection.Open();

            using (connection)
            {               

                var command = new SqlCommand("CREATE DATABASE MinionsDB", connection);

                command.ExecuteNonQuery();

                command = new SqlCommand("USE MinionsDB", connection);

                command.ExecuteNonQuery();

                command = new SqlCommand("CREATE TABLE Countries (Id CHAR(2) PRIMARY KEY NOT NULL, Name VARCHAR (30) NOT NULL)" +
                    "CREATE TABLE Towns (Id INT PRIMARY KEY IDENTITY, Name VARCHAR (30) NOT NULL, CountryCode CHAR(2) NOT NULL, CONSTRAINT FK_Country FOREIGN KEY (CountryCode) REFERENCES Countries(Id))" +
                    "CREATE TABLE Minions (Id INT PRIMARY KEY IDENTITY, Name VARCHAR (30) NOT NULL, Age INT NOT NULL, TownId INT NOT NULL, CONSTRAINT FK_Town FOREIGN KEY (TownId) REFERENCES Towns(Id))" +
                    "CREATE TABLE EvilnessFactors (Id INT PRIMARY KEY IDENTITY, Name VARCHAR(30) NOT NULL)" +
                    "CREATE TABLE Villains (Id INT PRIMARY KEY IDENTITY, Name VARCHAR (30) NOT NULL, EvilnessFactorId INT NOT NULL, CONSTRAINT FK_Evilness FOREIGN KEY (EvilnessFactorId) REFERENCES EvilnessFactors(Id))" +
                    "CREATE TABLE MinionsVillains (MinionId INT NOT NULL, VillainId INT NOT NULL, CONSTRAINT FK_Minion FOREIGN KEY (MinionId) REFERENCES Minions(Id), CONSTRAINT FK_Villain FOREIGN KEY (VillainId) REFERENCES Villains (Id), CONSTRAINT PK_Vill_Min PRIMARY KEY (MinionId, VillainId))", connection);

                command.ExecuteNonQuery();

                command = new SqlCommand("INSERT INTO Countries (Id, Name)" +
                    "VALUES ('BG', 'Bulgaria')," +
                    "('IS', 'Island')," +
                    "('RO', 'Romania')," +
                    "('GR', 'Greece')," +
                    "('DE', 'Deutschland')", connection);

                command.ExecuteNonQuery();

                command = new SqlCommand("INSERT INTO Towns (Name, CountryCode)" +
                    "VALUES ('Sofia', 'BG')," +
                    "('Putkintown', 'IS')," +
                    "('Kludge', 'RO')," +
                    "('Atina', 'GR')," +
                    "('Berlin', 'DE')", connection);

                command.ExecuteNonQuery();

                command = new SqlCommand("INSERT INTO Minions (Name, Age, TownId)" +
                    "VALUES ('Pesho', 18, 1)," +
                    "('Gancho', 23, 3)," +
                    "('Gosho', 21, 2)," +
                    "('Amatiorex', 20, 4)," +
                    "('Kurax', 22, 5)", connection);

                command.ExecuteNonQuery();

                command = new SqlCommand("INSERT INTO EvilnessFactors (Name)" +
                    "VALUES ('super good')," +
                    "('good')," +
                    "('bad')," +
                    "('evil')," +
                    "('super evil')", connection);

                command.ExecuteNonQuery();

                command = new SqlCommand("INSERT INTO Villains (Name, EvilnessFactorId)" +
                    "VALUES ('Osman', 1)," +
                    "('Blagoi', 2)," +
                    "('Kurcho', 3)," +
                    "('Pedalex', 4)," +
                    "('Ugrin', 5)", connection);

                command.ExecuteNonQuery();

                command = new SqlCommand("INSERT INTO MinionsVillains (MinionId, VillainId)" +
                    "VALUES (1, 1)," +
                    "(3, 4)," +
                    "(4, 3)," +
                    "(1, 2)," +
                    "(1, 3)," +
                    "(3, 5)," +
                    "(5, 2)," +
                    "(4, 2)," +
                    "(5, 3)," +
                    "(2, 3)," +
                    "(3, 3)," +
                    "(2, 4)", connection);

                command.ExecuteNonQuery();
            }
            connection.Close();
        }
    }
}
