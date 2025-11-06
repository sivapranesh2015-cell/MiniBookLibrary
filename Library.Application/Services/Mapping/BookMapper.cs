using Library.Application.DTOs;
using Library.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Application.Services.Mapping
{
    public static class BookMapper
    {

        public static BookDto Map(Book b) => new BookDto(b.Id, b.Title, b.Author, b.IsAvailable);
    }
}
