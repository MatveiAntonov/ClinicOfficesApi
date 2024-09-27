using Offices.Domain.Entities;

namespace Photos.Application.Interfaces
{
	public interface IPhotoRepository
	{
		Task<Photo> CreatePhoto(Photo photo, CancellationToken cancellationToken);
		Task<Photo> DeletePhoto(int id, CancellationToken cancellationToken);
	}
}
