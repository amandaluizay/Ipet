namespace Ipet.Domain.Models
{
    public class TagsProduto: Entity
    {
        public Guid ProdutoId { get; set; }

        public string Nome { get; set; }
        public string Estabelecimento { get; set; }
        public string Descricao { get; set; }
        public string Imagem { get; set; }
        public decimal Valor { get; set; }
        public bool Ativo { get; set; }
        public IEnumerable<HashTags> Produtos { get; set; }
    }
}
