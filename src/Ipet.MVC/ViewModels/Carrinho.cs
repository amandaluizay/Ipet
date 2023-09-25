using EnterpriseStore.Domain.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Ipet.Domain.Models
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
