namespace Aqsa.Domain.Entity
{
    public class CompanyBranchUser:BaseEntity
    {
        public int BranchId { get; set; }
        public int UserId { get; set; }
        public int CompanyId { get; set; }

        public CompanyBranch Branch { get; set; } = default!;
        public User User { get; set; }=default!;
        public Company Company{ get; set; }=default!;
    }
}
