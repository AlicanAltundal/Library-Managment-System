using LibraryManagement.Core.Domain.Common;

namespace LibraryManagement.Core.Domain.Entities
{
    public class Member : EntityBase
    {
        public string MemberCode { get; set; }
        public string FullName { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }

        public string Role { get; set; } = "User"; // "Admin" veya "User"

        public string PasswordHash { get; set; }    // BCrypt hash (never store plaintext)
        public bool IsLocked { get; set; } = false; // optional: brute-force protection

        // Relationships
        public ICollection<Loan> Loans { get; set; } = new List<Loan>();
    }
}
