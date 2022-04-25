using BookStore.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Persistence.EF.Books
{
    public class BookEntityMap : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
                builder.ToTable("Books");

                builder.HasKey(_ => _.Id);
                builder.Property(_ => _.Id)
                    .ValueGeneratedOnAdd();
        }
    }
}
