using System;
using System.ComponentModel.DataAnnotations;

namespace GG.Portafolio.Shared.Blog
{
    public class BlogComments
    {
        [Required]
        public string BlogId { get; set; }

        [Required(ErrorMessage = "El campo nombre es requerido.")]
        [Display(Name = "Nombre", Prompt = "Nombre")]
        public string Name { get; set; }

        [Required(ErrorMessage = "El campo nombre es requerido.")]
        [Display(Name = "Comentario", Prompt = "Comentario")]
        public string Content { get; set; }

        [Required]
        public DateTime Date { get; set; }

        public string User_Id { get; set; }
    }
}
