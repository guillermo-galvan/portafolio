using System.ComponentModel.DataAnnotations;

namespace GG.Portafolio.Shared.Blog
{
    public class BlogResponse : Blog
    {
        [Required(ErrorMessage = "El campo nombre es requerido.")]
        [Display(Name = "Titulo", Prompt = "Titulo")]
        public string Title { get; set; }


        [Required(ErrorMessage = "El campo nombre es requerido.")]
        [Display(Name = "Descripción", Prompt = "Descripción")]
        public string Dsc { get; set; }

        [Display(Name = "Fecha de creación", Prompt = "Descripción")]
        public long CreateDate { get; set; }
    }
}
