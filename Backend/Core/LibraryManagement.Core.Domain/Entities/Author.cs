using LibraryManagement.Core.Domain.Common;

namespace LibraryManagement.Core.Domain.Entities
{
    public class Author : EntityBase
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }

        // Relationships
        public ICollection<BookAuthor> BookAuthors { get; set; } = new List<BookAuthor>();
    }
}
