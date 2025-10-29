using LibraryManagement.Core.Domain.Common;

namespace LibraryManagement.Core.Domain.Entities
{
    public class Loan : EntityBase
    {
        public DateTime LoanDate { get; set; }
        public DateTime DueDate { get; set; }
        public DateTime? ReturnDate { get; set; }

        // Relationships
        public int BookId { get; set; }
        public Book Book { get; set; }

        public int MemberId { get; set; }
        public Member Member { get; set; }

        public int LibrarianId { get; set; }
        public Librarian Librarian { get; set; }
    }
}
