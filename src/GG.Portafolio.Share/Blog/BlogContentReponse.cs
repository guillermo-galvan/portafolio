using System.ComponentModel.DataAnnotations;

namespace GG.Portafolio.Shared.Blog
{
    public class BlogContentReponse : BlogResponse
    {
        [Required(ErrorMessage = "El campo es requerido.")]
        [Display(Name = "Contenido", Prompt = "Contenido")]
        public string Content { get; set; }

        public long EditDate { get; set; }
    }
}
