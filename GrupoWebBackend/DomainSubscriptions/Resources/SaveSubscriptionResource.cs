using System;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace GrupoWebBackend.DomainSubscriptions.Resources
{
    public class SaveSubscriptionResource
    {
        [Required]
        public int NumPosts { get; set; }
        [Required]
        public DateTime StartDate { get; set; }
        [Required]
        public DateTime EndDate { get; set; }
        [Required]
        public bool Expired { get; set; }
        [Required]
        public int UserId { get; set; }
    }
}