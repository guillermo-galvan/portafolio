using System.ComponentModel.DataAnnotations;

namespace GG.Portafolio.Shared.TemplateWord
{
    public class DetailRow
    {
        [Required(ErrorMessage = "El campo es requerido.")]
        [Display(Name = "Detalle columna", Prompt = "Detalle columna 1")]
        public string DetailColumn1 { get; set; }

        [Required(ErrorMessage = "El campo es requerido.")]
        [Display(Name = "Detalle columna", Prompt = "Detalle columna 2")]
        public string DetailColumn2 { get; set; }

        [Required(ErrorMessage = "El campo es requerido.")]
        [Display(Name = "Detalle columna", Prompt = "Detalle columna 3")]
        public string DetailColumn3 { get; set; }
    }
}
