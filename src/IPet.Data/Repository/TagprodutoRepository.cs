using Ipet.Data.Context;
using Ipet.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Ipet.Data.Repository
{
    public class TagProdutoRepository : Repository<TagProduto>, ITagProdutoRepository
    {
        public TagProdutoRepository(MeuDbContext context) : base(context) { }



    }
}