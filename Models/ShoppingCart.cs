using System;
using System.Collections.Generic;

namespace E_Commerce.Models
{
    public class ShoppingCart
    {
        private List<CartItem> items = new List<CartItem>();

        public void Add(Product product, int quantity, Customer customer)
        {
            if (product.Stock < quantity)
                throw new Exception("Not enough stock.");
            if (product.IsExpired)
                throw new Exception("Product is expired.");
            decimal total = GetTotal() + product.Price * quantity;
            if (NeedsShipping() || product.NeedsShipping)
                total += 30;
            if (customer.Balance < total)
                throw new Exception("Insufficient balance.");
            product.Stock -= quantity;
            items.Add(new CartItem(product, quantity));
        }

        public bool IncreaseQuantity(Product product, int quantity)
        {
            foreach (var item in items)
            {
                if (item.Product == product)
                {
                    if (product.Stock < quantity)
                        return false;
                    product.Stock -= quantity;
                    item.Quantity += quantity;
                    return true;
                }
            }
            return false;
        }

        public bool ContainsProduct(Product product)
        {
            foreach (var item in items)
            {
                if (item.Product == product)
                    return true;
            }
            return false;
        }

        public decimal GetTotal()
        {
            decimal total = 0;
            foreach (var item in items)
            {
                total += item.Product.Price * item.Quantity;
            }
            return total;
        }

        public bool Remove(string productName, int quantity)
        {
            for (int i = 0; i < items.Count; i++)
            {
                if (string.Equals(items[i].Product.Name, productName, StringComparison.OrdinalIgnoreCase))
                {
                    var item = items[i];
                    if (quantity >= item.Quantity)
                    {
                        items.RemoveAt(i);
                        return true;
                    }
                    else if (quantity > 0)
                    {
                        item.Quantity -= quantity;
                        return true;
                    }
                }
            }
            return false;
        }

        public bool HasExpiredItems()
        {
            foreach (var item in items)
            {
                if (item.Product.IsExpired)
                    return true;
            }
            return false;
        }

        public bool NeedsShipping()
        {
            foreach (var item in items)
            {
                if (item.Product.NeedsShipping)
                    return true;
            }
            return false;
        }

        public void DisplayCart()
        {
            DisplayCart(true);
        }

        public void DisplayCart(bool showEmptyMessage)
        {
            if (items.Count == 0)
            {
                if (showEmptyMessage)
                    Console.WriteLine("Cart is empty.");
                return;
            }
            Console.WriteLine("=== Cart Contents ===");
            foreach (var item in items)
            {
                Console.WriteLine($"{item.Quantity}x {item.Product.Name} - ${item.Product.Price * item.Quantity}");
            }
            Console.WriteLine($"Subtotal: ${GetTotal()}");
            var shipping = NeedsShipping() ? 30 : 0;
            Console.WriteLine($"Shipping: ${shipping}");
            Console.WriteLine($"Total: ${GetTotal() + shipping}");
            Console.WriteLine();
        }

        public void Checkout(Customer customer)
        {
            if (items.Count == 0)
                throw new Exception("Cart is empty.");
            decimal total = GetTotal();
            decimal shipping = NeedsShipping() ? 30 : 0;
            decimal grandTotal = total + shipping;
            if (customer.Balance < grandTotal)
                throw new Exception("Insufficient balance.");
            customer.Balance -= grandTotal;
            
            if (NeedsShipping())
            {
                Console.WriteLine("** Shipment notice **");
                double totalWeight = 0;
                foreach (var item in items)
                {
                    if (item.Product.NeedsShipping)
                    {
                        Console.WriteLine($"{item.Quantity}x {item.Product.Name} {item.Product.Weight}g");
                        totalWeight += item.Product.Weight * item.Quantity;
                    }
                }
                Console.WriteLine($"Total package weight {totalWeight / 1000}kg");
                Console.WriteLine("----------------------");
            }
            
            Console.WriteLine("** Checkout receipt **");
            foreach (var item in items)
            {
                Console.WriteLine($"{item.Quantity}x {item.Product.Name} {item.Product.Price * item.Quantity}");
            }
            Console.WriteLine($"Subtotal {total}");
            Console.WriteLine($"Shipping {shipping}");
            Console.WriteLine($"Amount {grandTotal}");
            
            items.Clear();
        }
    }
}
