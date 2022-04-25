using BookStore.Entities;
using BookStore.Services.Books.Contract;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Persistence.EF.Books
{
    public class EFBookRepository : BookRepository
    {
        private readonly EFDataContext _fDataContext;

        public EFBookRepository(EFDataContext eFDataContext)
        {
            _fDataContext = eFDataContext;
        }

        public void Add(Book book)
        {
            _fDataContext.Books.Add(book);
        }

        public void Delete(Book book)
        {
            _fDataContext.Books.Remove(book);
        }

        public HashSet< Book> GetAll()
        {
           return _fDataContext.Books.ToHashSet();
        }

        public Book GetById(int id)
        {
            return _fDataContext.Books.FirstOrDefault(x => x.Id.Equals(id));
        }
    }
}
