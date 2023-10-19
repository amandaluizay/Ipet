namespace Ipet.Domain.Models
{
    public class TagProduto : Entity
    {
        public string TipoAnimal { get; set; }
        public string Porte { get; set; }
        public string Marca { get; set; }
        public List<string> Hashtags { get; set; }
        public bool Ativo { get; set; }
        public Produto produto { get; set; }
    }
}
