using StefansSuperShop.Data.Entities;
using StefansSuperShop.Repositories;
//using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace StefansSuperShop.Services;

public interface IProductService
{
    //public Task CreateProduct(Product product);
    public Task<Product> GetById(int id);
    public Task<List<Product>> GetAll();
    //public Task EditProduct(Product product);
}

public class ProductService : IProductService
{
    private readonly IProductRepository _productRepository;

    public ProductService(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }

    //public async Task CreateProduct(Product product)
    //{
    //    await _productRepository.CreateProduct(product);
    //}

    public async Task<Product> GetById(int id) => await _productRepository.GetProductById(id);

    public async Task<List<Product>> GetAll() => await _productRepository.GetAll();

    //public async Task EditProduct(Product product)
    //{
    //    var prod = await _productRepository.GetProductById(product.ProductId);
    //    if (prod == null)
    //    {
    //        throw new Exception($"Product with id {product.ProductId} was not found in database");
    //    }
    //    prod.QuantityPerUnit = product.QuantityPerUnit;
    //    prod.UnitPrice = product.UnitPrice;
    //    prod.Discontinued = product.Discontinued;

    //    await _productRepository.EditProduct(prod);
    //}
}
