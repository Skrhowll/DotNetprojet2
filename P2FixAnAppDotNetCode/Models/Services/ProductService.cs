using P2FixAnAppDotNetCode.Models.Repositories;
using System.Collections.Generic;
using System.Linq;

namespace P2FixAnAppDotNetCode.Models.Services
{
    /// <summary>
    /// This class provides services to manages the products
    /// </summary>
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;
        private readonly IOrderRepository _orderRepository;

        public ProductService(IProductRepository productRepository, IOrderRepository orderRepository)
        {
            _productRepository = productRepository;
            _orderRepository = orderRepository;
        }

        /// <summary>
        /// Get all product from the inventory
        /// </summary>
        public List<Product> GetAllProducts()
        {
            // TODO (fait) : change the return type from array to List<T> and propagate the change
            // throughout the application
            return _productRepository.GetAllProducts().ToList();
        }

        /// <summary>
        /// Récupère un produit de l'inventaire par son identifiant.
        /// Cette méthode parcourt la liste de tous les produits retournée par le repository
        /// et renvoie le premier produit dont l'Id correspond à celui passé en paramètre.
        /// Si aucun produit n'est trouvé, elle retourne null.
        /// </summary>
        public Product GetProductById(int id)
        {
            // Utilise LINQ pour rechercher le produit avec l'Id correspondant.
            // FirstOrDefault retourne le premier produit trouvé ou null si aucun ne correspond.
            return _productRepository.GetAllProducts().FirstOrDefault(p => p.Id == id);
        }

        /// <summary>
        /// Update the quantities left for each product in the inventory depending of ordered the quantities
        /// </summary>
        public void UpdateProductQuantities(Cart cart)
        {
            // TODO (fait) : implement the method
            // update product inventory by using _productRepository.UpdateProductStocks() method.
            foreach (var line in cart.Lines)
            {
                _productRepository.UpdateProductStocks(line.Product.Id, line.Quantity);
            }
        }
    }
}
