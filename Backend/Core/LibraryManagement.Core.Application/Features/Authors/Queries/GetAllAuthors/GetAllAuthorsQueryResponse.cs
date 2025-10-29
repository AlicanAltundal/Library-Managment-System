namespace LibraryManagement.Core.Application.Features.Authors.Queries.GetAllAuthors
{
    public class GetAllAuthorsQueryResponse
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        // 🔹 opsiyonel: yazarın yazdığı kitap sayısı
        public int BookCount { get; set; }
        public string FullName { get; set; }

    }
}
