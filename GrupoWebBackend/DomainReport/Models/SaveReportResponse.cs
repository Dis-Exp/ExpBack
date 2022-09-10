using GrupoWebBackend.DomainDistrict.Domain.Services.Communications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GrupoWebBackend.DomainReport
{
    public class SaveReportResponse : BaseResponseA
    {
        public Report Report { get; private set; }
        public SaveReportResponse(Report report) : this(true, string.Empty, report)
        {
        }
        public SaveReportResponse(bool succces, string message, Report report) : base(succces, message)
        {
            Report = report;
        }

        public SaveReportResponse(string message) : this(true, message, null)
        {
        }
    }
}
