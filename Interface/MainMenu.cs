using System;
using E_Commerce.Services;

namespace E_Commerce.Interface
{
    public class MainMenu
    {
        private ProductService productService;
        private CartService cartService;
        private CheckoutService checkoutService;

        public MainMenu(ProductService productService, CartService cartService, CheckoutService checkoutService)
        {
            this.productService = productService;
            this.cartService = cartService;
            this.checkoutService = checkoutService;
        }

        public void DisplayMainMenu()
        {
            Console.WriteLine("Options:");
            Console.WriteLine("1. View products");
            Console.WriteLine("2. View cart");
            Console.WriteLine("3. Remove item from cart");
            Console.WriteLine("4. Checkout");
            Console.WriteLine("5. Exit");
            Console.Write("Select an option (1-5): ");
        }

        public void HandleMenuChoice(string choice)
        {
            switch (choice)
            {
                case "1":
                    HandleViewProducts();
                    break;
                case "2":
                    HandleViewCart();
                    break;
                case "3":
                    HandleRemoveItem();
                    break;
                case "4":
                    HandleCheckout();
                    break;
                case "5":
                    HandleExit();
                    break;
                default:
                    Console.WriteLine("Invalid option.\n");
                    break;
            }
        }

        private void HandleViewProducts()
        {
            while (true)
            {
                productService.DisplayProducts();
                int selection = productService.GetProductSelection();
                if (selection == -1) break;

                var selectedProduct = productService.GetProduct(selection);
                if (selectedProduct.IsExpired)
                {
                    Console.WriteLine("Product is expired and cannot be added.\n");
                    continue;
                }

                // Handle existing product in cart
                if (cartService.ContainsProduct(selectedProduct))
                {
                    Console.WriteLine($"{selectedProduct.Name} is already in your cart. Are you sure you want to add more quantity? (y/n)");
                    if (!InputHandler.GetYesOrNo(""))
                        continue;
                    
                    int addQty = productService.GetQuantity(selectedProduct);
                    if (addQty == 0) continue;
                    if (selectedProduct.Stock < addQty)
                    {
                        Console.WriteLine($"Not enough {selectedProduct.Name} in stock.\n");
                        continue;
                    }
                    
                    cartService.IncreaseProductQuantity(selectedProduct, addQty);
                }
                else
                {
                    // Add new product to cart
                    int quantity = productService.GetQuantity(selectedProduct);
                    if (quantity == 0) continue;
                    
                    decimal potentialTotal = cartService.GetTotal() + (selectedProduct.Price * quantity);
                    if (selectedProduct.NeedsShipping && !cartService.GetCart().NeedsShipping())
                        potentialTotal += 30;
                    
                    if (checkoutService.GetCustomerBalance() < potentialTotal)
                    {
                        Console.WriteLine("Error: Not enough balance to add this product.\n");
                        continue;
                    }
                    
                    cartService.AddProductToCart(selectedProduct, quantity);
                }

                if (!InputHandler.GetYesOrNo("Do you want to add another product? (y/n): "))
                    break;
            }
        }

        private void HandleViewCart()
        {
            cartService.DisplayCart();
        }

        private void HandleRemoveItem()
        {
            cartService.RemoveItemFromCart();
        }

        private void HandleCheckout()
        {
            if (checkoutService.CanCheckout(cartService.GetCart()))
            {
                checkoutService.ProcessCheckout(cartService.GetCart());
            }
        }

        private void HandleExit()
        {
            Console.WriteLine("Goodbye!");
        }
    }
} 