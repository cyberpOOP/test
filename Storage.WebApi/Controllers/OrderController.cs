using Microsoft.AspNetCore.Mvc;
using Storage.BLL.DTO.Order;
using Storage.BLL.DTO.User;
using Storage.BLL.Services.Interfaces;
using System.Net;

namespace Storage.WebApi.Controllers
{
	[ApiController]
	[Route("/api/orders")]
	public class OrderController : ControllerBase
	{
		private readonly IOrderService _orderService;

        public OrderController(IOrderService orderService)
        {
			_orderService = orderService;
        }

		[HttpGet]
		public async Task<ActionResult> GetAll()
		{
			try
			{
				var orders = await _orderService.GetOrders();
				if (orders == null)
				{
					return BadRequest();
				}

				return Ok(orders);
			}
			catch (Exception ex)
			{
				return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
			}
		}

		[HttpGet("not-done")]
		public async Task<ActionResult> GetAllDone()
		{
			try
			{
				var orders = await _orderService.GetNotDoneOrders();
				if (orders == null)
				{
					return BadRequest();
				}

				return Ok(orders);
			}
			catch (Exception ex)
			{
				return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
			}
		}

		[HttpGet("{id}")]
		public async Task<ActionResult> GetById(long id)
		{
			try
			{
				var order = await _orderService.GetOrder(id);
				if (order == null)
				{
					return BadRequest();
				}

				return Ok(order);
			}
			catch (Exception ex)
			{
				return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
			}
		}

		[HttpGet("user")]
		public async Task<ActionResult> GetUserOrders([FromBody] UserEmailDto userEmailDto)
		{
			try
			{
				var orders = await _orderService.GetUserOrders(userEmailDto);
				if (orders == null)
				{
					return BadRequest();
				}

				return Ok(orders);
			}
			catch (Exception ex)
			{
				return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
			}
		}


		[HttpPost]
		public async Task<ActionResult> Create([FromBody] CreateOrderDto orderDto)
		{
			try
			{
				var order = await _orderService.CreateOrder(orderDto);
				if (order == null)
				{
					return BadRequest();
				}

				return Ok(order);
			}
			catch (Exception ex)
			{
				return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
			}
		}

		[HttpPost("{id}/done")]
		public async Task<ActionResult> MarkAsDone(long id)
		{
			try
			{
				var order = await _orderService.MarkOrderAsDone(id);
				if (order == null)
				{
					return BadRequest();
				}

				return Ok();
			}
			catch (Exception ex)
			{
				return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
			}
		}
	}
}
