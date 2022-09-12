using System;

namespace GrupoWebBackend.DomainSubscriptions.Resources
{
    public class SubscriptionResource
    {
        public int Id { get; set; }
        public int NumPosts { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public bool Expired { get; set; }

        // Relationships
        public int UserId { get; set; }
    }
}