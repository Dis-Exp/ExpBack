using GrupoWebBackend.DomainAdvertisements.Domain.Models;
using GrupoWebBackend.DomainAdvertisements.Domain.Services.Communications;
using GrupoWebBackend.DomainSubscriptions.Domain.Models;

namespace GrupoWebBackend.DomainSubscriptions.Domain.Services.Communications
{
    public class SubscriptionResponse: BaseResponse<Subscription>
    {
        public SubscriptionResponse(string message): base(message)
        {

        }
        public SubscriptionResponse(Subscription resource): base(resource)
        {

        }
    }
}