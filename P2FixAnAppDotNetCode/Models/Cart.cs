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
            // Recherche si le produit existe déjà dans le panier (même Id)
            // Recherche la première ligne du panier (_cartLines) qui contient un produit
            // ayant le même identifiant (Id) que le produit passé en paramètre.
            // Si aucune ligne ne correspond, 'line' sera null.
            var line = _cartLines.FirstOrDefault(l => l.Product.Id == product.Id);

            if (line == null)
            {
                // Si le produit n'est pas encore dans le panier,
                // on crée une nouvelle ligne avec ce produit et la quantité demandée
                _cartLines.Add(new CartLine { Product = product, Quantity = quantity });
            }
            else
            {
                // Si le produit existe déjà dans le panier,
                // on augmente simplement la quantité de la ligne existante
                line.Quantity += quantity;
            }
        }

        /// <summary>
        /// Removes a product from the cart
        /// </summary>
        public void RemoveLine(Product product)
        {
            // Supprime toutes les lignes du panier correspondant à l'identifiant du produit donné
            _cartLines.RemoveAll(l => l.Product.Id == product.Id);
        }

        /// <summary>
        /// Calcule la valeur totale du panier.
        /// Pour chaque ligne du panier, on multiplie le prix du produit par la quantité,
        /// puis on additionne le tout pour obtenir le montant total.
        /// </summary>
        /// <returns>La somme totale des produits dans le panier.</returns>
        public double GetTotalValue()
        {
            double total = 0.0;
            foreach (var line in _cartLines)
            {
                // Pour chaque ligne, on multiplie le prix du produit par la quantité
                total += line.Product.Price * line.Quantity;
            }
            return total;
        }

        /// <summary>
        /// Calcule la valeur moyenne d'un article dans le panier.
        /// On divise la valeur totale du panier par le nombre total d'articles.
        /// Si le panier est vide, retourne 0.
        /// </summary>
        /// <returns>La valeur moyenne d'un article dans le panier.</returns>
        public double GetAverageValue()
        {
            int totalQuantity = 0;
            double totalValue = 0.0;

            foreach (var line in _cartLines)
            {
                totalQuantity += line.Quantity;
                totalValue += line.Product.Price * line.Quantity;
            }

            if (totalQuantity == 0)
                return 0.0;

            return totalValue / totalQuantity;
        }

        /// <summary>
        /// Looks after a given product in the cart and returns if it finds it
        /// </summary>
        public Product FindProductInCartLines(int productId)
        {
            // Parcourt chaque ligne du panier
            foreach (var line in _cartLines)
            {
                // Vérifie si l'identifiant du produit correspond à celui recherché
                if (line.Product.Id == productId)
                {
                    // Retourne le produit trouvé
                    return line.Product;
                }
            }
            // Si aucun produit n'est trouvé, retourne null
            return null;
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
