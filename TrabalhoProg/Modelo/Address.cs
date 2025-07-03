namespace TrabalhoProg.Modelo
{
    public class Address
    {
        public int AddressId { get; set; }
        public string? Street { get; set; }
        public string? Street1 { get; set; }
        public string? City { get; set; }
        public string? State { get; set; }
        public string? PostalCode { get; set; }
        public string? Country { get; set; }

        public bool Validate()
        {
            bool IsValid = true;

            IsValid = (AddressId > 0) &&
                      (!string.IsNullOrEmpty(Street)) &&
                      (!string.IsNullOrEmpty(City)) &&
                      (!string.IsNullOrEmpty(State)) &&
                      (!string.IsNullOrEmpty(PostalCode)) &&
                      (!string.IsNullOrEmpty(Country));

            return IsValid;
        }
    }
}
