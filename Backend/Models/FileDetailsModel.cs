namespace Backend.Models
{
    public class FileDetailsModel
    {
        public int Size { get; set; }
        public DateTime UploadDate { get; set; }
        public string Name { get; set; } = null!;
    }
}
