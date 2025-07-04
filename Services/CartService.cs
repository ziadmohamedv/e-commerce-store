using System;
using System.Collections.Generic;
using E_Commerce.Models;
using E_Commerce.Interface;

namespace E_Commerce.Services
{
    public class CartService
    {
        private ShoppingCart cart;
        private Customer customer;

        public CartService(Customer customer)
        {
            this.customer = customer;
            this.cart = new ShoppingCart();
        }

        public void AddProductToCart(Product product, int quantity)
        {
            try
            {
                cart.Add(product, quantity, customer);
                Console.WriteLine($"Added {quantity}x {product.Name} to cart.");
                Console.WriteLine($"Remaining stock: {product.Stock}\n");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}\n");
            }
        }

        public void IncreaseProductQuantity(Product product, int quantity)
        {
            decimal potentialTotal = cart.GetTotal() + (product.Price * quantity);
            if (product.NeedsShipping && !cart.NeedsShipping())
                potentialTotal += 30;
            
            if (customer.Balance < potentialTotal)
            {
                Console.WriteLine("Error: Not enough balance to add this product.\n");
                return;
            }
            
            if (cart.IncreaseQuantity(product, quantity))
            {
                Console.WriteLine($"Increased {product.Name} quantity by {quantity} in cart.\n");
                Console.WriteLine($"Remaining stock: {product.Stock}\n");
            }
            else
            {
                Console.WriteLine($"Could not increase quantity for {product.Name}.\n");
            }
        }

        public void RemoveItemFromCart()
        {
            while (true)
            {
                cart.DisplayCart();
                if (cart.GetTotal() == 0)
                {
                    Console.WriteLine("Cart is empty.\n");
                    break;
                }
                while (true)
                {
                    Console.Write("Enter product name to remove (0 to cancel): ");
                    var name = Console.ReadLine()?.Trim();
                    if (string.IsNullOrWhiteSpace(name))
                    {
                        Console.WriteLine("Input cannot be empty.\n");
                        continue;
                    }
                    if (name == "0")
                        break;
                    
                    int beforeQty = 0;
                    bool found = false;
                    foreach (var item in GetCartItems())
                    {
                        if (item.Product.Name.Equals(name, StringComparison.OrdinalIgnoreCase))
                        {
                            beforeQty = item.Quantity;
                            found = true;
                            break;
                        }
                    }
                    
                    if (!found)
                    {
                        Console.WriteLine("Item not found in cart.\n");
                        continue;
                    }
                    
                    Console.Write("Enter quantity to remove (0 to cancel): ");
                    var qtyInput = Console.ReadLine();
                    if (string.IsNullOrWhiteSpace(qtyInput))
                    {
                        Console.WriteLine("Input cannot be empty.\n");
                        continue;
                    }
                    
                    int qtyToRemove = 0;
                    if (!int.TryParse(qtyInput, out qtyToRemove) || qtyToRemove < 0)
                    {
                        Console.WriteLine("Invalid quantity.\n");
                        continue;
                    }
                    if (qtyToRemove == 0) break;
                    if (qtyToRemove > beforeQty)
                    {
                        Console.WriteLine($"You only have {beforeQty}x {name} in your cart.\n");
                        continue;
                    }
                    
                    bool removed = cart.Remove(name, qtyToRemove);
                    if (removed)
                    {
                        if (qtyToRemove >= beforeQty)
                            Console.WriteLine("Item removed from cart.\n");
                        else
                            Console.WriteLine("Item quantity updated in cart.\n");
                        
                        if (cart.GetTotal() == 0)
                        {
                            Console.WriteLine("Cart is now empty.\n");
                            return;
                        }
                        
                        if (InputHandler.GetYesOrNo("Do you want to remove another item? (y/n): "))
                            break;
                        else
                            goto EndRemoveLoop;
                    }
                    else
                    {
                        Console.WriteLine("Quantity too high.\n");
                        continue;
                    }
                }
            }
            EndRemoveLoop: ;
        }

        public void DisplayCart()
        {
            cart.DisplayCart(true);
        }

        public bool ContainsProduct(Product product)
        {
            return cart.ContainsProduct(product);
        }

        public decimal GetTotal()
        {
            return cart.GetTotal();
        }

        public bool HasExpiredItems()
        {
            return cart.HasExpiredItems();
        }

        public bool IsCartEmpty()
        {
            return cart.GetTotal() == 0;
        }

        private List<CartItem> GetCartItems()
        {
            var itemsField = typeof(ShoppingCart).GetField("items", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
            return (List<CartItem>)itemsField.GetValue(cart);
        }

        public ShoppingCart GetCart()
        {
            return cart;
        }
    }
}