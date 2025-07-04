using System;
using System.Collections.Generic;
using E_Commerce.Models;

namespace E_Commerce.Services
{
    public class ProductService
    {
        private List<Product> products;

        public ProductService()
        {
            InitializeProducts();
        }

        private void InitializeProducts()
        {
            products = new List<Product>
            {
                new Product { Name = "Cheese", Price = 100, Stock = 10, NeedsShipping = true, Weight = 400, ExpiryDate = DateTime.Now.AddDays(7) },
                new Product { Name = "TV", Price = 1000, Stock = 5, NeedsShipping = true, Weight = 15000 },
                new Product { Name = "Scratch Card", Price = 50, Stock = 100, NeedsShipping = false },
                new Product { Name = "Laptop", Price = 1500, Stock = 8, NeedsShipping = true, Weight = 2500 },
                new Product { Name = "Coffee Mug", Price = 25, Stock = 50, NeedsShipping = true, Weight = 300 },
                new Product { Name = "Old Bread", Price = 10, Stock = 3, NeedsShipping = true, Weight = 200, ExpiryDate = DateTime.Now.AddDays(-2) }
            };
        }

        public List<Product> GetAllProducts()
        {
            return products;
        }

        public void DisplayProducts()
        {
            Console.WriteLine("=== Product List ===");
            Console.WriteLine("ID | Name            | Price  | Stock | Shipping Fee | Weight | Expiry");
            Console.WriteLine("---|-----------------|--------|-------|--------------|--------|--------");
            
            for (int i = 0; i < products.Count; i++)
            {
                var p = products[i];
                var shipping = p.NeedsShipping ? "Yes" : "No";
                var weight = p.NeedsShipping ? $"{p.Weight}g" : "-";
                var stockText = p.Stock > 0 ? p.Stock.ToString() : "Out";
                var expiry = p.ExpiryDate.HasValue ? p.ExpiryDate.Value.ToShortDateString() : "-";
                
                Console.WriteLine($"{i + 1,2} | {p.Name,-15} | ${p.Price,5} | {stockText,5} | {shipping,12} | {weight,6} | {expiry}");
            }
            
            Console.WriteLine("\nNote: Shipping fees are $30 for orders containing shippable items.\n");
        }

        public int GetProductSelection()
        {
            while (true)
            {
                Console.Write("Select product ID (0 to cancel): ");
                var input = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(input))
                {
                    Console.WriteLine("Input cannot be empty.");
                    continue;
                }
                
                int selection = 0;
                if (int.TryParse(input, out selection))
                {
                    if (selection == 0)
                        return -1;
                    
                    if (selection >= 1 && selection <= products.Count)
                    {
                        var product = products[selection - 1];
                        if (product.Stock > 0)
                            return selection - 1;
                        else
                            Console.WriteLine("Product is out of stock.");
                    }
                    else
                    {
                        Console.WriteLine("Invalid ID.");
                    }
                }
                else
                {
                    Console.WriteLine("Please enter a valid number.");
                }
            }
        }

        public Product GetProduct(int index)
        {
            if (index >= 0 && index < products.Count)
                return products[index];
            return null;
        }

        public int GetQuantity(Product product)
        {
            while (true)
            {
                Console.Write($"Quantity for {product.Name} (max {product.Stock}, 0 to cancel): ");
                var input = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(input))
                {
                    Console.WriteLine("Input cannot be empty.");
                    continue;
                }
                
                int qty = 0;
                if (int.TryParse(input, out qty))
                {
                    if (qty == 0)
                        return 0;
                    
                    if (qty > 0 && qty <= product.Stock)
                        return qty;
                    else
                        Console.WriteLine($"Enter a number between 1 and {product.Stock}.");
                }
                else
                {
                    Console.WriteLine("Invalid input.");
                }
            }
        }
    }
} 