using Ipet.Data.Context;
using Ipet.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Ipet.Data.Repository
{
    public class ServiçoHashtagRepository : Repository<ServiçoHashtag>, IServiçoHashtagRepository
    {
        public ServiçoHashtagRepository(MeuDbContext context) : base(context) { }



    }
}