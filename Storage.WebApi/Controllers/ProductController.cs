using Microsoft.AspNetCore.Mvc;
using Storage.BLL.DTO.Product;
using Storage.BLL.Services.Interfaces;
using System.Net;

namespace Storage.WebApi.Controllers
{
	[ApiController]
	[Route("/api/products")]
	public class ProductController : ControllerBase
	{
		private readonly IProductService _productService;

		public ProductController(IProductService productService)
		{
			_productService = productService;
		}

		[HttpGet]
		public async Task<ActionResult> GetAll()
		{
			try
			{
				var products = await _productService.GetProducts();
				if (products == null)
				{
					return BadRequest();
				}

				return Ok(products);
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
				var product = await _productService.GetProduct(id);
				if (product == null)
				{
					return BadRequest();
				}

				return Ok(product);
			}
			catch (Exception ex)
			{
				return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
			}
		}


		[HttpPost]
		public async Task<ActionResult> Create([FromBody] CreateProductDto productDto)
		{
			try
			{
				var product = await _productService.Create(productDto);
				if (product == null)
				{
					return BadRequest();
				}

				return Ok(product);
			}
			catch (Exception ex)
			{
				return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
			}
		}

		[HttpPut("{id}")]
		public async Task<ActionResult> Edit(long id, [FromBody] EditProductDto productDto)
		{
			try
			{
				var product = await _productService.Edit(id, productDto);
				if (product == null)
				{
					return BadRequest();
				}

				return Ok(product);
			}
			catch (Exception ex)
			{
				return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
			}
		}

		[HttpDelete("{id}")]
		public async Task<ActionResult> Delete(long id)
		{
			try
			{
				var product = await _productService.Delete(id);
				if (product == null)
				{
					return BadRequest();
				}

				return Ok(product);
			}
			catch (Exception ex)
			{
				return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
			}
		}

		[HttpPost("renew")]
		public async Task<ActionResult> Renew()
		{
			try
			{
				var isSuccess = await _productService.RenewProductQuantity();
				if (!isSuccess)
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
