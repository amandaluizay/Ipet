using System.ComponentModel.DataAnnotations;

namespace Ipet.Api.ViewModels
{
    public class ProdutoHashtagViewModel
    {
        [Key]
        public Guid Id { get; set; }

        public Guid IdProduto { get; set; }
        public string Tag { get; set; }
    }
}
