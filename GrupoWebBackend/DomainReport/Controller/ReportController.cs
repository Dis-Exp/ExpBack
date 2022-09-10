using AutoMapper;
using GrupoWebBackend.DomainReport.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GrupoWebBackend.DomainReport
{
    [ApiController]
    [Route("/api/v1/[controller]")]
    public class ReportController : ControllerBase
    {
        private readonly IReportService _reportService;
        private readonly IMapper _mapper;

        public ReportController(IMapper mapper, IReportService reportService)
        {
            _reportService = reportService;
            _mapper = mapper;
        }

        [HttpGet]
        public  IEnumerable<Report> ListAsync()
        {
            var reports =  _reportService.List();
            
            return reports;
        }

        [HttpGet("{id:int}")]
        public async Task<Report> FindAsync(int id)
        {
            var reports = _reportService.FindAsync(id);

            return await reports;
        }

        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody] ReportResource report)
        {
            var _report = _mapper.Map<ReportResource, Report>(report);

            var result = await _reportService.AddAsync(_report);
            if (!result.Succces)
                return BadRequest(result.Message);

            return Ok(report);
        }

    }
}
