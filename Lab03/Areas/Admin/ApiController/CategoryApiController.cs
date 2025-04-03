using Lab03.Models;
using Lab03.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Lab03.Areas.Admin.ApiController
{
    [Route("api/admin/categories")]
    [ApiController]
    // [Authorize(Roles = "Admin")]
    public class CategoryApiController : ControllerBase
    {
        private readonly ICategoryRepository _categoryRepository;

        public CategoryApiController(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Category>>> GetCategories()
        {
            var categories = await _categoryRepository.GetAllAsync();
            return Ok(categories);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Category>> GetCategory(int id)
        {
            var category = await _categoryRepository.GetByIdAsync(id);
            if (category == null)
            {
                return NotFound();
            }
            return Ok(category);
        }

        [HttpPost]
        public async Task<IActionResult> CreateCategory(Category category)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await _categoryRepository.AddAsync(category);

            return CreatedAtAction(nameof(GetCategory), new { id = category.Id },
                new
                {
                    message = "Thêm danh mục thành công",
                    data = category
                }
            );
        }
   

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCategory(int id, Category category)
        {
            if (id != category.Id)
            {
                return BadRequest(new { message = "ID không khớp" });
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await _categoryRepository.UpdateAsync(category);

            return Ok(new { message = "Sửa danh mục thành công", data = category });
        }


        [HttpDelete("{id}")]
public async Task<IActionResult> DeleteCategory(int id)
{
    var category = await _categoryRepository.GetByIdAsync(id);
    if (category == null)
    {
        return NotFound(new { message = "Danh mục không tồn tại" });
    }

    await _categoryRepository.DeleteAsync(id);

    return Ok(new { message = "Xóa danh mục thành công" });
}

    }
}