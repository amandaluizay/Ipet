using Ipet.Data.Context;
using Ipet.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Ipet.Data.Repository
{
    public class HashtagRepository : Repository<Hashtag>, IHashtagRepository
    {
        public HashtagRepository(MeuDbContext context) : base(context) { }



    }
}