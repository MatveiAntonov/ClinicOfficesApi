namespace Offices.WebApi.DatabaseSettings;

public class OfficesDatabaseSettings : IOfficesDatabaseSettings
{
    public string CoursesCollectionName { get; set; } = String.Empty;
    public string ConnectionString { get; set; } = String.Empty;
    public string DatabaseName { get; set; } = String.Empty;
}
