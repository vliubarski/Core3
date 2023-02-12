using Backend.DataBase;
using Backend.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace Backend.Services;

public class MongoDbService : IMongoDbService
{
    private readonly IMongoCollection<FileDbModel> _filesCollection;
    private readonly ILogger _logger;

    public MongoDbService(
        IOptions<FileStoreDatabaseSettings> bookStoreDatabaseSettings, ILogger<MongoDbService> logger)
    {
        _logger = logger;
         var mongoClient = new MongoClient(
            bookStoreDatabaseSettings.Value.ConnectionString);

        var mongoDatabase = mongoClient.GetDatabase(
            bookStoreDatabaseSettings.Value.DatabaseName);

        _filesCollection = mongoDatabase.GetCollection<FileDbModel>(
            bookStoreDatabaseSettings.Value.FileCollectionName);

        InitIndexForFileName();
    }

    private void InitIndexForFileName()
    {
        var indexKey = Builders<FileDbModel>.IndexKeys;
        var indexModel = new CreateIndexModel<FileDbModel>(indexKey.Ascending(x => x.Name), new CreateIndexOptions { Unique = true });
        _filesCollection.Indexes.CreateOne(indexModel);
    }

    public async Task<bool> CreateAsync(FileDbModel fileDb)
    {
        try
        {
            await _filesCollection.InsertOneAsync(fileDb);
            return true;
        }
        catch (Exception e)
        {
            _logger.LogError(e, e.Message);
            return false;
        }
    }

    public async Task<FileDbModel?> GetFileDetails(string fileName)
    {
        var condition = Builders<FileDbModel>.Filter.Eq(p => p.Name, fileName);
        var fields = Builders<FileDbModel>.Projection
            .Include(p => p.Size)
            .Include(p => p.UploadDate)
            .Include(p => p.Name);

        var results = _filesCollection.Find(condition).Project<FileDbModel>(fields)
            .SingleOrDefaultAsync();

        return await results;
    }

    public async Task<List<FileDbModel>> GetFilesDetails()
    {
        var res = await _filesCollection.FindAsync(_ => true);
        return await res.ToListAsync();
    }
}