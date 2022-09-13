using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using GrupoWebBackend.DomainAdoptionsRequests.Domain.Models;
using GrupoWebBackend.DomainAdoptionsRequests.Domain.Repositories;
using GrupoWebBackend.DomainAdoptionsRequests.Domain.Services;
using GrupoWebBackend.DomainPets.Domain.Repositories;
using GrupoWebBackend.Shared.Persistence.Repositories;
using GrupoWebBackend.Shared.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using GrupoWebBackend.DomainAdoptionsRequests.Domain.Services.Communications;
using GrupoWebBackend.DomainPublications.Domain.Repositories;
using GrupoWebBackend.Security.Domain.Repositories;
using GrupoWebBackend.Shared.Domain.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GrupoWebBackend.DomainAdoptionsRequests.Services
{
    public class AdoptionsRequestsService:IAdoptionsRequestsService
    {
        private readonly IAdoptionsRequestsRepository _requestsAdoptionsRepository;

        private readonly IUnitOfWork _unitOfWork;
        
        private readonly IPublicationRepository _publicationRepository;
        private readonly IUserRepository _userRepository;

        public AdoptionsRequestsService(IAdoptionsRequestsRepository adoptionsRequestsRepository,
            IPublicationRepository publicationRepository,
            IUnitOfWork unitOfWork, IUserRepository userRepository)
        {
            _requestsAdoptionsRepository = adoptionsRequestsRepository;
            _unitOfWork = unitOfWork;
            _publicationRepository = publicationRepository;
            _userRepository = userRepository;

        }

        public async Task<IEnumerable<AdoptionsRequests>> ListAdoptionsRequestsAsync()
        {
            return await _requestsAdoptionsRepository.ListAdoptionsRequestsAsync();
        }

     /*   public async Task<IEnumerable<AdoptionsRequests>> ListByUserId(int userId)
        {
            return await _requestsAdoptionsRepository.FindByUserId(userId);
        }
*/
      public async Task<SaveAdoptionsRequestsResponse> AddAsync(AdoptionsRequests adoptionsRequest)
      {
          var existingPublication = _publicationRepository.FindByUserId(adoptionsRequest.UserIdFrom);
          
          if (existingPublication == null)
              return new SaveAdoptionsRequestsResponse(false, "Invalid User.", adoptionsRequest);
           
          var existingUser = await _userRepository.FindByIdAsync(adoptionsRequest.UserIdFrom);
          
          if (existingUser == null)
              return new SaveAdoptionsRequestsResponse(false, "Invalid User.", adoptionsRequest);
          
          if (!existingUser.IsAuthenticated())
              return new SaveAdoptionsRequestsResponse(false, "This user is not authenticated.", adoptionsRequest);
          if (existingUser.IsReported())
              return new SaveAdoptionsRequestsResponse(false, "This user has at least one report.", adoptionsRequest);
          
          try
          {
              await _requestsAdoptionsRepository.AddAsync(adoptionsRequest);
              await _unitOfWork.CompleteAsync();
              return new SaveAdoptionsRequestsResponse(adoptionsRequest);
          }
          catch (Exception e)
          {
              return new SaveAdoptionsRequestsResponse($"An error occurred while saving Category: {e.Message}");
          }
      }

      public async Task<AdoptionsRequestsResponse> UpdateAsync(int id, AdoptionsRequests adoptionsRequest)
      {
          var existingAdoptionsRequests = await _requestsAdoptionsRepository.FindByIdAsync(id);
          if (existingAdoptionsRequests == null)
              return new AdoptionsRequestsResponse("Adoptions Requests not Found");
          existingAdoptionsRequests.Message = adoptionsRequest.Message;
          existingAdoptionsRequests.Status = adoptionsRequest.Status;
          existingAdoptionsRequests.UserIdFrom = adoptionsRequest.UserIdFrom;
          existingAdoptionsRequests.UserIdAt = adoptionsRequest.UserIdAt;
          existingAdoptionsRequests.PublicationId = adoptionsRequest.PublicationId;

          try
          {
              _requestsAdoptionsRepository.Update(existingAdoptionsRequests);
              await _unitOfWork.CompleteAsync();
              return new AdoptionsRequestsResponse(existingAdoptionsRequests);
          }
          catch (Exception e)
          {
              return new AdoptionsRequestsResponse($"An error occurred while saving AdoptionsRequests: {e.Message}");
          }
      }

      public async Task<IEnumerable<AdoptionsRequests>> getAllUserAt(int id)
      {
              var result = await _requestsAdoptionsRepository.getAllUserAtNotifications(id);
              return result;
      }
      public async Task<IEnumerable<AdoptionsRequests>> getAllUserFrom(int id)
      {
          var result = await _requestsAdoptionsRepository.getAllUserFromNotifications(id);
          return result;      
      }

      public async Task<AdoptionsRequestsResponse> DeleteAsync(int id)
      {
          var existingAdoptionsRequests = await _requestsAdoptionsRepository.FindByIdAsync(id);

          if (existingAdoptionsRequests == null)
              return new AdoptionsRequestsResponse("Adoptions Requests not found");

          try
          {
              _requestsAdoptionsRepository.Remove(existingAdoptionsRequests);
              await _unitOfWork.CompleteAsync();
              return new AdoptionsRequestsResponse(existingAdoptionsRequests);
          }
          catch(Exception e)
          {
              return new AdoptionsRequestsResponse($"An error occurred while deleting the pet: {e.Message}");
          }
      }
    }
}