using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using GrupoWebBackend.DomainSubscriptions.Domain.Models;
using GrupoWebBackend.DomainSubscriptions.Domain.Services.Communications;
using GrupoWebBackend.DomainPublications.Domain.Models;

namespace GrupoWebBackend.DomainSubscriptions.Domain.Repositories
{
    public interface ISubscriptionRepository
    { 
        Task<IEnumerable<Subscription>> ListAsync();
        Task<Subscription> FindAsync(int id);
        Task AddAsync(Subscription subscription);
        void UpdateAsync(Subscription subscription);
        void Delete(Subscription subscription);
        IEnumerable<Subscription> GetSubscription(int userId);
    }
}