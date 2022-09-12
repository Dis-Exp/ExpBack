using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using GrupoWebBackend.DomainPublications.Domain.Models;
using GrupoWebBackend.Security.Domain.Entities;

namespace GrupoWebBackend.DomainSubscriptions.Domain.Models
{
    public class Subscription
    {
        public int Id { get; set; }
        public int NumPosts { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public bool Expired { get; set; }

        // Relationships
        public int UserId { get; set; }
        public User User { get; set; }
    }
}
