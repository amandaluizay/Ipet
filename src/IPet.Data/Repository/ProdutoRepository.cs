using Ipet.Data.Context;
using Ipet.Domain.Intefaces;
using Ipet.Domain.Models;

namespace Ipet.Data.Repository
{
    public class ProdutoRepository : Repository<Produto>, IProdutoRepository
    {
        public ProdutoRepository(MeuDbContext context) : base(context) { }


    }
}