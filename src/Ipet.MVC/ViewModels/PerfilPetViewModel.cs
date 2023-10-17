namespace Ipet.Domain.Models
{
    public class PerfilPetViewModel
    {
        public Guid Id { get; set; }
        public Guid IdUsuario { get; set; }
        public string TipoAnimal { get; set; }

        public string Nome { get; set; }

        public string Raca { get; set; }

        public int Idade { get; set; }

        public string Porte { get; set; }

        public string Imagem { get; set; }

        public string Observacao { get; set; }
    }

}
