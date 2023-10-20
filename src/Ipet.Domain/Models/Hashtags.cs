using Ipet.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ipet.Domain.Models
{
    public class Hashtag : Entity
    {
        public Guid IdProduto { get; set; }
        public Guid IdServico { get; set; }
        public string Tag { get; set; }

        public Servico servico { get; set; }
        public Produto produto { get; set; }
    }
}
