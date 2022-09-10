using GrupoWebBackend.Shared.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GrupoWebBackend.DomainReport
{
    public class ReportService : IReportService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IReportRepository _reportRespository;

        public ReportService(IReportRepository reportRespository, IUnitOfWork unitOfWork)
        {
            _reportRespository = reportRespository;
            _unitOfWork = unitOfWork;
        }

        public async Task<SaveReportResponse> AddAsync(Report report)
        {

            try
            {
                await _reportRespository.AddAsync(report);
                await _unitOfWork.CompleteAsync();
                return new SaveReportResponse(report);
            }
            catch (Exception e)
            {
                return new SaveReportResponse($"An error occurred while saving Report: {e.Message}");
            }
        }

        public async Task<Report> FindAsync(int id)
        {
            return await _reportRespository.FindAsync(id);
        }

        public IEnumerable<Report> List()
        {
            return _reportRespository.List();
        }
    }
}
