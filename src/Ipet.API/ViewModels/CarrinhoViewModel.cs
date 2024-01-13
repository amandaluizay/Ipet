using System.ComponentModel.DataAnnotations;

namespace Ipet.Api.ViewModels
{
    public class CarrinhoViewModel
    {
        [Key]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "O campo UsuarioId é obrigatório.")]
        public Guid UsuarioId { get; set; }

        [Display(Name = "Produtos no Carrinho")]
        public List<CarrinhoProdutoViewModel> Produtos { get; set; }

    }
}
