using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EnterpriseStore.Data.Context;
using EnterpriseStore.Domain.Intefaces;
using EnterpriseStore.Domain.Models;
using Ipet.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace EnterpriseStore.Data.Repository
{
    public class CarrinhoRepository : Repository<Carrinho>, ICarrinhoRepository
    {
        public CarrinhoRepository(MeuDbContext context) : base(context) { }

        public async Task<Carrinho> ObterCarrinhoPorUsuario(Guid usuarioId)
        {
            return await Db.Carrinhos
                .Include(c => c.CarrinhoProdutos)
                .FirstOrDefaultAsync(c => c.UsuarioId == usuarioId);
        }

        public async Task<bool> ObterUsuarioPorId(Guid usuarioId)
        {
            return await Db.Carrinhos.AnyAsync(c => c.UsuarioId == usuarioId);
        }

    }
}