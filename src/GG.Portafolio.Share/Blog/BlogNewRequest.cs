using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace GG.Portafolio.Shared.Blog
{
    public class BlogNewRequest
    {
        [Required]
        public string Title { get; set; }

        [Required]
        public string Dsc { get; set; }

        [Required]
        public string UserId { get; set; }

        [Required]
        public string Content { get; set; }

        public List<ContentFile> ContentFiles { get; set; } = new List<ContentFile>();
    }
}
