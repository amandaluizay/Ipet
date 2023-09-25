using EnterpriseStore.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ipet.Domain.Models
{
    public class Carrinho : Entity
    {
        public Guid UsuarioId { get; set; }
        public ICollection<CarrinhoProduto> CarrinhoProdutos { get; set; }

    }
}
