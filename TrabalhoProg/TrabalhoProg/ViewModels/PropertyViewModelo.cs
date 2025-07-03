using TrabalhoProg.Modelo;

namespace TrabalhoProg.ViewModels
{
    public class PropertyViewModel
    {
        public Property Property { get; set; } = new Property();
        public List<Category> Categories { get; set; } = new List<Category>();
        public List<Address> Addresses { get; set; } = new List<Address>();
    }
}

