using System;
using E_Commerce.Models;
using E_Commerce.Services;
using E_Commerce.Interface;

namespace E_Commerce
{
    class Program
    {
        static void Main()
        {
            // Initialize customer with his balance
            var customer = new Customer("Ziad", 2000);
            var productService = new ProductService();
            var cartService = new CartService(customer);
            var checkoutService = new CheckoutService(customer);
            var menu = new MainMenu(productService, cartService, checkoutService);

            Console.WriteLine("=== E-Commerce Store ===\n");
            Console.WriteLine($"Hello, {customer.Name}. Your balance: ${customer.Balance}\n");

            while (true)
            {
                menu.DisplayMainMenu();
                var choice = InputHandler.GetUserInput("");
                Console.WriteLine();

                if (choice == "5")
                {
                    menu.HandleMenuChoice(choice);
                    break;
                }
                else
                {
                    menu.HandleMenuChoice(choice);
                }
            }
        }
    }
}