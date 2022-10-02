namespace Database.Common;

#pragma warning disable CS8618
public class DatabaseOptions
{
    public bool UseInMemoryDatabase { get; set; }
    public string ConnectionString { get; set; }
    public bool EnableAutoMigration { get; set; }
}