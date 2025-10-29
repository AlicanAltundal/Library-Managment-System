using LibraryManagement.Core.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagement.Core.Application.Features.Books.Command.CreateBook
{
    public class CreateBookCommandRequest : IRequest<Unit>
    {
        public string ISBN { get; set; }
        public string Title { get; set; }
        public string Genre { get; set; }
        public DateTime PublicationDate { get; set; }

        // Relationships
        public List<int> AuthorIds { get; set; } = new();
        public int PublisherId { get; set; }
    }
}
