using MongoDB.Driver;
using Offices.Application.Interfaces;
using Offices.Domain.Entities;
using Offices.Application.DatabaseSettings;
using Microsoft.Extensions.Logging;

namespace Offices.Application.Repositories;

public class OfficeRepository : IOfficeRepository
{
	private readonly IMongoCollection<Office> _offices;

	public OfficeRepository(IOfficesDatabaseSettings settings, ILogger<OfficeRepository> logger)
	{
		var client = new MongoClient(settings.ConnectionString);
		var database = client.GetDatabase(settings.OfficeDatabaseName);
		_offices = database.GetCollection<Office>(settings.OfficeCollectionName);
	}

	public async Task<IEnumerable<Office>> GetAllOffices(CancellationToken cancellationToken)
	{
		return await _offices.Find(s => true).ToListAsync();
	}

	public async Task<Office> GetOffice(string id, CancellationToken cancellationToken)
	{
		return await _offices.Find<Office>(s => s.Id == id).FirstOrDefaultAsync();
	}

	public async Task<Office> CreateOffice(Office office, CancellationToken cancellationToken)
	{
		await _offices.InsertOneAsync(office);
		return await _offices.Find<Office>(s => s.Id == office.Id).FirstOrDefaultAsync();
	}
	public async Task<Office> UpdateOffice(Office office, CancellationToken cancellationToken)
	{
		await _offices.ReplaceOneAsync(s => s.Id == office.Id, office);
		return await _offices.Find<Office>(s => s.Id == office.Id).FirstOrDefaultAsync();
	}

	public async Task<Office> DeleteOffice(string id, CancellationToken cancellationToken)
	{
		await _offices.DeleteOneAsync(s => s.Id == id);
		return await _offices.Find<Office>(s => s.Id == id).FirstOrDefaultAsync();
	}

}
