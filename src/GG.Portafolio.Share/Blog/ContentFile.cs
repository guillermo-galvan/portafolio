using System.ComponentModel.DataAnnotations;

namespace GG.Portafolio.Shared.Blog
{
    public  class ContentFile
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string Url { get; set; }

        [Required]
        public byte[] File { get; set; }
    }

}
