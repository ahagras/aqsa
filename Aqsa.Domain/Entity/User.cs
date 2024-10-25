using Aqsa.Domain.Entity.Enums;

namespace Aqsa.Domain.Entity
{
    public class User : BaseEntity
    {
        public string Name { get; private set; } = default!;
        public string Email { get; private set; } = default!;
        public string Phone { get; private set; } = default!;
        public string Password { get; private set; } = default!;
        public UserStatus Status { get; private set; }
        public DateTimeOffset CreatAt { private get; set; }

        public IReadOnlyCollection<CompanyUser> CompanyUsers => _CompanyUsers;

        private readonly List<CompanyUser> _CompanyUsers = new List<CompanyUser>();

        public IReadOnlyCollection<CompanyBranchUser> CompanyBranchUsers => _CompanyBranchUsers;

        private readonly List<CompanyBranchUser> _CompanyBranchUsers = new List<CompanyBranchUser>();
        private User()
        {
            Status = UserStatus.Unknown;
        }

        public User(string name, string email, string phone, string password)
        {
            Name = name;
            Email = email;
            Phone = phone;
            Password = password;
        }
    }
} 
