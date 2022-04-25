using BookStore.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Services.Books.Contract
{
    public interface BookService
    {
        void Add(CreateBookDTO book);
        void Remove(int bookId);
        void Update(CreateBookDTO book, int id);
        Book GetbyId(int id);
        HashSet<Book> GetAll();
    }
}
