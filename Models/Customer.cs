namespace E_Commerce.Models
{
    public class Customer
    {
        public string Name { get; set; }
        public decimal Balance { get; set; }

        public Customer(string name, decimal balance)
        {
            Name = name;
            Balance = balance;
        }
    }
}
