using System.Collections.Generic;
using System.Linq;

namespace P2FixAnAppDotNetCode.Models
{
    /// <summary>
    /// The Cart class
    /// </summary>
    public class Cart : ICart
    {
        // Avant, la liste des lignes du panier était recréée à chaque appel via GetCartLineList(),
        // ce qui empêchait de conserver les produits ajoutés et rendait le panier inutilisable.
        // Maintenant, on utilise un champ privé _cartLines pour stocker l'état réel du panier.
        // Cela permet de gérer correctement l'ajout, la suppression et la consultation des produits.
        private readonly List<CartLine> _cartLines = new();

        /// <summary>
        /// Propriété en lecture seule pour afficher les lignes du panier
        /// </summary>
        public IEnumerable<CartLine> Lines => _cartLines;

        /// <summary>
        /// Ajoute un produit au panier ou incrémente sa quantité s'il existe déjà
        /// </summary>
        public void AddItem(Product product, int quantity)
        {
            var line = _cartLines.FirstOrDefault(l => l.Product.Id == product.Id);
            if (line == null)
            {
                _cartLines.Add(new CartLine { Product = product, Quantity = quantity });
            }
            else
            {
                line.Quantity += quantity;
            }
        }

        /// <summary>
        /// Removes a product from the cart
        /// </summary>
        public void RemoveLine(Product product) =>
            _cartLines.RemoveAll(l => l.Product.Id == product.Id);

        /// <summary>
        /// Get total value of a cart
        /// </summary>
        public double GetTotalValue()
        {
            return _cartLines.Sum(l => l.Product.Price * l.Quantity);
        }

        /// <summary>
        /// Get average value of a cart
        /// </summary>
        public double GetAverageValue()
        {
            int totalQuantity = _cartLines.Sum(l => l.Quantity);
            if (totalQuantity == 0) return 0.0;
            return GetTotalValue() / totalQuantity;
        }

        /// <summary>
        /// Looks after a given product in the cart and returns if it finds it
        /// </summary>
        public Product FindProductInCartLines(int productId)
        {
            var line = _cartLines.FirstOrDefault(l => l.Product.Id == productId);
            return line?.Product;
        }

        /// <summary>
        /// Get a specific cartline by its index
        /// </summary>
        public CartLine GetCartLineByIndex(int index)
        {
            return _cartLines[index];
        }

        /// <summary>
        /// Clears a the cart of all added products
        /// </summary>
        public void Clear()
        {
            _cartLines.Clear();
        }
    }

    public class CartLine
    {
        public int OrderLineId { get; set; }
        public Product Product { get; set; }
        public int Quantity { get; set; }
    }
}
