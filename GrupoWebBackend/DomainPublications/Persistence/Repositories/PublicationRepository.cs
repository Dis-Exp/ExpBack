using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GrupoWebBackend.DomainPublications.Domain.Models;
using GrupoWebBackend.DomainPublications.Domain.Repositories;
using GrupoWebBackend.Shared.Persistence.Context;
using GrupoWebBackend.Shared.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;

namespace GrupoWebBackend.DomainPublications.Persistence.Repositories
{
    public class PublicationRepository:BaseRepository,IPublicationRepository
    {
        public PublicationRepository(AppDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Publication>> ListPublicationsAsync()
        {
            //USING framework net
            
            return await _context.Publications.ToListAsync();
        }

        public async Task AddAsync(Publication publication)
        {
            await _context.Publications.AddAsync(publication);
        }

        public async Task<Publication> FindByIdAsync(int id)
        {
            return await _context.Publications.FindAsync(id);
        }

        public void Update(Publication publication)
        {
            _context.Publications.Update(publication);
        }

        public void Remove(Publication publication)
        {
            _context.Publications.Remove(publication);
        }

        public async Task<IEnumerable<Publication>> FindByUserId(int userId)
        {
            return await _context.Publications.Where(p => p.UserId == userId)
                .Include(p => p.User)
                .ToListAsync();
        }
        
        public async Task<Publication> FindByPetId(int petId)
        {
            return await _context.Publications.Where(p => p.PetId == petId)
                .FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<object>> ListPublicationsInfoPetsAsync()
        {
            var query = from pu in _context.Publications
                join pe in _context.Pets on pu.PetId equals pe.Id
                select new
                {
                    userId = pu.UserId,
                    publicationId = pu.Id,
                    petId = pe.Id,
                    type = pe.Type,
                    image = pe.UrlToImage,
                    name = pe.Name,
                    comment = pu.Comment,
                };
            return await query.ToListAsync();
        }

        public async Task<IEnumerable<object>> ListPublicationsInfoPetsAsyncByUserId(int id)
        {
            var query = from pu in _context.Publications
                join pe in _context.Pets on pu.PetId equals pe.Id
                select new
                {
                    userId = pu.UserId,
                    publicationId = pu.Id,
                    petId = pe.Id,
                    type = pe.Type,
                    image = pe.UrlToImage,
                    name = pe.Name,
                    comment = pu.Comment,
                };
            return await query.Where(p => p.userId == id).ToListAsync();
        }

    }
}