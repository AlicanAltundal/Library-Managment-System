namespace LibraryManagement.Core.Application.Features.Books.Queries.GetGenres
{
    public class GetGenresQueryResponse
    {
        public string GenreName { get; set; }
        public int BookCount { get; set; }  // 💡 o türe ait kitap sayısı
    }
}
