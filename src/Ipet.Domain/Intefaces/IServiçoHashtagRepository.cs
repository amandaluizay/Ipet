
using Ipet.Domain.Intefaces;
using Ipet.Domain.Models;

namespace Ipet.Data.Repository
{
    public interface IServiçoHashtagRepository : IRepository<ServiçoHashtag>
    {
        Task ExcluirTagsDoServico(Guid idServico);
        Task<List<ServiçoHashtag>> ObterPorServicoId(Guid servicoId);
    }
}