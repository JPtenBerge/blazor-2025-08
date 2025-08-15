using PhotoSharingApplication.Core.Models;

namespace PhotoSharingApplication.Core.Interfaces;

public interface IPhotosRepository
{
    Task<Photo> AddPhotoAsync(Photo photo);
    Task<Photo?> GetPhotoByIdAsync(int id);
    Task<IEnumerable<Photo>> GetPhotosAsync();
    Task<Photo?> DeletePhotoAsync(int id);
    Task UpdatePhotoAsync(Photo photo);
}