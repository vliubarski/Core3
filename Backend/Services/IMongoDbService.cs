using Backend.Models;

namespace Backend.Services
{
    public interface IMongoDbService
    {
        Task<bool> CreateAsync(FileDbModel fileDb);
        Task<FileDbModel?> GetFileDetails(string fileName);
        Task<List<FileDbModel>> GetFilesDetails();
    }
}