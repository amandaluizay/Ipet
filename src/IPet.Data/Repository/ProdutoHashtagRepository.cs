using Ipet.Data.Context;
using Ipet.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Ipet.Data.Repository
{
    public class ProdutoHashtagRepository : Repository<ProdutoHashtag>, IProdutoHashtagRepository
    {
        public ProdutoHashtagRepository(MeuDbContext context) : base(context) { }



    }
}