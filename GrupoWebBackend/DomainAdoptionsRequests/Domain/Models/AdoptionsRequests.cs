
using System.Diagnostics.CodeAnalysis;
using GrupoWebBackend.DomainPublications.Domain.Models;
using GrupoWebBackend.Security.Domain.Entities;

namespace GrupoWebBackend.DomainAdoptionsRequests.Domain.Models
{
    public enum AdoptionRequestStatus
    {
        Pending,
        Accepted,
        Rejected
    }     
    public class AdoptionsRequests
    {
        public int Id { get; set; }
        public string Message { get; set; }
        public AdoptionRequestStatus Status { get; set; }
        public int UserIdFrom { get; set; }
        public int UserIdAt { get; set; }
        public User User { get; set; }
        public int PublicationId { get; set; }
        
        [AllowNull]
        public Publication Publication { get; set; }
    }
}