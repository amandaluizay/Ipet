using Ipet.Service.Services;

namespace Ipet.Domain.Models
{
    public class TagProduto : Entity
    {
        public Guid IdProduto { get; set; }
        public string TipoAnimal { get; set; }
        public string Porte { get; set; }
        public string Marca { get; set; }
        public List<Hashtag> Hashtags { get; set; }
        public bool Ativo { get; set; }
        public Produto Produto { get; set; }
    }
}
