using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GrupoWebBackend.DomainReport
{
    public interface IReportRepository
    {
        IEnumerable<Report> List();
        Task<Report> FindAsync(int id);
        Task AddAsync(Report pet);
    }
}
