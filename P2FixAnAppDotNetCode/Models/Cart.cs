using System.Collections.Generic;
using System.Linq;
using System;

namespace P2FixAnAppDotNetCode.Models
{
    /// <summary>
    /// The Cart class
    /// </summary>
    public class Cart : ICart
    {   /// <summary>
        /// Read-only property for display only
        /// </summary>
        private readonly List<CartLine> _cartLines = new();

        /// <summary>
        /// Propriété en lecture seule pour afficher les lignes du panier
        /// </summary>
        public IEnumerable<CartLine> Lines => _cartLines;
        
        /// <summary>
        /// Adds a product in the cart or increment its quantity in the cart if already added
        /// </summary>
        public void AddItem(Product product, int quantity)
        {
            if (product == null || quantity <= 0)
                return;

            // Recherche d'une ligne existante pour ce produit (par son Id)
            var line = _cartLines.FirstOrDefault(l => l.Product.Id == product.Id);

            int currentQty;
            if (line == null)
            {
                currentQty = 0; 
            }
            else
            {
                currentQty = line.Quantity; 
            }

            // Quantité maximale encore ajoutable sans dépasser le stock
            int allowedToAdd = product.Stock - currentQty;


            // Si plus rien n'est ajoutable (stock déjà atteint ou dépassé), on ne fait rien
            if (allowedToAdd <= 0)
                return;

            int toAdd = System.Math.Min(quantity, allowedToAdd);

            // Ajout ou incrément de la ligne en respectant le plafond calculé
            if (line == null)
            {
                _cartLines.Add(new CartLine { Product = product, Quantity = toAdd });
            }
            else
            {
                line.Quantity += toAdd;
            }
        }

        /// <summary>
        /// Removes a product from the cart
        /// </summary>
        public void RemoveLine(Product product)
        {
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

            // Parcourt chaque ligne du panier
            foreach (var line in _cartLines)
            {
                totalQuantity += line.Quantity; // Ajoute la quantité de chaque ligne au total
                totalValue += line.Product.Price * line.Quantity; // Ajoute la valeur de chaque ligne au total
            }

            // Si le panier est vide (aucun article), retourne 0 pour éviter une division par zéro
            if (totalQuantity == 0)
                return 0.0;

            // Calcule et retourne la valeur moyenne d'un article
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
