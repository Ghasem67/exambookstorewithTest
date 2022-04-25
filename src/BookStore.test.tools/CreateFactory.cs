using BookStore.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.test.tools
{
    public static class CreateFactory
    {
        public static Category Create(string title)
        {
            return new Category
            {
                Title = title
            };
        }
    }
}
