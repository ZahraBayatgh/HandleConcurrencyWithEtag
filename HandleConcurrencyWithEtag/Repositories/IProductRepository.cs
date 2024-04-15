using HandleConcurrencyWithEtag.Dtos;

namespace HandleConcurrencyWithEtag.Repositories
{
    public interface IProductRepository
    {
        ProductDto GetById(int id);
        int Add(CreateProductDto productDto);
        public void Update(int id, CreateProductDto productDto);  
        void Delete(int id);
    }


}
