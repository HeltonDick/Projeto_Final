namespace TrabalhoProg.Modelo
{
    public class Customer
    {
        public int CustomerId { get; set; }
        public string? Name { get; set; }
        public string? Email { get; set; }
        public string? Phone { get; set; }
        public Address? Address { get; set; }

        public bool Validate()
        {
            bool IsValid = true;

            IsValid = (CustomerId > 0) &&
            (!string.IsNullOrEmpty(this.Name)) &&
            (!string.IsNullOrEmpty(this.Email)) &&
            (!string.IsNullOrEmpty(this.Phone));

            return IsValid;
        }

        public Customer Retrieve()
        {
            return new Customer();
        }

        public void Save(Customer customer)
        {
        }
    }
}