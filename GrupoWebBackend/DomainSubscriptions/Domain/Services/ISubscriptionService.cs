using System.Collections.Generic;
using System.Threading.Tasks;
using GrupoWebBackend.DomainSubscriptions.Domain.Models;
using GrupoWebBackend.DomainSubscriptions.Domain.Services.Communications;

namespace GrupoWebBackend.DomainSubscriptions.Domain.Services
{
    public interface ISubscriptionService
    {
        Task<IEnumerable<Subscription>> ListAsync();
        Task<Subscription> FindAsync(int id);
        Task<SaveSubscriptionResponse> AddAsync(Subscription publication);
        Task<SubscriptionResponse> UpdateAsync(Subscription subscription, int id);
        IEnumerable<Subscription> GetSubscription(int userId);
        Task<SubscriptionResponse> DeleteAsync(int id);

    }
    
}