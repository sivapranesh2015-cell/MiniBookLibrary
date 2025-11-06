using Library.Application.DTOs;
using Library.Application.Services.Implementation;
using Library.Infrastructure.Repositories;
using System.Threading.Tasks;

namespace Library.Application.Tests
{
    public class BookServiceTests
    {
        [Fact]
        public async Task AddBook_ShouldReturnAddedBook()
        {
            var repo = new InMemoryBookRepository();
            var service = new BookService(repo);
            var requestDto = new CreateBookDto
            {
                Title = "ASP.NET Black Book",
                Author = "Mike"
               
            };
            var addedBook = await service.AddBookAsync(requestDto);
            Assert.NotNull(addedBook);
            Assert.Equal("ASP.NET Black Book",addedBook.Title);
            Assert.Equal("Mike", addedBook.Author);

        }
        [Fact]

        public async Task GetAvailableBooks_ShouldReturnOnlyAvailable()
        {
            var repo = new InMemoryBookRepository();
            var service = new BookService(repo);


            var availableBooks = (await service.GetAvailableBooksAsync()).ToList();

            Assert.NotNull(availableBooks);
            Assert.All(availableBooks, b => Assert.True(b.IsAvailable));
        
        }


        [Fact]
        public async Task UpdateBookAsync_ShouldUpdateExistingBook()
        {
            var repo = new InMemoryBookRepository();
            var service = new BookService(repo);
            var createRequest = new CreateBookDto { Title = "Test Old Title", Author = "Test Old Author",IsAvailable=true};
            var addedBook = await service.AddBookAsync(createRequest);
            var updateBookRequest = new UpdateBookDto { Title = "New Title", Author = "New Author", IsAvailable = true };
            var updatedBook = await service.UpdateBookAsync(addedBook.Id, updateBookRequest);
            
            Assert.NotNull(addedBook);

            Assert.Equal("New Title", updatedBook.Title);

            Assert.True(updatedBook.IsAvailable);


        }
        [Fact]
        public async Task DeleteBookAsync_ShouldDeleteExistingBook()
        {

            var repo = new InMemoryBookRepository();
            var service = new BookService(repo);

            var createRequest = new CreateBookDto { Title = "Book To be deleted", Author = "Test Author", IsAvailable = false };
            var addedBook = await service.AddBookAsync(createRequest);


            var result =await repo.DeleteBookAsync(addedBook.Id);
            
            Assert.True(result);
        
        
        
        }


    }
}