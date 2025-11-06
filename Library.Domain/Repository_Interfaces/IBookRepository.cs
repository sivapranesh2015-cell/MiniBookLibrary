using Library.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Domain.Repository_Interfaces
{
    public interface IBookRepository
    {

        Task<Book> AddBookAsync(Book book);
        Task<Book?> GetBookByIdAsync(int id);
        Task<IEnumerable<Book>> GetAllBooksAsync();
        Task<IEnumerable<Book>> GetAvailableAsync();
        Task<Book?> UpdateBookAsync(Book book);
        Task<bool> DeleteBookAsync(int id);

    }
}
