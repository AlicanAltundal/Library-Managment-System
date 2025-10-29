using LibraryManagement.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagement.Core.Application.Features.Books.Queries.GetAllBooks
{

    public class GetAllBooksQueryResponse
    {
        public int Id { get; set; }
        public string ISBN { get; set; }
        public string Title { get; set; }
        public string Genre { get; set; }
        public DateTime PublicationDate { get; set; }

        public string PublisherName { get; set; }

        // Relationships
        public int PublisherId { get; set; }

        public List<string> Authors { get; set; } = new();
    }
}
