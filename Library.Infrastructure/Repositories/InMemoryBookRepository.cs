using Library.Domain.Entities;
using Library.Domain.Repository_Interfaces;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Infrastructure.Repositories
{
    public class InMemoryBookRepository : IBookRepository
    {

        private readonly ConcurrentDictionary<int, Book> _bookStore = new();


        public InMemoryBookRepository()
        {
            var initialValues = new List<Book>
            {
                new (){Id=1, Title="Mastering Design Patterns" , Author="John" ,IsAvailable= true },
                new (){ Id=2, Title= "Design APIs using .NET Core",Author="Rick", IsAvailable= true},
                new (){ Id=3,Title="Learn ASP.NET MVC" , Author="William", IsAvailable= true},
                new (){ Id=4, Title="Master Cloud computing", Author="Steve", IsAvailable= true},
                new (){ Id=5, Title="Learn SQL server", Author="Mark", IsAvailable= false}


            };

            foreach (var book in initialValues)
            {
                _bookStore[book.Id] = book;
            }

        }

        public Task<Book> AddBookAsync(Book book)
        {
       
            _bookStore[book.Id] = book;
            return Task.FromResult(book);
            
        }

        public Task<bool> DeleteBookAsync(int id)
        {
            var toBeRemoved = _bookStore.TryRemove(id, out _);
            return Task.FromResult(toBeRemoved);
        }

        public Task<IEnumerable<Book>> GetAllBooksAsync()
        {
            return Task.FromResult<IEnumerable<Book>>(_bookStore.Values.ToList());
        }

        public Task<IEnumerable<Book>> GetAvailableAsync()
        {
            var availableBooks = _bookStore.Values.Where(b => b.IsAvailable).ToList();
            return Task.FromResult<IEnumerable<Book>>(availableBooks);
        }

        public Task<Book?> GetBookByIdAsync(int id)
        {
            _bookStore.TryGetValue(id, out var book);
            return Task.FromResult<Book?>(book);
        }

        public Task<Book?> UpdateBookAsync(Book book)
        {
            if (!_bookStore.ContainsKey(book.Id))
                return Task.FromResult<Book?>(null);
            _bookStore[book.Id] = book;
            return Task.FromResult<Book?>(book);
        }
    }
}
