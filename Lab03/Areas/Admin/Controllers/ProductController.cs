using Lab03.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Lab03.Models;

namespace Lab03.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class ProductController : Controller
    {
        private readonly IProductRepository _productRepository;
        private readonly ICategoryRepository _categoryRepository;

        public ProductController(IProductRepository productRepository, ICategoryRepository categoryRepository)
        {
            _productRepository = productRepository;
            _categoryRepository = categoryRepository;
        }

        public async Task<IActionResult> Index()
        {
            var products = await _productRepository.GetAllAsync();
            return View(products);
        }

        public async Task<IActionResult> Display(int id)
        {
            var product = await _productRepository.GetByIdAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            return View(product);
        }

        public async Task<IActionResult> Add()
        {
            var categories = await _categoryRepository.GetAllAsync();
            ViewBag.Categories = new SelectList(categories, "Id", "Name");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Add(Product product, IFormFile imgUrl)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (imgUrl == null || imgUrl.Length == 0)
                    {
                        return Json(new { success = false, message = "Product image is required" });
                    }

                    product.ImgUrl = await SaveImage(imgUrl);
                    await _productRepository.AddAsync(product);

                    return Json(new
                    {
                        success = true,
                        message = "Product added successfully",
                        redirect = Url.Action("Index")
                    });
                }

                var errors = ModelState.Values
                    .SelectMany(v => v.Errors)
                    .Select(e => e.ErrorMessage)
                    .ToList();

                return Json(new
                {
                    success = false,
                    message = "Validation errors",
                    errors = errors
                });
            }
            catch (Exception ex)
            {
                return Json(new
                {
                    success = false,
                    message = "Error adding product: " + ex.Message
                });
            }
        }

        public async Task<IActionResult> Edit(int id)
        {
            var product = await _productRepository.GetByIdAsync(id);
            if (product == null)
            {
                return NotFound();
            }

            var categories = await _categoryRepository.GetAllAsync();
            ViewBag.Categories = new SelectList(categories, "Id", "Name");
            return View(product);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Product product, IFormFile imgUrl)
        {
            try
            {
                if (id != product.Id)
                {
                    return Json(new { success = false, message = "ID mismatch" });
                }

                if (ModelState.IsValid)
                {
                    var existingProduct = await _productRepository.GetByIdAsync(id);
                    if (existingProduct == null)
                    {
                        return Json(new { success = false, message = "Product not found" });
                    }

                    if (imgUrl != null)
                    {
                        existingProduct.ImgUrl = await SaveImage(imgUrl);
                    }

                    // Cập nhật các thuộc tính khác
                    existingProduct.Name = product.Name;
                    existingProduct.Price = product.Price;
                    existingProduct.Description = product.Description;
                    existingProduct.CategoryId = product.CategoryId;

                    await _productRepository.UpdateAsync(existingProduct);

                    return Json(new
                    {
                        success = true,
                        message = "Product updated successfully",
                        redirect = Url.Action("Index")
                    });
                }

                var errors = ModelState.Values
                    .SelectMany(v => v.Errors)
                    .Select(e => e.ErrorMessage);

                return Json(new
                {
                    success = false,
                    message = "Validation errors",
                    errors = errors
                });
            }
            catch (Exception ex)
            {
                return Json(new
                {
                    success = false,
                    message = "Error updating product: " + ex.Message
                });
            }
        }


        public async Task<IActionResult> Delete(int id)
        {
            var product = await _productRepository.GetByIdAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            return View(product);
        }

        [HttpPost, ActionName("DeleteConfirmed")]
        public async Task<IActionResult> DelectConfirmed(int id)
        {
            await _productRepository.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }

        private async Task<string> SaveImage(IFormFile imgUrl)
        {
            if (imgUrl == null || imgUrl.Length == 0)
                return null;

            // Đảm bảo thư mục tồn tại
            var uploadFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Image");
            if (!Directory.Exists(uploadFolder))
            {
                Directory.CreateDirectory(uploadFolder);
            }

            // Đổi tên file tránh trùng lặp
            var fileName = Guid.NewGuid().ToString() + Path.GetExtension(imgUrl.FileName);
            var filePath = Path.Combine(uploadFolder, fileName);

            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                await imgUrl.CopyToAsync(fileStream);
            }

            return "/Image/" + fileName; // Trả về đường dẫn ảnh để lưu vào database
        }
    }
}
