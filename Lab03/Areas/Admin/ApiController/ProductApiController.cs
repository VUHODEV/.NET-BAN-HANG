    using Lab03.Repositories;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Lab03.Models;
    using System.Collections.Generic;
    using System.IO;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Http;

    namespace Lab03.Areas.Admin.ApiController
    {
        [Route("api/admin/products")]
        [ApiController]
        // [Authorize(Roles = "Admin")]
        public class ProductApiController : ControllerBase
        {
            private readonly IProductRepository _productRepository;
            private readonly ICategoryRepository _categoryRepository;

            public ProductApiController(IProductRepository productRepository, ICategoryRepository categoryRepository)
            {
                _productRepository = productRepository;
                _categoryRepository = categoryRepository;
            }

            /// <summary>
            /// Lấy danh sách tất cả sản phẩm.
            /// </summary>
            /// <returns>Danh sách sản phẩm.</returns>
            [HttpGet]
            public async Task<IActionResult> GetAll()
            {
                var products = await _productRepository.GetAllAsync();
                return Ok(products);
            }

            /// <summary>
            /// Lấy thông tin chi tiết của một sản phẩm.
            /// </summary>
            /// <param name="id">ID của sản phẩm.</param>
            /// <returns>Thông tin sản phẩm.</returns>
            [HttpGet("{id}")]
            public async Task<IActionResult> GetById(int id)
            {
                var product = await _productRepository.GetByIdAsync(id);
                if (product == null)
                {
                    return NotFound();
                }
                return Ok(product);
            }

            // Thêm sản phẩm mới
            [HttpPost]
            public async Task<IActionResult> Add([FromBody] Product product)
            {
                if (string.IsNullOrEmpty(product.Name) || product.Price <= 0)
                {
                    return BadRequest(new { message = "Dữ liệu sản phẩm không hợp lệ." });
                }

                if (string.IsNullOrEmpty(product.ImgUrl))
                {
                    product.ImgUrl = "/image/default.png"; // Gán ảnh mặc định nếu thiếu
                }

                await _productRepository.AddAsync(product);

                return CreatedAtAction(nameof(GetById), new { id = product.Id }, new
                {
                    message = "Sản phẩm thêm thành công",
                    data = new
                    {
                        id = product.Id,
                        name = product.Name,
                        price = product.Price,
                        description = product.Description,
                        imgUrl = product.ImgUrl,
                        categoryId = product.CategoryId,
                        category = await _categoryRepository.GetByIdAsync(product.CategoryId)
                    }
                });
            }


            [HttpPut("{id}")]
            public async Task<IActionResult> Update(int id, [FromBody] Product product)
            {
                if (id != product.Id)
                {
                    return BadRequest();
                }

                var existingProduct = await _productRepository.GetByIdAsync(id);
                if (existingProduct == null)
                {
                    return NotFound();
                }

                if (string.IsNullOrEmpty(product.ImgUrl))
                {
                    product.ImgUrl = existingProduct.ImgUrl; // Giữ ảnh cũ nếu không có ảnh mới
                }

                existingProduct.Name = product.Name;
                existingProduct.Price = product.Price;
                existingProduct.Description = product.Description;
                existingProduct.CategoryId = product.CategoryId;
                existingProduct.ImgUrl = product.ImgUrl;

                await _productRepository.UpdateAsync(existingProduct);
                return Ok(existingProduct);
            }


            // Xóa sản phẩm
            [HttpDelete("{id}")]
            public async Task<IActionResult> Delete(int id)
            {
                var product = await _productRepository.GetByIdAsync(id);
                if (product == null)
                {
                    return NotFound();
                }

                await _productRepository.DeleteAsync(id);
               return Ok(new { message = "Sản phẩm đã được xóa thành công" });
            }

            // Lưu ảnh vào thư mục wwwroot
            private async Task<string> SaveImage(IFormFile imgUrl)
            {
                var savePath = Path.Combine("wwwroot/image", imgUrl.FileName);
                using (var fileStream = new FileStream(savePath, FileMode.Create))
                {
                    await imgUrl.CopyToAsync(fileStream);
                }
                return "/image/" + imgUrl.FileName;
            }
        }
    }
