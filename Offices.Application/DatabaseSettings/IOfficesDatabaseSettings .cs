using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Offices.Application.DatabaseSettings;

public interface IOfficesDatabaseSettings
{
    public string OfficeCollectionName { get; set; }
    public string PhotoCollectionName { get; set; }
    public string ConnectionString { get; set; }
    public string OfficeDatabaseName { get; set; }
}
