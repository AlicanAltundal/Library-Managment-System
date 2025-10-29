using System.Collections.Generic;

namespace LibraryManagement.Core.Application.Features.Search.Queries
{
    public class SearchQueryResponse
    {
        public List<BookSearchDto> Books { get; set; } = new();
        public List<AuthorSearchDto> Authors { get; set; } = new();
        public List<MemberSearchDto> Members { get; set; } = new();
    }

    public record BookSearchDto(int Id, string Title);
    public record AuthorSearchDto(int Id, string Firstname, string Lastname);
    public record MemberSearchDto(int Id, string FullName);

}
