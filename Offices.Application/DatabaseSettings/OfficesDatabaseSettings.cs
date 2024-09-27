namespace Offices.Application.DatabaseSettings;

public class OfficesDatabaseSettings : IOfficesDatabaseSettings
{
    public string OfficeCollectionName { get; set; } = String.Empty;
    public string PhotoCollectionName { get; set; } = String.Empty;
    public string ConnectionString { get; set; } = String.Empty;
    public string OfficeDatabaseName { get; set; } = String.Empty;
}

