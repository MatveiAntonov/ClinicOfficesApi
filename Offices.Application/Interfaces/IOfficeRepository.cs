using Offices.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Offices.Application.Interfaces {
    public interface IOfficeRepository {
        Task<IEnumerable<Office>> GetAllOffices(CancellationToken cancellationToken);
        Task<Office> GetOffice(string id, CancellationToken cancellationToken);
        Task<Office> CreateOffice(Office office, CancellationToken cancellationToken);
        Task<Office> UpdateOffice(Office office, CancellationToken cancellationToken);
        Task<Office> DeleteOffice(string id, CancellationToken cancellationToken);
    }
}
