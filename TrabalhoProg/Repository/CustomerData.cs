using TrabalhoProg.Modelo;

namespace TrabalhoProg.Repository
{
    public static class CustomerData
    {
        public static List<Customer> Customers { get; set; } = new();
        public static List<Property> Properties { get; set; } = new();
    }
}
