using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using GrupoWebBackend.DomainPets.Domain.Repositories;
using GrupoWebBackend.DomainPublications.Domain.Models;
using GrupoWebBackend.DomainPublications.Domain.Repositories;
using GrupoWebBackend.DomainPublications.Domain.Services;
using GrupoWebBackend.DomainPublications.Domain.Services.Communications;
using GrupoWebBackend.DomainPublications.Resources;
using GrupoWebBackend.Security.Domain.Repositories;
using GrupoWebBackend.Shared.Domain.Repositories;

namespace GrupoWebBackend.DomainPublications.Services
{
    public class PublicationService:IPublicationService
    {
        private readonly IPublicationRepository _publicationRepository;
        //private readonly IUserRepository _userRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUserRepository _userRepository;

        public PublicationService(IPublicationRepository publicationRepository,IUnitOfWork unitOfWork, IUserRepository userRepository)
        {
            _publicationRepository = publicationRepository;
            _unitOfWork = unitOfWork;
            _userRepository = userRepository;
        }
        public async Task<IEnumerable<Publication>> ListPublicationAsync()
        {
            return await _publicationRepository.ListPublicationsAsync();
        }
        public async Task<IEnumerable<Publication>> ListByUserId(int userId)
        {
            return await _publicationRepository.FindByUserId(userId);
        }

        public async Task<SavePublicationResponse> SaveAsync(Publication publication)
        { 
           
            var existingUserPublication = await _publicationRepository.FindByUserId(publication.UserId);
            if (existingUserPublication == null)
                return new SavePublicationResponse(false,"This pet is already published.", publication);
            
            var existingPetPublication = await _publicationRepository.FindByPetId(publication.PetId);
            if (existingPetPublication != null)
                return new SavePublicationResponse(false,"This pet is already published.", publication);
            
            var existingUser = await _userRepository.FindByIdAsync(publication.UserId);
          
            if (existingUser == null)
                return new SavePublicationResponse(false, "This user does not exist", publication);
            
            if (existingUser.IsReported())
                return new SavePublicationResponse(false, "This user has at least one report.", publication);
            
            if(existingUser.Subscription == null)
                return new SavePublicationResponse(false, "This user not have a subscription.", publication);
            
            try
            {
                await _publicationRepository.AddAsync(publication);
                await _unitOfWork.CompleteAsync();
                return new SavePublicationResponse(publication);
            }
            catch (Exception e)
            {
                return new SavePublicationResponse($"An error occurred while saving the Publication: {e.Message}");
            }
        }

        public async Task<Publication> FindByIdAsync(int id)
        {
            return await _publicationRepository.FindByIdAsync(id);
            
        }

        public async Task<PublicationResponse> UpdateAsync(int id, Publication publication)
        {
            var existingPublication = await _publicationRepository.FindByIdAsync(id);

            if (existingPublication == null)
                return new PublicationResponse("Publication not Found");
            existingPublication.Comment = publication.Comment;
            existingPublication.DateTime = publication.DateTime;
            existingPublication.PetId = publication.PetId;
            existingPublication.UserId = publication.UserId;
            
            try
            {
                _publicationRepository.Update(existingPublication);
                await _unitOfWork.CompleteAsync();
                return new PublicationResponse(existingPublication);
            }
            catch (Exception e)
            {
                return new PublicationResponse($"An error occurred while saving Publication: {e.Message}");
            }
            
        }

        public async Task<PublicationResponse> DeleteAsync(int id)
        {

            var existingPublication = await _publicationRepository.FindByIdAsync(id);
            if (existingPublication == null)
                return new PublicationResponse("Pet not found.");
            try
            {
                _publicationRepository.Remove(existingPublication);
                await _unitOfWork.CompleteAsync();
                return new PublicationResponse(existingPublication);
            }
            catch (Exception e)
            {
                return new PublicationResponse($"An error occurred while deleting the publication: {e.Message}");
            }
        }
        
    }
}