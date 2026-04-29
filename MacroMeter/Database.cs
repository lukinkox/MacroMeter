using Microsoft.Data.Sqlite;
using System.IO;

namespace MacroMeter
{
    public static class Database
    {
        private static string dbPath =
    Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "users.db");

        public static void Initialize()
        {
            if (!File.Exists(dbPath))
            {
                using (var connection = new SqliteConnection($"Data Source={dbPath}"))
                {
                    connection.Open();

                    var command = connection.CreateCommand();
                    command.CommandText =
                    @"CREATE TABLE Users (
                        Id INTEGER PRIMARY KEY AUTOINCREMENT,
                        Meno TEXT,
                        Priezvisko TEXT,
                        Email TEXT UNIQUE,
                        Password TEXT
                    );";

                    command.ExecuteNonQuery();
                }
            }
        }

        public static void SaveUser(User user)
        {

            using (var connection = new SqliteConnection($"Data Source={dbPath}"))
            {
                connection.Open();

                var command = connection.CreateCommand();
                command.CommandText =
                @"INSERT INTO Users (Meno, Priezvisko, Email, Password, Vaha, CielovaVaha, Vek, Vyska, Pohlavie, Aktivita, Ciel)
                  VALUES ($meno, $priezvisko, $email, $password, $vaha, $cielovaVaha, $vek, $vyska, $pohlavie, $aktivita, $ciel)";

                command.Parameters.AddWithValue("$meno", user.Meno);
                command.Parameters.AddWithValue("$priezvisko", user.Priezvisko);
                command.Parameters.AddWithValue("$email", user.Email);
                command.Parameters.AddWithValue("$password", user.Password);
                command.Parameters.AddWithValue("$vaha", user.Vaha);
                command.Parameters.AddWithValue("$cielovaVaha", user.CielovaVaha);
                command.Parameters.AddWithValue("$vek", user.Vek);
                command.Parameters.AddWithValue("$vyska", user.Vyska);
                command.Parameters.AddWithValue("$pohlavie", user.Pohlavie);
                command.Parameters.AddWithValue("$aktivita", user.Aktivita);
                command.Parameters.AddWithValue("$ciel", user.Ciel);

                command.ExecuteNonQuery();
            }
        }

        public static User GetUser(string email, string password)
        {
            using (var connection = new SqliteConnection($"Data Source={dbPath}"))
            {
                connection.Open();

                var command = connection.CreateCommand();
                command.CommandText =
                @"SELECT * FROM Users 
                  WHERE Email = $email AND Password = $password";

                command.Parameters.AddWithValue("$email", email);
                command.Parameters.AddWithValue("$password", password);

                using (var reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        return new User
                        {
                            Meno = reader["Meno"].ToString(),
                            Priezvisko = reader["Priezvisko"].ToString(),
                            Email = reader["Email"].ToString(),
                            Vaha = Convert.ToInt32(reader["Vaha"]),
                            CielovaVaha = Convert.ToInt32(reader["CielovaVaha"]),
                            Vek = Convert.ToInt32(reader["Vek"]),
                            Vyska = Convert.ToInt32(reader["Vyska"])
                        };
                    }
                }
            }

            return null;
        }
    }
}