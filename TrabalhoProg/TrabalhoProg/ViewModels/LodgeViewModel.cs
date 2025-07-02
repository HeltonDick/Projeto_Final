using TrabalhoProg.Modelo;
using TrabalhoProg.Repository;

namespace TrabalhoProg.ViewModels
{
    public class LodgeViewModel
    {
        public List<Customer> Customers { get; set; } = [];
        public int? CustomerId { get; set; }
        public List<SelectedLodge> SelectedLodge { get; set; } = [];
    }

    public class SelectedLodge
    {
        public bool IsSelected { get; set; } = false;
    }
}
    
