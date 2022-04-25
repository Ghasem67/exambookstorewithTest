using BookStore.Entities;
using BookStore.Infrastructure.Application;
using BookStore.Infrastructure.Test;
using BookStore.Persistence.EF;
using BookStore.Persistence.EF.Books;
using BookStore.Services.Books;
using BookStore.Services.Books.Contract;
using BookStore.test.tools;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace BookStore.Services.Test.Unit.Books
{
    public class BookServiceTests
    {
        private readonly EFDataContext _dbContext;
        private readonly UnitOfWork _unitOfWork;
        private readonly BookRepository _bookRepository;
        private readonly BookService _sut;
        public BookServiceTests()
        {
            _dbContext =
                 new EFInMemoryDatabase()
                 .CreateDataContext<EFDataContext>();
            _unitOfWork = new EFUnitOfWork(_dbContext);
            _bookRepository = new EFBookRepository(_dbContext);
            _sut = new bookAppService(_unitOfWork, _bookRepository);

        }
        [Fact]
        public void Add_adds_book_property()
        {
            var category = CreateFactory.Create("jenaee");
            _dbContext.Manipulate(_ => _.Categories.Add(category));

            CreateBookDTO createBookDTO = new CreateBookDTO
            {
                Title = "book1",
                Author = "author1",
                Description = "salam",
                Pages = 52,
                CategoryId = category.Id
            };
            _sut.Add(createBookDTO);
            var excepted = _dbContext.Books.FirstOrDefault();
            excepted.Title.Should().Be(createBookDTO.Title);
            excepted.Author.Should().Be(createBookDTO.Author);
            excepted.CategoryId.Should().Be(createBookDTO.CategoryId);
            excepted.Description.Should().Be(createBookDTO.Description);
            excepted.Pages.Should().Be(createBookDTO.Pages);
        }
        [Fact]
        public void update_book_update_property()
        {
            var category = CreateFactory.Create("jenaee");
            _dbContext.Manipulate(_ => _.Categories.Add(category));

            Book NewBook = new Book
            {
                Author = "Ali",
                CategoryId = category.Id,
                Description = "salman",
                Pages = 13,
                Title = "jenaee"
            };
            _dbContext.Manipulate(_ => _.Books.Add(NewBook));
            CreateBookDTO bookDTO = new CreateBookDTO
            {
                Author = "ali",
                Description = "salam",
                Pages = 12,
                Title = "mardan",
                CategoryId = category.Id
            };
            Book book = _dbContext.Books.FirstOrDefault();
            var books = book;
            _sut.Update(bookDTO, books.Id);
            _dbContext.Books.Should().Contain(x => x.Pages == bookDTO.Pages);
            _dbContext.Books.Should().Contain(x => x.Title == bookDTO.Title);
            _dbContext.Books.Should().Contain(x => x.Author == bookDTO.Author);
            _dbContext.Books.Should().Contain(x => x.Id == books.Id);
        }
        [Fact]
        public void Delete_category_inMemory()
        {
            var category = CreateFactory.Create("jenaee");
            _dbContext.Manipulate(_ => _.Categories.Add(category));

            Book NewBook = new Book
            {
                Author = "Ali",
                CategoryId = category.Id,
                Description = "salman",
                Pages = 13,
                Title = "jenaee"
            };
            _dbContext.Manipulate(_ => _.Books.Add(NewBook));
            _sut.Remove(NewBook.Id);
            _dbContext.Books.Should().HaveCount(0);
        }
        [Fact]
        public void GetAll()
        {
            var category = CreateFactory.Create("jenaee");
            _dbContext.Manipulate(_ => _.Categories.Add(category));
            Book NewBook = new Book
            {
                Author = "Ali",
                CategoryId = category.Id,
                Description = "salman",
                Pages = 13,
                Title = "jenaee"
            };
            _dbContext.Manipulate(_ => _.Books.Add(NewBook));
            
            _sut.GetAll().Should().HaveCount(1);
        }
    }
}
