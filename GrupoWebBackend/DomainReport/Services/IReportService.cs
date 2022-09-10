using GrupoWebBackend.DomainReport.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GrupoWebBackend.DomainReport
{
    public interface IReportService
    {
        IEnumerable<Report> List();
        Task<Report> FindAsync(int id);
        Task<SaveReportResponse> AddAsync(Report report);
    }
}
