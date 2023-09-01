using Microsoft.AspNetCore.Identity;

namespace Ipet.MVC.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string Nome { get; set; }    
        public string Password { get; set; }
        public string Endereco { get; set; }
        public string ImagemUpload { get; set; }
        public string Imagem { get; set; }
        public string Documento { get; set; }
        public string ConfirmPassword { get; set; }
    }
}
