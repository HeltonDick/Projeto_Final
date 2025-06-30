using Modelo;
using Repository;

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
            _lodgeRepository = new LodgeRepository;
            _customerRepository = new CustomerRepository;
            _propertyRepository = new PropertyRepository;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View(_lodgeRepository.RetrieveAll());
        }

        [HttpGet]
        public IActionResult Create()
        {
            LodgeRepository viewModel = new();
            viewModel.Customers = _customerRepository.RetrieveAll();

            var properties = _propertyRepository.RetrieveAll();
            List<SelectedLodge> items = new List<SelectedLodge>();
            foreach (var property in properties)
            {
                items.Add(new SelectedLodge()
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
        public IActionResult Create(Lodge lodge)
        {
            if (ModelState.IsValid)
            {
                _lodgeRepository.Save(lodge);
                return RedirectToAction("Index");
            }
            return View(lodge);
        }
    }
}
