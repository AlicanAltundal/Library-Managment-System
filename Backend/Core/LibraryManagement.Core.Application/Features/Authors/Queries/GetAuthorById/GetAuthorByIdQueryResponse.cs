using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagement.Core.Application.Features.Authors.Queries.GetAuthorById
{
    public class GetAuthorByIdQueryResponse
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }

        // 🔹 opsiyonel: yazarın yazdığı kitap sayısı
        public int BookCount { get; set; }
    }
}
