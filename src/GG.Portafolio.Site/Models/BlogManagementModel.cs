using GG.Portafolio.Shared.Blog;
using System.ComponentModel.DataAnnotations;

namespace GG.Portafolio.Site.Models
{
    public class BlogManagementModel : BlogContentReponse
    {
        public string Images { get; set; }
    }

    public class ImgageLoad : Blog
    {
        [Required]
        public string FileName { get; set; }

        [Required]
        public byte[] FileContent { get; set; }
    }

    public class ImageLoadResponse
    {
        public bool Success { get; set; }

        public string Url { get; set; }

        public string FileName { get; set; }
    }
}
