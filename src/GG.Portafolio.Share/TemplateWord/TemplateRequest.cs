using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace GG.Portafolio.Shared.TemplateWord
{
    public class TemplateRequest
    {
        public TemplateRequest()
        {
            DetailRows = new();
        }

        [Required(ErrorMessage = "El campo nombre es requerido.")]
        [Display(Name = "Nombre del solicitante", Prompt = "Nombre del solicitante")]
        public string Name { get; set; }

        [Required(ErrorMessage = "El campo titulo columna 1 es requerido.")]
        [Display(Name = "Titulo columna 1", Prompt = "Titulo columna 1")]        
        public string ColumnName1 { get; set; }

        [Required(ErrorMessage = "El campo titulo columna 2 es requerido.")]
        [Display(Name = "Titulo columna 2", Prompt = "Titulo columna 2")]
        public string ColumnName2 { get; set; }

        [Required(ErrorMessage = "El campo titulo columna 3 es requerido.")]
        [Display(Name = "Titulo columna 3", Prompt = "Titulo columna 3")]
        public string ColumnName3 { get; set; }

        [Display(Name = "Password para control de cambios del archivo.", Prompt = "Password para control de cambios del archivo.")]
        public string PasswordChangeControl { get; set; }

        public List<DetailRow> DetailRows { get; set; }
    }
}
