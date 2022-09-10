using GrupoWebBackend.Security.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GrupoWebBackend.DomainReport
{
    public class Report
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }

        public string Type { get; set; }
        public string Description { get; set; }
        public string DateTime { get; set; }

    }
}
