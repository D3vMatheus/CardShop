using AutoMapper;
using CardShop.Context;
using CardShop.DTOs;
using CardShop.Model;
using CardShop.Pagination;
using CardShop.Repository.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CardShop.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IProductRepository _productRepository;
        private readonly ILogger<ProductController> _logger;
        private readonly IMapper _mapper;
        public ProductController(IUnitOfWork unitOfWork,
                                 IProductRepository productRepository,
                                 IMapper mapper,
                                 ILogger<ProductController> logger)
        {
            _unitOfWork = unitOfWork;
            _productRepository = productRepository;
            _mapper = mapper;
            _logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductDTO>>> GetProducts()
        {
            var product = await _unitOfWork.ProductRepository.GetAllAsync();

            if(product is null)
            {
                _logger.LogWarning("Products doesn't exist");
                return NotFound("Product not found");
            }

            var productDto = _mapper.Map<IEnumerable<ProductDTO>>(product);

            return Ok(productDto);
        }

        [HttpGet("{id}", Name = "GetProductById")]
        public async Task<ActionResult<ProductDTO>> GetProductById(int id)
        {
            var product = await _unitOfWork.ProductRepository.GetAsync(p => p.ProductId == id);

            if (product is null)
            {
                _logger.LogWarning($"Product {id} doesn't exist");
                return NotFound("Product not found");
            }

            var productDto = _mapper.Map<ProductDTO>(product);

            return Ok(productDto);
        }
        [HttpGet("pagination")]
        public async Task<ActionResult<ProductDTO>> Get([FromQuery] ProductsParameters productsParameters)
        {
            var products = await _unitOfWork.ProductRepository.GetProducts(productsParameters);
            var productsDto = _mapper.Map<ProductDTO>(products);

            return Ok(productsDto);
        }

        [HttpPost]
        public async Task<ActionResult<ProductDTO>> AddProduct(ProductDTO productDto)
        {
            
            if (productDto is null)
            {
                _logger.LogWarning($"Couldn't add product due invalid information detected");
                return BadRequest("Invalid information detected");
            }

            var product = _mapper.Map<Product>(productDto);

            var newProduct = await _unitOfWork.ProductRepository.CreateAsync(product);

            await _unitOfWork.CommitAsync();

            var newProductDto = _mapper.Map<ProductDTO>(product);

            return new CreatedAtRouteResult("GetProductById", new { id = newProductDto.ProductId }, newProductDto);
        }

        //This approach only allow full update products
        //It's possible bypass this approach using PATCH or a different PUT implementation
        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateProduct(int id, ProductDTO productDto)
        {
            if (id != productDto.ProductId)
            {
                _logger.LogWarning($"Couldn't update product due invalid information detected: {id} doesn't exist");
                return BadRequest("Invalid information detected");
            }

            var product = _mapper.Map<Product>(productDto);

            var updatedProduct = await _unitOfWork.ProductRepository.UpdateAsync(product);

            await _unitOfWork.CommitAsync();

            return NoContent();
        }

        [HttpPatch("{id}/InStock")]
        public async Task<ActionResult<ProductDTOUpdateResponse>> PatchProduct(int id,
                            JsonPatchDocument<ProductDTOUpdateRequest> patchProductDto)
        {
            if (patchProductDto is null || id <=0)
                return BadRequest();

            var product = await _unitOfWork.ProductRepository.GetAsync(p => p.ProductId == id);

            if (product is null)
                return NotFound();

            var productUpdateRequest = _mapper.Map<ProductDTOUpdateRequest>(product);

            patchProductDto.ApplyTo(productUpdateRequest, ModelState);
            
            if(!ModelState.IsValid || TryValidateModel(productUpdateRequest))
                return BadRequest(ModelState);

            await _unitOfWork.ProductRepository.UpdateAsync(product);
            await _unitOfWork.CommitAsync();
            
            var productUpdateResponse = _mapper.Map<ProductDTOUpdateResponse>(product);
            
            return Ok(productUpdateResponse);
        }


        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteProduct(int id)
        {
            var product = await _unitOfWork.ProductRepository.GetAsync(p => p.ProductId == id);

            if (product is null)
            {
                _logger.LogWarning($"Couldn't delete product due invalid information detected: {id} doesn't exist");
                return NotFound("Product not found");
            }

            await _productRepository.DeleteAsync(id);

            return NoContent();
        }
    }
}
