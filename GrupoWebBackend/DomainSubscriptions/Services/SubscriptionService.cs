using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using GrupoWebBackend.DomainSubscriptions.Domain.Models;
using GrupoWebBackend.DomainSubscriptions.Domain.Repositories;
using GrupoWebBackend.DomainSubscriptions.Domain.Services;
using GrupoWebBackend.DomainSubscriptions.Domain.Services.Communications;
using GrupoWebBackend.DomainPublications.Domain.Repositories;
using GrupoWebBackend.Shared.Domain.Repositories;
using GrupoWebBackend.Security.Domain.Repositories;

namespace GrupoWebBackend.DomainSubscriptions.Services
{
    public class SubscriptionService: ISubscriptionService
    {
        private readonly ISubscriptionRepository _subscriptionRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUserRepository _userRepository;

        public SubscriptionService(ISubscriptionRepository subscriptionRepository, IUserRepository userRepository, IUnitOfWork unitOfWork)
        {
            _subscriptionRepository = subscriptionRepository;
            _unitOfWork = unitOfWork;
            _userRepository = userRepository;
        }

        public async Task<IEnumerable<Subscription>> ListAsync()
        {
            return await _subscriptionRepository.ListAsync();
        }



        public async Task<Subscription> FindAsync(int id)
        {
            return await _subscriptionRepository.FindAsync(id);
        }
        
        public async Task<SaveSubscriptionResponse> AddAsync(Subscription subscription)
        {
            var existingUser = _userRepository.FindById(subscription.UserId);
            if (existingUser == null)
                return new SaveSubscriptionResponse("Invalid user");
            
            try
            {
                await _subscriptionRepository.AddAsync(subscription);
                await _unitOfWork.CompleteAsync();
                return new SaveSubscriptionResponse(subscription);
            }
            catch (Exception e)
            {
                return new SaveSubscriptionResponse($"An error occurred while saving Subscription: {e.Message}");
            }
        }

        public async Task<SubscriptionResponse> UpdateAsync(Subscription subscription, int id)
        {
            var existingSubscription = await _subscriptionRepository.FindAsync(id);

            if (existingSubscription == null)
                return new SubscriptionResponse("Subscription not found");

            existingSubscription.NumPosts = subscription.NumPosts;
            existingSubscription.StartDate = subscription.StartDate;
            existingSubscription.EndDate = subscription.EndDate;
            existingSubscription.Expired = subscription.Expired;
            existingSubscription.UserId = subscription.UserId;

            try
            {
                _subscriptionRepository.UpdateAsync(existingSubscription);
                await _unitOfWork.CompleteAsync();
                return new SubscriptionResponse(existingSubscription);
            }
            catch (Exception e)
            {
                return new SubscriptionResponse($"An error occurred while saving Category: {e.Message}");
            }
        }

        public IEnumerable<Subscription> GetSubscription(int userId)
        {
            return _subscriptionRepository.GetSubscription(userId);
        }

        public async Task<SubscriptionResponse> DeleteAsync(int id)
        {
            var existingSubscription = await _subscriptionRepository.FindAsync(id);
            if (existingSubscription == null)
                return new SubscriptionResponse("Subscription not found.");
            try
            {
                _subscriptionRepository.Delete(existingSubscription);
                await _unitOfWork.CompleteAsync();
                return new SubscriptionResponse(existingSubscription);
            }
            catch (Exception e)
            {
                return new SubscriptionResponse($"An error occurred while deleting the subscription: {e.Message}");
            }
        }
    }
}

