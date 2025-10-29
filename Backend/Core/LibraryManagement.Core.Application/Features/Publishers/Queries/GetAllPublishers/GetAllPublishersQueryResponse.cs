using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagement.Core.Application.Features.Publishers.Queries.GetAllPublishers
{
    public class GetAllPublishersQueryResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }

        public int BookCount { get; set; } // 🔥 eklendi
    }
}
