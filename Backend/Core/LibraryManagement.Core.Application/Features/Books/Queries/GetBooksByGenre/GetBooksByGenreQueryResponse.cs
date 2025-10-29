using System;

namespace LibraryManagement.Core.Application.Features.Books.Queries.GetBooksByGenre
{
    public class GetBooksByGenreQueryResponse
    {
        public string ISBN { get; set; }
        public string Title { get; set; }
        public string Genre { get; set; }
        public string PublisherName { get; set; }
        public DateTime PublicationDate { get; set; }
    }
}
