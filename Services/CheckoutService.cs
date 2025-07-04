using System;
using E_Commerce.Models;
using E_Commerce.Interface;

namespace E_Commerce.Services
{
    public class CheckoutService
    {
        private Customer customer;

        public CheckoutService(Customer customer)
        {
            this.customer = customer;
        }

        public bool CanCheckout(ShoppingCart cart)
        {
            // Validate cart before checkout
            if (cart.GetTotal() == 0)
            {
                Console.WriteLine("Cart is empty.\n");
                return false;
            }

            if (cart.HasExpiredItems())
            {
                Console.WriteLine("Cart contains expired items. Please remove them before checkout.\n");
                return false;
            }

            return true;
        }

        public void ProcessCheckout(ShoppingCart cart)
        {
            Console.WriteLine($"Total: ${cart.GetTotal()}");
            Console.WriteLine($"Your balance: ${customer.Balance}");

            if (InputHandler.GetYesOrNo("Proceed to checkout? (y/n): "))
            {
                try
                {
                    cart.Checkout(customer);
                    Console.WriteLine($"\nCheckout completed. Remaining balance: ${customer.Balance}");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Checkout failed: {ex.Message}");
                }
            }
            else
            {
                Console.WriteLine("Checkout cancelled.\n");
            }
        }

        public decimal GetCustomerBalance()
        {
            return customer.Balance;
        }

        public string GetCustomerName()
        {
            return customer.Name;
        }
    }
} 