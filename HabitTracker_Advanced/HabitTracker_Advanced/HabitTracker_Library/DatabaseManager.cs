using System.Data;
using System.Data.SQLite;
using System.Configuration;

namespace HabitTracker_Advanced.HabitTracker_Library;

public class DatabaseManager
{
    private static readonly string connectionString = LoadCnnString();
    public static List<string> currentHabits = [];


    private static string LoadCnnString(string id = "Default")
    {
        return ConfigurationManager.ConnectionStrings[id].ConnectionString;

        //// Build a configuration object from JSON file
        //IConfiguration config = new ConfigurationBuilder()
        //    .AddJsonFile("appsettings.json")
        //    .Build();

        //// Get a configuration section
        //IConfigurationSection section = config.GetSection("ConnectionStrings");

        //// Read simple values
        //string? connectionString = section.GetConnectionString("DefaultConnection");

        //return connectionString;
    }

}

