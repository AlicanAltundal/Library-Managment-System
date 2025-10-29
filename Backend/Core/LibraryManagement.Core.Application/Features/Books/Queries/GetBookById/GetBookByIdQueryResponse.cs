using System;
using System.Collections.Generic;

namespace LibraryManagement.Core.Application.Features.Books.Queries.GetBookById
{
    public class GetBookByIdQueryResponse
    {
        public string ISBN { get; set; }
        public string Title { get; set; }
        public string Genre { get; set; }
        public DateTime PublicationDate { get; set; }

        public string PublisherName { get; set; }
        public List<string> Authors { get; set; } = new();
    }
}
