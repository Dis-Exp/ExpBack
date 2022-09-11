using GrupoWebBackend.DomainAdvertisements.Domain.Services.Communications;
using GrupoWebBackend.DomainSubscriptions.Domain.Models;

namespace GrupoWebBackend.DomainSubscriptions.Domain.Services.Communications
{
    public class SaveSubscriptionResponse: BaseResponseA
    {

        public Subscription Subscription { get; private set; }
        public SaveSubscriptionResponse(Subscription subscription) : this(true, string.Empty, subscription)
        {
        }
        public SaveSubscriptionResponse(bool succces, string message, Subscription subscription) : base(succces, message)
        {
            Subscription = subscription;
        }

        public SaveSubscriptionResponse(string message) : this(true, message, null)
        {
        }
    }
}