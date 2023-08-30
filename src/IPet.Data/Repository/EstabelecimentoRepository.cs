
using EnterpriseStore.Data.Context;
using EnterpriseStore.Domain.Intefaces;
using EnterpriseStore.Domain.Models;
using EnterpriseStore.Service.Models;
using Ipet.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace EnterpriseStore.Data.Repository
{
    public class EstabelecimentoRepository : Repository<Estabelecimento>, IEstabelecimentoRepository
    {
        public EstabelecimentoRepository(MeuDbContext context) : base(context)
        {
        }


    }
}