namespace Offices.Application.DatabaseSettings;

public class OfficesDatabaseSettings : IOfficesDatabaseSettings
{
    public string DatabaseCollectionName { get; set; } = String.Empty;
    public string ConnectionString { get; set; } = String.Empty;
    public string DatabaseName { get; set; } = String.Empty;
}
