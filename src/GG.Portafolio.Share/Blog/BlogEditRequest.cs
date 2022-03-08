using System.ComponentModel.DataAnnotations;

namespace GG.Portafolio.Shared.Blog
{
    public class BlogEditRequest : BlogNewRequest
    {
        [Required]
        public string Id { get; set; }
    }
}
