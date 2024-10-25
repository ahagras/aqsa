using Aqsa.Domain.Entity.Enums;

namespace Aqsa.Domain.Entity;

public class Company : BaseEntity
{
    public string Name { get; private set; } = default!;
    public string Description { get; private set; } = default!;
    public string Phone { get; private set; } = default!;
    public CompanyStatus Status { get; private set; } = default!;
    public double Lat { get; private set; } = default!;
    public double Lng { get; private set; } = default!;

    public IReadOnlyCollection<CompanyBranch> Branches => _Branches;

    private readonly List<CompanyBranch> _Branches = new List<CompanyBranch>();
    public IReadOnlyCollection<CompanyUser> CompanyUsers => _CompanyUsers;

    private readonly List<CompanyUser> _CompanyUsers = new List<CompanyUser>();
    public IReadOnlyCollection<CompanyBranchUser> CompanyBranchUsers => _CompanyBranchUsers;

    private readonly List<CompanyBranchUser> _CompanyBranchUsers = new List<CompanyBranchUser>();

    private Company()
    {
    }

    public Company(string name, string description, string phone)
    {
        Name = name;
        Description = description;
        Phone = phone;
        Status = CompanyStatus.Unknown;
    }
}
