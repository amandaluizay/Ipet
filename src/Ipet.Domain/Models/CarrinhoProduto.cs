using EnterpriseStore.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ipet.Domain.Models
{
    public class CarrinhoProduto : Entity
    {
        public Guid CarrinhoId { get; set; }
        public Carrinho Carrinho { get; set; }
        public Guid ProdutoId { get; set; }
        public Produto Produto { get; set; }
        public int Quantidade { get; set; }
    }
}
