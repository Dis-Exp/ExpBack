using GrupoWebBackend.DomainAdoptionsRequests.Domain.Models;

namespace GrupoWebBackend.DomainAdoptionsRequests.Resources
{
    public class AdoptionsRequestsResource
    {
        public int Id { get; set; }
        public string Message { get; set; }
        public AdoptionRequestStatus Status { get; set; }
        public int UserIdFrom { get; set; }
        public int UserIdAt { get; set; }
        public int PublicationId { get; set; }
       
    }
}