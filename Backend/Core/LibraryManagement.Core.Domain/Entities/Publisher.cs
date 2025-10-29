using LibraryManagement.Core.Domain.Common;

namespace LibraryManagement.Core.Domain.Entities
{
    public class Publisher : EntityBase
    {
        public string Name { get; set; }
        public string Address { get; set; }

        // Relationships
        public ICollection<Book> Books { get; set; } = new List<Book>();
    }
}
