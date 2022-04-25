using BookStore.Entities;
using BookStore.Infrastructure.Application;
using BookStore.Services.Books.Contract;
using BookStore.Services.Books.Exceptions;
using BookStore.Services.Categories.Contracts;
using BookStore.Services.Categories.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Services.Books
{
    public class bookAppService : BookService
    {
        private readonly BookRepository _bookRepository;
        private readonly CategoryRepository _categoryRepository;
        private readonly UnitOfWork _unitOfWork;

        public bookAppService(UnitOfWork unitOfWork, BookRepository bookRepository, CategoryRepository categoryRepository)
        {
            _unitOfWork = unitOfWork;
            _bookRepository = bookRepository;
            _categoryRepository = categoryRepository;
        }

        public void Add(CreateBookDTO createBookDTO)
        {
          var category=  _categoryRepository.GetbyId(createBookDTO.CategoryId);
            if (category==null)
            {
                throw new CategoryNotExistException();
            }
          Book book = new Book() 
          {
          Author = createBookDTO.Author,
          CategoryId = createBookDTO.CategoryId,
          Description= createBookDTO.Description,
          Pages=createBookDTO.Pages,
          Title= createBookDTO.Title,
          };
            _bookRepository.Add(book);
            _unitOfWork.Commit();
        }

        public HashSet<Book> GetAll()
        {
         return  _bookRepository.GetAll();
        }

        public Book GetbyId(int id)
        {
          return _bookRepository.GetById(id);
        }

        public void Remove(int bookId)
        {
          var book=  _bookRepository.GetById(bookId);
            if (book==null)
            {
                throw new BooknotexistExeption();
            }
            _bookRepository.Delete(book);
            _unitOfWork.Commit();
        }

        public void Update(UpdatebooksDTO book, int id)
        {
            var oneBook = _bookRepository.GetById(id);
            if (oneBook == null)
            {
                throw new BooknotexistExeption();
            }
            var category = _categoryRepository.GetbyId(book.CategoryId);
            if (category == null)
            {
                throw new CategoryNotExistException();
            }
            oneBook.Author = book.Author;
                oneBook.CategoryId = book.CategoryId;
                oneBook.Description = book.Description;
                oneBook.Pages = book.Pages;
                oneBook.Title = book.Title;
                _unitOfWork.Commit();
            
        }
    }
}
