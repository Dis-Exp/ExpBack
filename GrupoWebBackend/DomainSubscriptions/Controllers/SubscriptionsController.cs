using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using GrupoWebBackend.DomainSubscriptions.Domain.Models;
using GrupoWebBackend.DomainSubscriptions.Domain.Services;
using GrupoWebBackend.DomainPublications.Domain.Models;
using GrupoWebBackend.Security.Authorization.Attributes;
using GrupoWebBackend.Extensions;
using GrupoWebBackend.DomainSubscriptions.Resources;
using Microsoft.AspNetCore.Mvc;

namespace GrupoWebBackend.DomainSubscriptions.Controllers
{
    [ApiController]
    [Route("/api/v1/[controller]")]
    public class SubscriptionsController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly ISubscriptionService _subscriptionService;

        public SubscriptionsController(IMapper mapper, ISubscriptionService subscriptionService)
        {
            _mapper = mapper;
            _subscriptionService = subscriptionService;
        }

        [HttpGet]
        public async Task<IEnumerable<Subscription>> ListAsync()
        {
            var subscriptions = await _subscriptionService.ListAsync();
            return subscriptions;
        }    
        
        [HttpGet("{id:int}")]
        public async Task<Subscription> FindAsync(int id)
        {
            var subscription = await _subscriptionService.FindAsync(id);
            return subscription;
        }


        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody] SaveSubscriptionResource resource)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.GetErrorMessages());
            
            var subscription = _mapper.Map<SaveSubscriptionResource, Subscription>(resource);
            var result = await _subscriptionService.AddAsync(subscription);
            
            if (!result.Succces)
                return BadRequest(result.Message);
            
            var subscriptionResource = _mapper.Map<Subscription, SubscriptionResource>(result.Subscription);
            return Ok(subscriptionResource);
        }
        
        [HttpPut("{id:int}")]
        public async Task<IActionResult> PutAsync ([FromBody] SaveSubscriptionResource resource, int id)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.GetErrorMessages());

            var subscription = _mapper.Map<SaveSubscriptionResource, Subscription>(resource);
            var result = await _subscriptionService.UpdateAsync(subscription, id);

            if (!result.Success)
                return BadRequest(result.Message);

            var subscriptionResource = _mapper.Map<Subscription, SubscriptionResource>(result.Resource);

            return Ok(subscriptionResource);
            
        }

        [HttpGet("userId={userId:int}")]
        public IEnumerable<Subscription> GetSubscriptions(int userId)
        {
            return _subscriptionService.GetSubscription(userId);
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var result = await _subscriptionService.DeleteAsync(id);
            if (!result.Success)
                return BadRequest(result.Message);
            var subscriptionResourceResource = _mapper.Map<Subscription, SubscriptionResource>(result.Resource);
            return Ok(subscriptionResourceResource);
        }
        
        
        
        

    }
}