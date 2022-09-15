using GrupoWebBackend.DomainAdoptionsRequests.Domain.Models;
using GrupoWebBackend.Shared.Persistence.Context;
using GrupoWebBackend.DomainPets.Domain.Services.Communications;
using GrupoWebBackend.DomainPets.Persistence.Repositories;
using GrupoWebBackend.DomainPublications.Domain.Models;

namespace GrupoWebBackend.DomainPublications.Domain.Services.Communications
{
    public class SavePublicationResponse:BaseResponseA
    {
        public Publication PublicationResponse { get; set; }
    
        public SavePublicationResponse(Publication publication) : this(true, string.Empty, publication)
        {
        }
    
        public SavePublicationResponse(bool succces, string message, Publication publication) : base(succces,
            message)
        {
            PublicationResponse = publication;
        }
        
        public SavePublicationResponse(string message) : this(true, message, null)
        {
            
        }
        
    }
}



