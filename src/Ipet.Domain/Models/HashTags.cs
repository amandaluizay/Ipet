using EnterpriseStore.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ipet.Domain.Models
{
    public class HashTags : Entity
    {
        public string Descricao { get; set; }
        public bool Ativo { get; set; }
        public Produto produto { get; set; }
    }
}
