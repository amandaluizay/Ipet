using Ipet.Data.Context;
using Ipet.Domain.Intefaces;
using Ipet.Domain.Models;

namespace Ipet.Data.Repository
{
    public class ServicoRepository : Repository<Servico>, IServicoRepository
    {
        public ServicoRepository(MeuDbContext context) : base(context) { }

        //public async Task<Produto> ObterProdutoEstabelecimento(Guid id)
        //{
        //    return await Db.Produtos.AsNoTracking().Include(f => f.Estabelecimento)
        //        .FirstOrDefaultAsync(p => p.Id == id);
        //}

        //public async Task<IEnumerable<Produto>> ObterProdutosEstabelecimento()
        //{
        //    return await Db.Produtos.AsNoTracking().Include(f => f.Estabelecimento)
        //        .OrderBy(p => p.Nome).ToListAsync();
        //}

        //public async Task<IEnumerable<Produto>> ObterProdutosPorEstabelecimento(Guid estabelecimentoId)
        //{
        //    return await Buscar(p => p.EstabelecimentoId == estabelecimentoId);
        //}
    }
}