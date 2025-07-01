using TrabalhoProg.Modelo;
using TrabalhoProg.Repository;

namespace TrabalhoProg.ViewModels
{
    public class PropertyViewModel
    {
        public List<Category> Categories { get; set; } = [];
        public Property? Property { get; set; }
        public List<Address>? Addresses { get; set; } = [];

    }
}