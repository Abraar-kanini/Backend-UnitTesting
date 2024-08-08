using System.ComponentModel.DataAnnotations.Schema;

namespace PracticeTesting.Models
{
    public class Image
    {
        public Guid id { get; set; }
        [NotMapped]
        public IFormFile File { get; set; }

        public string fileName { get; set; }


        public string FileExtention { get; set; }

        public long FileSizeInBytes { get; set; }
        public string FilePath { get; set; }
    }
}
