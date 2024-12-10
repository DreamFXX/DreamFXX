using Spectre.Console;
using System.Globalization;

class SubstanceUseApp
{
    private const string VIEW_SUBSTANCE_USE_LOGS = "View all Substance records.";
    private const string CREATE_SUBSTANCE_LOG = "Add a record about Substance use.";

    //private const string REPORT_ABOUT_RECORD = "Write a report about your mood, feelings and impacts on you.";
    //private const string CHANGE_SUBSTANCE_USE_LOGS = "Change records about Substance usage
    //private const string DELETE_RECORDS_OF_SUBSTANCE_USE = "Delete parts of already logged Substance usage.";

    private const string EXIT = "Close Application.";

    private DatabaseManager _databaseManager;
    public SubstanceUseApp(DatabaseManager databaseManager)
    {
        _databaseManager = databaseManager;
    }

    public void AppStart()
    {
        while (true)
        {
            MainMenu();
        }
    }

    private void MainMenu()
    {
        AnsiConsole.Clear();

        List<string> menuOperations = new List<string>
        {
            VIEW_SUBSTANCE_USE_LOGS,
            CREATE_SUBSTANCE_LOG,

            //CHANGE_SUBSTANCE_USE_LOGS,
            //REPORT_ABOUT_RECORD,
            //DELETE_RECORDS_OF_SUBSTANCE_USE,

            EXIT,
        };

        var menuSelection = AnsiConsole.Prompt(
            new SelectionPrompt<string>()
            .Title<string>("[underline][yellow]Substance Usage Tracker[/][/]\n -[grey]Log your consumption of mind-altering Substances![/]\n\n [underline yellow]--MAIN MENU--[/]")
            .PageSize(10)
            .MoreChoicesText("[grey](Move up and down with arrows to reveal more options.)[/]")
            .AddChoices(menuOperations)
            );
        try
        {
            MenuRouter(menuSelection);
        }
        catch (Exception e)
        {
            AnsiConsole.MarkupLine($"[red]Error: {e.Message}[/]");
            Console.ReadKey();
        }
    }

    private void MenuRouter(string menuSelection) // Začít zde!
    {
        switch (menuSelection)
        {
            case VIEW_SUBSTANCE_USE_LOGS:
                ViewSubstanceLogs();
                break;
            case CREATE_SUBSTANCE_LOG:
                CreateSubstanceLog();
                break;
            //case REPORT_ABOUT_RECORD:
            //    CreateReportOfRecord();
            //    break;
            //case CHANGE_SUBSTANCE_USE_RECORDS:
            //    ChangeSubstanceUseRecord();
            //    break;
            //case DELETE_RECORDS_OF_SUBSTANCE_USE:
            //    DeleteSubstanceUseRecords();
            //    break;
            case EXIT:
                AnsiConsole.MarkupLine("Stay safe!");
                Environment.Exit(0);
                break;
        }
    }

    private void ViewSubstanceLogs()
    {
        List<SubstanceType> substanceTypes = _databaseManager.GetSubstancesList();
        AnsiConsole.Clear(); //?


        DisplaySubstanceTypes(substanceTypes);
        AnsiConsole.MarkupLine("[yellow]Press any key to continue..[/]");
        Console.ReadKey();
    }

    private void DisplaySubstanceTypes(List<SubstanceType> substanceTypes)
    {
        Table substanceLogsTable = new Table();
        substanceLogsTable.Title(new TableTitle("Coding Sessions"));
        substanceLogsTable.AddColumns("Id", "Substance", "Dosage in Units", "Date and Time");
        substanceLogsTable.Columns[0].Width = 5;

        foreach (SubstanceType substanceType in substanceTypes)
        {
            Markup[] columns =
            [
                new Markup(substanceType.Id.ToString()),
                new Markup(substanceType.Substance),
                new Markup($"{substanceType.Dosage.ToString() }{substanceType.Unit}"),
                new Markup(substanceType.DateTime.ToString())
            ];
            substanceLogsTable.AddRow(columns);
        }
        AnsiConsole.Write(substanceLogsTable);
        AnsiConsole.MarkupLine("[green]Substances logs and their data List.[/]");
    }


    private void CreateSubstanceLog()
    {
        AnsiConsole.Clear();
        AnsiConsole.Markup("[bold yellow]You are creating your Substance Log![/]\n[green]Congratulations to your new healthier life.[/]");
        AnsiConsole.Markup("[yellow]\n\nNow, please enter specific information about your Substance Usage.[/]");

        string substance_Type = AnsiConsole.Prompt(new TextPrompt<string>("[purple]\nWhat Substance you want to log? Please, enter the Substance now.[/]"));
        double dosageInput = AnsiConsole.Prompt(new TextPrompt<double>("[red]\nWhat amount was your last dosage? Please type in just number, Units will be set soon.[/]"));
        string unitInput = AnsiConsole.Prompt(new TextPrompt<string>("[white]\nEnter a short symbol (ml, l, mg, g etc.) to specify units, which will be used to display your dosages[/]"));

        string dateTimeString = AnsiConsole.Prompt(new TextPrompt<string>("[white]\nType in the Date and Time of your last dosage in format -> dd-MM HH:mm[/]")
            .ValidationErrorMessage("[red]Invalid date format. Please enter a valid date and time in the format 'dd-MM HH:mm'[/]")
            .Validate((string input) =>
            {
                try
                {
                    DateTime.ParseExact(input, "dd-MM HH:mm", CultureInfo.InvariantCulture);
                    return true;
                }
                catch
                {
                    return false;
                }
            }));

        DateTime dateTime = DateTime.ParseExact(dateTimeString, "dd-MM HH:mm", CultureInfo.InvariantCulture);

        SubstanceType substanceType = new SubstanceType
        {
            Substance = substance_Type,
            Dosage = dosageInput,
            Unit = unitInput,
            DateTime = dateTime,
        };

        _databaseManager.CreateSubstancesLog(substanceType);
        AnsiConsole.MarkupLine("[red]YAY![/] [green]Congratulations on your new logged substance habit! Entered substance type log table sucessfully created.[/]");
    }
}