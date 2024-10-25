using Aqsa.Domain.Entity.Enums;

namespace Aqsa.Domain.Entity
{
    public class CompanyUser:BaseEntity
    {
        public int CompanyId { get; set; }
        public int UserId { get; set; }
        public DateTimeOffset JoinAt { get; set; }
        public CompanyUserStatus Status { get; set; }
        public CompanyUserAccessLevel AccessLevel { get; set; }

        public Company Company { get; set; }=default!;
        public User User { get; set; }=default!;
    }
}
