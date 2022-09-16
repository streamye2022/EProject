using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DotNetCore.CAP;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Streamye.OrderServices.Dtos.Input;
using Microsoft.Streamye.OrderServices.Models;
using Microsoft.Streamye.OrderServices.Services;

namespace Microsoft.Streamye.OrderServices.Controllers
{
    public class OrderController : ControllerBase
    {
        private readonly IOrderService OrderService;
        private readonly IOrderItemService orderItemService;

        public OrderController(IOrderService OrderService,
            IOrderItemService orderItemService)
        {
            this.OrderService = OrderService;
            this.orderItemService = orderItemService;
        }

        // GET: api/Orders
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Order>>> GetOrders()
        {
            var result = await OrderService.GetOrdersAsync();
            return result.ToList();
        }

        // GET: api/Orders/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Order>> GetOrder(int id)
        {
            var Order = await OrderService.GetOrderByIdAsync(id);

            if (Order == null)
            {
                return NotFound();
            }

            return Order;
        }

        // PUT: api/Orders/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutOrder(int id, Order Order)
        {
            if (id != Order.Id)
            {
                return BadRequest();
            }

            try
            {
                await OrderService.UpdateAsync(Order);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await OrderExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Ok();
        }

        /// <summary>
        /// 创建订单
        /// </summary>
        /// <param name="Order"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult<Order>> PostOrder(Order Order)
        {
            // 1、创建订单
            Order.Createtime = new DateTime();
            await OrderService.CreateAsync(Order);

            // 2、创建订单项
            foreach (var orderItem in Order.OrderItems)
            {
                await orderItemService.CreateAsync(orderItem);
            }

            return CreatedAtAction("GetOrder", new { id = Order.Id }, Order);
        }

        // 创建订单
        [NonAction]
        [CapSubscribe("seckill.order.create")]
        public async Task<ActionResult<Order>> CapPostOrder(Order Order)
        {
            Order.Createtime = new DateTime();
            await OrderService.CreateAsync(Order);

            return CreatedAtAction("GetOrder", new { id = Order.Id }, Order);
        }


        // 订单取消
        [HttpPut("Cancel")]
        public async Task<IActionResult> CancelOrder(OrderCancelDto orderCancelDto)
        {
            return Ok("订单取消成功");
        }

        // DELETE: api/Orders/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Order>> DeleteOrder(int id)
        {
            var Order = await OrderService.GetOrderByIdAsync(id);
            if (Order == null)
            {
                return NotFound();
            }

            await OrderService.DeleteAsync(Order);
            return Order;
        }

        private async Task<bool> OrderExists(int id)
        {
            return await OrderService.OrderExistsAsync(id);
        }
    }
}