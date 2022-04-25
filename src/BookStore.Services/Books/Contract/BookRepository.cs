using BookStore.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Services.Books.Contract
{
    public interface BookRepository
    {
        void Add(Book book);
       HashSet< Book> GetAll();
        Book GetById(int id);
        void Delete(Book book);
    }
}
