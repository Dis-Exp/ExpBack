using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GrupoWebBackend.DomainSubscriptions.Domain.Models;
using GrupoWebBackend.DomainSubscriptions.Domain.Repositories;
using GrupoWebBackend.Shared.Persistence.Context;
using GrupoWebBackend.Shared.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;

namespace GrupoWebBackend.DomainSubscriptions.Persistence.Repositories
{
    public class SubscriptionRepository: BaseRepository, ISubscriptionRepository
    {
        public SubscriptionRepository(AppDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Subscription>> ListAsync()
        {
            return await _context.Subscriptions.ToListAsync();
        }
        public async Task<Subscription> FindAsync(int id)
        {
            return await _context.Subscriptions.FindAsync(id);   
        }
        public async Task AddAsync(Subscription subscription)
        {
            await _context.Subscriptions.AddAsync(subscription);
        }

        public void UpdateAsync(Subscription subscription)
        {
            _context.Subscriptions.Update(subscription);
        }

        public void Delete(Subscription subscription)
        {
            _context.Subscriptions.Remove(subscription);
        }

        public IEnumerable<Subscription> GetSubscription(int userId)
        {
            return _context.Subscriptions.Where(p => p.UserId.Equals(userId)).ToList();
        }
    }
}