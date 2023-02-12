using Backend.Models;

namespace Backend.Services;

public class FileService : IFileService
{
    private readonly IMongoDbService _mongoDbService;

    public FileService(IMongoDbService mongoDbService)
    {
        _mongoDbService = mongoDbService;
    }

    public async Task<FileDetailsModel?> UploadFile(FileUploadModel file)
    {
        byte[] fileBytes;

        using (var reader = new MemoryStream())
        {
            file.FormFile.CopyTo(reader);
            fileBytes = reader.ToArray();
        }

        var dbFile = new FileDbModel { Content = fileBytes, Name = file.FormFile.FileName, Size = fileBytes.Length, UploadDate = DateTime.Now };
        var result = await _mongoDbService.CreateAsync(dbFile);

        return result 
            ? ToFileDetailsModel(dbFile)
            : null;
    }

    public async Task<FileDetailsModel?> FileDetails(string fileName)
    {
        var dbFile = await _mongoDbService.GetFileDetails(fileName);
        return dbFile is not null
            ? ToFileDetailsModel(dbFile)
            : null;
    }

    public async Task<IEnumerable<FileDetailsModel>> FilesDetails()
    {
        var dbFiles = await _mongoDbService.GetFilesDetails();
        var res = dbFiles.Select(x => ToFileDetailsModel(x));
        return res;
    }

    private FileDetailsModel ToFileDetailsModel(FileDbModel fileDbModel)
    {
        return new FileDetailsModel { Name = fileDbModel.Name, Size = fileDbModel.Size, UploadDate = fileDbModel.UploadDate };
    }

}