using LibraryManagement.Core.Domain.Common;

namespace LibraryManagement.Core.Domain.Entities
{
    public class Librarian : EntityBase
    {
        public string EmployeeCode { get; set; }
        public string FullName { get; set; }
        public string Department { get; set; }

        // Relationships
        public ICollection<Loan> Loans { get; set; } = new List<Loan>();
    }
}
