using BookStore.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.test.tools
{
    public static class BookFactory
    {
        public static Book Create(Book book
            )
        {
            return new Book
            {
                Title = book.Title,
                Author =book.Author,
                CategoryId=book.CategoryId,
                Description=book.Description,
                Pages=book.Pages
            };
        }
    }
}
