using TrabalhoProg.Modelo;

namespace TrabalhoProg.Repository
{
    public class PropertyData
    {
        public static List<Property> RealStates { get; set; } = new List<Property>();
        public static List<Category> Categories { get; set; } = new List<Category>();
        public static List<Address> Addresses { get; set; } = new List<Address>();
    }
}
