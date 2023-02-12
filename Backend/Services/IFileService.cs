using Backend.DataBase;
using Backend.Models;

namespace Backend.Services
{
    public interface IFileService
    {
        Task<FileDetailsModel?> UploadFile(FileUploadModel fileDetails);
        Task<FileDetailsModel?> FileDetails(string fileName);
        Task<IEnumerable<FileDetailsModel>> FilesDetails();

    }
}