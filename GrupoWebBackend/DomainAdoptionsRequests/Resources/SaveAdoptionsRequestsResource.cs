using System.ComponentModel.DataAnnotations;
using GrupoWebBackend.DomainAdoptionsRequests.Domain.Models;

namespace GrupoWebBackend.DomainAdoptionsRequests.Resources
{
    public class SaveAdoptionsRequestsResource
    {
        [Required]
        public string Message { get; set; }
        
        [Required]
        public AdoptionRequestStatus Status { get; set; }
        
        [Required]
        public int UserIdFrom { get; set; }
        
        [Required]
        public int UserIdAt { get; set; }
          
        public int PublicationId { get; set; }

    }
}