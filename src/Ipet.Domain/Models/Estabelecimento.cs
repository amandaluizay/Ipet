using EnterpriseStore.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ipet.Domain.Models
{
    public class Estabelecimento : Entity
    {
        public Guid Conta { get; set; }
        public string Nome { get; set; }
        public string Documento { get; set; }
        public string Imagem { get; set; }
        public string Endereco { get; set; }
        public bool Ativo { get; set; }

        /* EF Relations */
        public IEnumerable<Produto> Produtos { get; set; }
    }
}
