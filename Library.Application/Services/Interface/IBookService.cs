using Library.Application.DTOs;
using Library.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Application.Services.Interface
{
    public interface IBookService
    {

        Task<BookDto> AddBookAsync(CreateBookDto createBookDto);

        Task<IEnumerable<BookDto>> GetAllBooksAsync();

        Task<IEnumerable<BookDto>> GetAvailableBooksAsync();

        Task<BookDto> UpdateBookAsync(int id, UpdateBookDto updateBookDto);

        Task<BookDto?> GetBookByIdAsync(int id);

        Task<bool> RemoveBookAsync(int id);
    }
}
