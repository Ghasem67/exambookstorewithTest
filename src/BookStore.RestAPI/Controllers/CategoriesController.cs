using BookStore.Services.Categories.Contracts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace BookStore.RestAPI.Controllers
{
    [Route("api/catgeories")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly CategoryService _service;
        public CategoriesController(CategoryService service)
        {
            _service = service;
        }

        [HttpPost]
        public void Add(AddCategoryDto dto)
        {
            _service.Add(dto);
        }
        [HttpPut("{id}")]
        public void Edit(AddCategoryDto dto,int id)
        {
            _service.Update(dto,id);
        }
        [HttpDelete]
        public void Delete( int id)
        {
            _service.Delete(id);
        }
        [HttpGet]
        public IList<GetCategoryDto> GettAll(int id)
        {
           return _service.GetAll();
        }
    }
}
