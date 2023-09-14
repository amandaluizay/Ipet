using EnterpriseStore.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ipet.Domain.Models
{
    public class Produto : Entity
    {
        public Guid EstabelecimentoId { get; set; }

        public string Nome { get; set; }
        public string Estabelecimento { get; set; }
        public string Descricao { get; set; }
        public string Imagem { get; set; }
        public decimal Valor { get; set; }
        public bool Ativo { get; set; }
        public IEnumerable<HashTags> Servicos { get; set; }
    }
}
