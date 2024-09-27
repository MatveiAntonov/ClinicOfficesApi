using MongoDB.Driver;
using Photos.Application.Interfaces;
using Microsoft.Extensions.Logging;
using Offices.Domain.Entities;
using Offices.Application.DatabaseSettings;
using Events;
using MassTransit;

namespace Offices.Persistence.Repositories;

public class PhotoRepository : IPhotoRepository
{
	private readonly IMongoCollection<Photo> _photos;


	public PhotoRepository(IOfficesDatabaseSettings settings)
	{
		var client = new MongoClient(settings.ConnectionString);
		var database = client.GetDatabase(settings.OfficeDatabaseName);
		_photos = database.GetCollection<Photo>(settings.PhotoCollectionName);
	}

	public async Task<Photo> CreatePhoto(Photo photo, CancellationToken cancellationToken)
	{
		await _photos.InsertOneAsync(photo);

		return await _photos.Find<Photo>(s => s.Id == photo.Id).FirstOrDefaultAsync();
	}

	public async Task<Photo> DeletePhoto(int id, CancellationToken cancellationToken)
	{
		await _photos.DeleteOneAsync(s => s.Id == id);
		return await _photos.Find<Photo>(s => s.Id == id).FirstOrDefaultAsync();
	}

}
