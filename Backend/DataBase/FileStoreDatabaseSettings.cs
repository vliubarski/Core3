namespace Backend.DataBase
{
    public class FileStoreDatabaseSettings
    {
        public string ConnectionString { get; set; } = null!;

        public string DatabaseName { get; set; } = null!;

        public string FileCollectionName { get; set; } = null!;
    }
}
