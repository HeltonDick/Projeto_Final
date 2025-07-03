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
            return (CustomerId > 0) &&
                   (!string.IsNullOrEmpty(Name)) &&
                   (!string.IsNullOrEmpty(Email)) &&
                   (!string.IsNullOrEmpty(Phone));
        }
    }
}
