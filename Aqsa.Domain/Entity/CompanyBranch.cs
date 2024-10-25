using Aqsa.Domain.Entity.Enums;

namespace Aqsa.Domain.Entity
{
    public class CompanyBranch : BaseEntity
    {
        public int CompanyId { get;private set; }
        public string Name { get; private set; } = default!;
        public double Lat { get; private set; } = default!;
        public double Lng { get; private set; } = default!;
        public CompanyBranchStatus Status { get;private set; }

        public Company Company { get; private set; } = default!;
        public IReadOnlyCollection<CompanyBranchUser> CompanyBranchUsers => _CompanyBranchUsers;

        private readonly List<CompanyBranchUser> _CompanyBranchUsers = new List<CompanyBranchUser>();

        private CompanyBranch()
        {
            
        }

        private CompanyBranch(int companyId, string name, double lat, double lng)
        {
            CompanyId = companyId;
            Name = name;
            Lat = lat;
            Lng = lng;
        }
    }
}
