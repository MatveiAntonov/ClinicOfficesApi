using MongoDB.Driver;
using Offices.Application.Interfaces;
using Offices.Domain.Entities;
using Offices.Application.DatabaseSettings;

namespace Offices.Application.Repositories
{
    public class OfficeRepository : IOfficeRepository
    {
        private readonly IMongoCollection<Office> _offices;

        public OfficeRepository(IOfficesDatabaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);
            _offices = database.GetCollection<Office>(settings.DatabaseCollectionName);
        }

        public async Task<IEnumerable<Office>> GetAllOffices(CancellationToken cancellationToken)
        {
            return await _offices.Find(s => true).ToListAsync();
        }

        public async Task<Office> GetOffice(string id, CancellationToken cancellationToken)
        {
            var entity = await _offices.Find<Office>(s => s.Id == id).FirstOrDefaultAsync();
            if (entity == null || entity.Id != id)
            {
                // HANDLE EXCEPTION
                throw new Exception();
            }
            else
            {
                return entity;
            }
        }

        public async Task<Office> CreateOffice(Office office, CancellationToken cancellationToken)
        {
            if (office is not null)
            {
                await _offices.InsertOneAsync(office);
                var entity = await _offices.Find<Office>(s => s.Id == office.Id).FirstOrDefaultAsync();
                if (entity is not null)
                    return entity;
                else
                    // HANDLE EXCEPTION
                    throw new Exception();
            }
            else
                // HANDLE EXCEPTION
                throw new Exception();
        }
        public async Task<Office> UpdateOffice(Office office, CancellationToken cancellationToken)
        {
            await _offices.ReplaceOneAsync(s => s.Id == office.Id, office);
            var entity = await _offices.Find<Office>(s => s.Id == office.Id).FirstOrDefaultAsync();

            if (entity is not null && entity.Id == office.Id) 
            {
                return entity;
            }
            else
            {
                // HANDLE EXCEPTION
                throw new Exception();
            }
        }

        public async Task<Office> DeleteOffice(string id, CancellationToken cancellationToken)
        {
            var entity = await _offices.Find<Office>(s => s.Id == id).FirstOrDefaultAsync();

            if (entity is not null || entity.Id == id)
            {
                await _offices.DeleteOneAsync(s => s.Id == id);
            }
            else
            {
                // HANDLE EXCEPTION 
                throw new Exception();
            }
            return entity;
        }

    }
}
