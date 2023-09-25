namespace Ipet.Domain.Models
{
    public class HashTags : Entity
    {
        public string Descricao { get; set; }
        public bool Ativo { get; set; }
        public Produto produto { get; set; }
    }
}
