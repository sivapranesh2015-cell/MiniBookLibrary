using Library.Application.DTOs;
using Library.Application.Services.Interface;
using Library.Application.Services.Mapping;
using Library.Domain.Entities;
using Library.Domain.Exceptions;
using Library.Domain.Repository_Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Application.Services.Implementation
{
    public class BookService : IBookService
    {

        private readonly IBookRepository _repo;
       
        public BookService(IBookRepository repo)
        {
            _repo = repo;
        }

        /// <summary>
        /// Below method is used to add a new book into the system
        /// </summary>
        /// <param name="bookDto"></param>
        /// <returns></returns>
        public async Task<BookDto> AddBookAsync(CreateBookDto bookDto)
        {
            var allBooks = await _repo.GetAllBooksAsync();

            var nextId = allBooks.Any() ? allBooks.Max(b => b.Id) + 1 : 1;
            var book = new Book(nextId, bookDto.Title.Trim(), bookDto.Author.Trim(), true);

           var added =await _repo.AddBookAsync(book);

            return BookMapper.Map(added);
        }

       //Below method will retrieve all the books from the In Memory repository

        public async Task<IEnumerable<BookDto>> GetAllBooksAsync()
        {
            try
            {
                var books = await _repo.GetAllBooksAsync();
                return books.Select(BookMapper.Map);
            }
            catch (Exception ex)
            {
               
                throw;
            }

        }
        //Below method will retrieve the books that are available in the In Memory repository
        public async Task<IEnumerable<BookDto>> GetAvailableBooksAsync()
        {
            try
            {
                var books = await _repo.GetAvailableAsync();
                return books.Select(BookMapper.Map);
            }
            catch (Exception)
            {

                throw;
            }
        }

        //Below method will retrieve the a single book based on the book ID
        public async Task<BookDto?> GetBookByIdAsync(int id)
        {
            var book = await _repo.GetBookByIdAsync(id);
            if(book is null)
                throw new NotFoundException(nameof(Book), id);
            
            return BookMapper.Map(book);
        }
        //Below method will remove the a single book based on the book ID
        public async Task<bool> RemoveBookAsync(int id)
        {
            var existingBook = await _repo.GetBookByIdAsync(id);

            if (existingBook is null)
                throw new Exception("Book with ID " + id + " Not found");
            return await _repo.DeleteBookAsync(id);
        }
        //Below method will update the a single book based on the book ID
        public async Task<BookDto> UpdateBookAsync(int id, UpdateBookDto updateBookDto)
        {
            var existingBook = await _repo.GetBookByIdAsync(id);
            if (existingBook is null)
                throw new Exception("Book with ID " + id + " Not found");

            existingBook.Title = updateBookDto.Title ?? existingBook.Title;
            existingBook.Author = updateBookDto.Author ?? existingBook.Author;
            existingBook.IsAvailable = updateBookDto.IsAvailable ?? existingBook.IsAvailable;
            var updatedBook = await _repo.UpdateBookAsync(existingBook) ?? throw new Exception("Failed to update Book");
            return BookMapper.Map(updatedBook);
        }

       
    }
}
