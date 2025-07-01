using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using TrabalhoProg.Modelo;
using TrabalhoProg.Repository;
using TrabalhoProg.ViewModels;

namespace TrabalhoProg.Controllers
{
    public class LodgeController : Controller
    {
        private readonly IWebHostEnvironment environment;
        private readonly LodgeRepository _lodgeRepository;
        private readonly CustomerRepository _customerRepository;
        private readonly PropertyRepository _propertyRepository;

        public LodgeController(IWebHostEnvironment environment)
        {
            this.environment = environment;
            _lodgeRepository = new LodgeRepository();
            _customerRepository = new CustomerRepository();
            _propertyRepository = new PropertyRepository();
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View(_lodgeRepository.RetrieveAll());
        }

        [HttpGet]
        public IActionResult Create()
        {
            LodgeViewModel viewModel = new();
            viewModel.Customers = _customerRepository.RetrieveAll();

            var properties = _propertyRepository.RetrieveAll();
            List<SelectedLodge> lodges = new List<SelectedLodge>();
            foreach (var property in properties)
            {
                lodges.Add(new SelectedLodge()
                {
                    LodgeProperty = new()
                    {
                        Property = property
                    }
                });
            }
            viewModel.SelectedLodge = lodges;

            return View(viewModel);
        }

        [HttpPost]
        public IActionResult Create(LodgeViewModel model)
        {
            Lodge lodge = new Lodge();
            lodge.Customer =
                _customerRepository.Retrieve(model.CustomerId!.Value);
            lodge.LodgeDate = DateTime.Now;

            int count = 1;
            foreach (var property in model.SelectedLodge!)
            {
                if (property.IsSelected)
                {
                    lodge.LodgeProperty!.Add(
                        new LodgeProperty()
                        {
                            LodgePropertyId = count,
                            Property = _propertyRepository.Retrieve(property.LodgeProperty.Property!.PropertyId),
                            PricePerNight = property.LodgeProperty.PricePerNight,
                        }
                    );
                    count++;
                }
            }
            _lodgeRepository.Save(lodge);
            return RedirectToAction("Index");


        }
    }
}
