using GrupoWebBackend.Shared.Persistence.Context;
using GrupoWebBackend.Shared.Persistence.Repositories;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GrupoWebBackend.DomainReport
{
    public class ReportRepository : BaseRepository, IReportRepository
    {
        public ReportRepository(AppDbContext context) : base(context)
        {
        }

        public async Task AddAsync(Report report)
        {
            await _context.Reports.AddAsync(report);
        }

        public async Task<Report> FindAsync(int id)
        {
            return await _context.Reports.FindAsync();
        }

        public IEnumerable<Report> List()
        {
            return _context.Reports.ToList();
        }
    }
}
