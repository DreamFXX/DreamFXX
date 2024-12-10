using System.Data.SQLite;
using Dapper;
using System.Data;

class DatabaseManager
{
    private string _connectionString = "";

    public DatabaseManager(string connectionString)
    {
            _connectionString = connectionString;
            SqlMapper.AddTypeHandler(new DateTimeHandler());

        using (SQLiteConnection connection = new SQLiteConnection(_connectionString))
        {
            connection.Execute("CREATE TABLE IF NOT EXISTS Substances_Data (Id INTEGER PRIMARY KEY AUTOINCREMENT, Substance TEXT, Dosage REAL, Unit TEXT, DateTime TEXT)");
        }
    }

    public void CreateSubstancesLog(SubstanceType substanceType)
    {
        using (SQLiteConnection connection = new SQLiteConnection(_connectionString))
        {
            string sql = "INSERT INTO Substances_Data (Id, Substance, Dosage, Unit, DateTime) VALUES (@Id, @Substance, @Dosage, @Unit, @DateTime)";
            connection.Execute(sql, substanceType);
        }
    }

    public SubstanceType GetSubstanceType(string substance) // int id? want to search by substanceType!
    {
        using (SQLiteConnection connection = new SQLiteConnection(_connectionString))
        {
            string sql = "SELECT * FROM Substances_Data WHERE Substance = @substance";

            SubstanceType substanceType = connection.QueryFirstOrDefault<SubstanceType>(sql, new { Substance = substance });
            // SubstanceType substanceType = connection.QueryFirstOrDefault<SubstanceType>(sql, new {SubstanceType = substanceTypeFind });
            if (substanceType == null)
            {
                throw new Exception("Coding session not found");
            }
            return substanceType;
        }
    }

    public List<SubstanceType> GetSubstancesList()
    {
        using(SQLiteConnection connection = new SQLiteConnection(_connectionString))
        {
            string sql = "SELECT * FROM Substances_Data ORDER BY Id DESC";
            return connection.Query<SubstanceType>(sql).ToList();
        }
    }
}

class DateTimeHandler : SqlMapper.TypeHandler<DateTimeOffset>
{
    private readonly TimeZoneInfo databaseTimeZone = TimeZoneInfo.Local;
    public static readonly DateTimeHandler Default = new DateTimeHandler();

    public DateTimeHandler()
    {

        SqlMapper.RemoveTypeMap(typeof(DateTimeOffset));
        SqlMapper.AddTypeHandler(DateTimeHandler.Default);
    }

    public override DateTimeOffset Parse(object value)
    {
        DateTime storedDateTime;
        if (value == null)
            storedDateTime = DateTime.MinValue;
        else
            storedDateTime = (DateTime)value;

        if (storedDateTime.ToUniversalTime() <= DateTimeOffset.MinValue.UtcDateTime)
            return DateTimeOffset.MinValue;
        else
            return new DateTimeOffset(storedDateTime, databaseTimeZone.BaseUtcOffset);
    }

    public override void SetValue(IDbDataParameter parameter, DateTimeOffset value)
    {
        DateTime paramVal = value.ToOffset(this.databaseTimeZone.BaseUtcOffset).DateTime;
        parameter.Value = paramVal;
    }
}