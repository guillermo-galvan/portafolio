using System.ComponentModel.DataAnnotations;

namespace GG.Portafolio.Shared.User
{
    public class UserRequest
    {
        [Required(ErrorMessage = "El campo es requerido.")]
        public string Subject { get; set; }

        [Required(ErrorMessage = "El campo es requerido.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "El campo es requerido.")]
        [EmailAddress]
        public string Email { get; set; }
    }
}
