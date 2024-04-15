using HandleConcurrencyWithEtag.Data;
using HandleConcurrencyWithEtag.Dtos;

namespace HandleConcurrencyWithEtag.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly AppDbContext _dbContext;

        public ProductRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public ProductDto GetById(int id)
        {
            var product = _dbContext.Products.FirstOrDefault(p => p.Id == id);
            var productDto = new ProductDto
            {
                Id = product.Id,
                Name = product.Name,
                Price = product.Price
            };

            return productDto;
        }



        public int Add(CreateProductDto productDto)
        {
            var newProduct = new Product
            {
                Name = productDto.Name,
                Price = productDto.Price
            };

            _dbContext.Add(newProduct);
            _dbContext.SaveChanges();
            return newProduct.Id;
        }

        public void Update(int id, CreateProductDto productDto)
        {
            var existingProduct = _dbContext.Products.FirstOrDefault(p => p.Id == id);
            if (existingProduct == null)
            {
                throw new InvalidOperationException($"Product with ID {id} not found");
            }

            existingProduct.Name = productDto.Name;
            existingProduct.Price = productDto.Price;

            _dbContext.Products.Update(existingProduct);
            _dbContext.SaveChanges();
        }

        public void Delete(int id)
        {
            var productToRemove = _dbContext.Products.FirstOrDefault(p => p.Id == id);
            if (productToRemove != null)
            {
                _dbContext.Remove(productToRemove);
                _dbContext.SaveChanges();
            }
        }
    }

}
