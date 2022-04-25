using BookStore.Services.Books.Contract;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookStore.RestAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookController : ControllerBase
    {
        private readonly BookService _bookService;

        public BookController(BookService bookService)
        {
            _bookService = bookService;
        }

        [HttpGet]
        public void GetAll()
        {
            _bookService.GetAll();
        }
        [HttpPost]
        public void Add(CreateBookDTO createBookDTO)
        {
            _bookService.Add(createBookDTO);
        }
        [HttpPut("{id}")]
        public void Edit(CreateBookDTO createBookDTO,int id)
        {
            _bookService.Update(createBookDTO,id);
        }
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            _bookService.Remove(id);
        }
    }
}
