using Product_service.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace Product_service.Services;

public class ProductService
{
    private readonly IMongoCollection<Product> _productsCollection;

    public ProductService(
        IOptions<ProductDatabaseSettings> productDatabaseSettings
    ) {
        var mongoClient = new MongoClient(
            productDatabaseSettings.Value.ConnectionString
        );

        var mongoDatabase = mongoClient.GetDatabase(
            productDatabaseSettings.Value.DatabaseName
        );

        _productsCollection = mongoDatabase.GetCollection<Product> (
            productDatabaseSettings.Value.ProductCollectionName
        );
    }

    public async Task<List<Product>> GetProductsAsync () => 
        await _productsCollection.Find(_ => true).ToListAsync();
    
    public async Task<Product?> GetProductAsync (string id) =>
        await _productsCollection.Find(item => item.Id == id).FirstOrDefaultAsync();

    public async Task CreateAsync (Product newProduct) =>
        await _productsCollection.InsertOneAsync(newProduct);

    public async Task UpdateAsync (string id, Product updateProduct) =>
        await _productsCollection.ReplaceOneAsync(item => item.Id == id, updateProduct);

    public async Task RemoveAsync (string id) => 
        await _productsCollection.DeleteOneAsync(item => item.Id == id);
}