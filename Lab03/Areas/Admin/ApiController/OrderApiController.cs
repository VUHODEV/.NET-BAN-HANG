// using Microsoft.AspNetCore.Authorization;
// using Microsoft.AspNetCore.Mvc;
// using Lab03.Models;
// using Lab03.Repositories;
// using System.Collections.Generic;
// using System.Threading.Tasks;

// namespace Lab03.Areas.Admin.Controllers
// {
//     [Route("api/admin/orders")]
//     [ApiController]
//     // [Authorize(Roles = "Admin")]
//     public class OrderApiController : ControllerBase
//     {
//         private readonly IOrderRepository _orderRepository;

//         public OrderApiController(IOrderRepository orderRepository)
//         {
//             _orderRepository = orderRepository;
//         }

//         // Lấy danh sách đơn hàng
//         [HttpGet]
//         public async Task<IActionResult> GetAll()
//         {
//             var orders = await _orderRepository.GetAllAsync();
//             return Ok(orders);
//         }

//         // Lấy thông tin chi tiết đơn hàng theo ID
//         [HttpGet("{id}")]
//         public async Task<IActionResult> GetById(int id)
//         {
//             var order = await _orderRepository.GetByIdAsync(id);
//             if (order == null)
//             {
//                 return NotFound();
//             }
//             return Ok(order);
//         }

//         // Thêm đơn hàng mới
//         [HttpPost]
//         public async Task<IActionResult> Add([FromBody] Order model)
//         {
//             if (!ModelState.IsValid)
//             {
//                 return BadRequest(ModelState);
//             }

//             await _orderRepository.AddAsync(model);
//             return CreatedAtAction(nameof(GetById), new { id = model.Id }, model);
//         }

//         // Cập nhật đơn hàng
//         [HttpPut("{id}")]
//         public async Task<IActionResult> Update(int id, [FromBody] Order model)
//         {
//             if (id != model.Id)
//             {
//                 return BadRequest();
//             }

//             var existingOrder = await _orderRepository.GetByIdAsync(id);
//             if (existingOrder == null)
//             {
//                 return NotFound();
//             }

//             existingOrder.CustomerName = model.CustomerName;
//             existingOrder.TotalAmount = model.TotalAmount;
//             existingOrder.Status = model.Status;
//             existingOrder.OrderDate = model.OrderDate;

//             await _orderRepository.UpdateAsync(existingOrder);
//             return Ok(existingOrder);
//         }

//         // Xóa đơn hàng
//         [HttpDelete("{id}")]
//         public async Task<IActionResult> Delete(int id)
//         {
//             var order = await _orderRepository.GetByIdAsync(id);
//             if (order == null)
//             {
//                 return NotFound();
//             }

//             await _orderRepository.DeleteAsync(id);
//             return NoContent();
//         }
//     }
// }
