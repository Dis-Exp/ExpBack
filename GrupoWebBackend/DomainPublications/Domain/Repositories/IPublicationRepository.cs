using System.Collections.Generic;
using System.Threading.Tasks;
using GrupoWebBackend.DomainPublications.Domain.Models;

namespace GrupoWebBackend.DomainPublications.Domain.Repositories
{
    public interface IPublicationRepository
    {
        Task<IEnumerable<Publication>> ListPublicationsAsync();
        Task AddAsync(Publication publication);
        Task<Publication> FindByIdAsync(int id);
        void Update(Publication publication);
        void Remove(Publication publication);
        Task<IEnumerable<Publication>> FindByUserId(int userId);
        Task<Publication> FindByPetId(int petId);
        Task<IEnumerable<object>> ListPublicationsInfoPetsAsync();
        Task<IEnumerable<object>> ListPublicationsInfoPetsAsyncByUserId(int id);
    }
}