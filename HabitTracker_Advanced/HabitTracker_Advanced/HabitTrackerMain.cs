using HabitTracker_Advanced.HabitTracker_Library;

namespace HabitTracker_Advanced;

internal class HabitTrackerMain
{
    static DatabaseManager dbManager = new DatabaseManager();

    private static bool exit;

    private static void Main(string[] args)
    {
        while (!exit)
        {
            Console.WriteLine("\n-- WELCOME IN HABIT TRACKER ADVANCED --\n");
            dbManager.CreateDbIfNotExists();
            ShowMenu();
            string menuChoice = Console.ReadLine();
            //SwitchMenuChoice(menuChoice);
        }
    }

    private static void ShowMenu()
    {
        Console.WriteLine("Please enter one of the following options:\n" +
                          "===============================\n" +
                          "\t'c' - Create a habit\n" +
                          "\t'v' - View habit\n" +
                          "\t'u' - Update habit\n" +
                          "\t'i' - Insert into habit\n" +
                          "\t'd' - Delete from habit\n" +
                          "\t'r' - Create a report\n" +
                          "===============================\n" +
                          "'exit' - Close the application\n" +
                          "===============================\n" +
                          "Your choice: \n");
    }
}


