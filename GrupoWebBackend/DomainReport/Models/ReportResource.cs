using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GrupoWebBackend.DomainReport.Models
{
    public class ReportResource
    {
        public int UserId { get; set; }
        public string? Type { get; set; }
        public string? Description { get; set; }
        public string? DateTime { get; set; }

    }
}
