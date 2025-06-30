using Modelo;

namespace Repository {
    public class CustomerData {
        public static List<Customer> Customers { get; set; } = [];
        public static List<Property> Properties { get; set; } = [];
        public static List<Lodge> Lodges { get; set; } = [];
    }
}