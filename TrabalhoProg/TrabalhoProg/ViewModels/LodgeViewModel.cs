using Modelo;
using Repository;

namespace TrabalhoProg.ViewModels
{
    public class LodgeViewModel
    {
        public List<Customer> Customers { get; set; } = [];
        public int CustomerId { get; set; }
        public List<SelectedLodge> SelectedLodge { get; set; } = [];
    }

    public class SelectedLodge
    {
        public bool IsSelected { get; set; } = false;
        public LodgeProperty LodgeProperty { get; set; } = null;
    }
}
    
