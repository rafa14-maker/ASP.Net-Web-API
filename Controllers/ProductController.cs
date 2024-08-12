
using Akij_Bashir_It.Dto;
using Akij_Bashir_It.Interfaces;
using Akij_Bashir_It.Models;
using Akij_Bashir_It.Repository;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace Akij_Bashir_It.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IProductRepository _productRepository;
        public ProductController(IProductRepository productRepository, IMapper mapper)
        {
           _mapper = mapper;
            _productRepository = productRepository;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Product>))]

        public IActionResult GetProducts()
        {
            var products = _mapper.Map<List<ProductDto>>(_productRepository.GetProducts());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(products);
        }

        [HttpGet("{productId}")]
        [ProducesResponseType(200, Type = typeof(Product))]
        [ProducesResponseType(400)]
        public IActionResult GetProduct(int productId)
        {
            if (!_productRepository.ProductExistis(productId))
                return NotFound();

            var product = _mapper.Map<ProductDto>(_productRepository.GetProduct(productId)); ;

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(product);
        }

        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CreateProduct([FromQuery] int userId, [FromBody] ProductDto productCreate)
        {
            if (productCreate == null)
                return BadRequest(ModelState);

            var product = _productRepository.GetProducts()
                             .Where(c => c.Name.Trim().ToUpper() == productCreate.Name.Trim().ToUpper())
                             .FirstOrDefault();

            if (product != null)
            {
                ModelState.AddModelError("", "Product already Exits");
                return StatusCode(422, ModelState);
            }

            if (!ModelState.IsValid) return BadRequest();

            var productMap = _mapper.Map<Product>(productCreate);

            if (!_productRepository.CreateProduct(productMap, userId))
            {
                ModelState.AddModelError("", "Something went wrong while saving");
                return StatusCode(500, ModelState);
            }

            return Ok("Successfully created");
        }

        [HttpPut("{productId}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public IActionResult UpdateProduct([FromQuery] int productId, [FromQuery] int userId, [FromBody] ProductDto updatedProduct)
        {
            if (updatedProduct == null)
                return BadRequest(ModelState);

            if (productId != updatedProduct.Id)
                return BadRequest(ModelState);

            if (!_productRepository.ProductExistis(productId))
                return NotFound();

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var appUser = _productRepository.GetUserByProduct(productId).FirstOrDefault();

         //   if (appUser.Id != userId)
        //    {
         //       ModelState.AddModelError("", "You Dont have the Permission to make changes");
         //       return StatusCode(500, ModelState);
          //  }

            var productMap = _mapper.Map<Product>(updatedProduct);

            if (!_productRepository.UpdateProduct(productMap, userId))
            {
                ModelState.AddModelError("", "Something went wrong");
                return StatusCode(500, ModelState);
            }

            return Ok("Successfully Updated Product");

        }

        [HttpDelete("{productId}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public IActionResult DeleteProduct(int productId)
        {
            if (!_productRepository.ProductExistis(productId))
            {
                return NotFound();
            }
            var productToDelete = _productRepository.GetProduct(productId);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!_productRepository.DeleteProduct(productToDelete))
            {
                ModelState.AddModelError("", "Something went really wrong");
            }

            return Ok("Successfully Deleted Product");
        }



    }
}
