using Microsoft.AspNetCore.Mvc;
using TrabalhoProg.ViewModels;
using TrabalhoProg.Repository;
using TrabalhoProg.Modelo;

namespace TrabalhoProg.Controllers
{
    public class PropertyController : Controller
    {
        private readonly IWebHostEnvironment environment;

        private readonly PropertyRepository _propertyRepository;
        private readonly CategoryRepository _categoryRepository;
        private readonly AddressRepository _addressRepository;

        public PropertyController(IWebHostEnvironment environment)
        {
            this.environment = environment;
            _propertyRepository = new PropertyRepository();
            _categoryRepository = new CategoryRepository();
            _addressRepository = new AddressRepository();
        }

        [HttpGet]
        public IActionResult Index()
        {
            List<Property> realState = _propertyRepository.RetrieveAll();
            return View(realState);
        }

        [HttpGet]
        public IActionResult Create()
        {
            var viewModel = new PropertyViewModel
            {
                Property = new Property(),
                Categories = _categoryRepository.RetrieveAll(),
                Addresses = _addressRepository.RetrieveAll()
            };
            return View(viewModel);
        }

        [HttpPost]
        public IActionResult Create(Property property)
        {
            var selectedCategory = _categoryRepository.RetrieveAll()
                .FirstOrDefault(c => c.Name == property.Category?.Name);

            property.Category = selectedCategory;

            _propertyRepository.Save(property);

            List<Property> properties = _propertyRepository.RetrieveAll();
            return View("Index", properties);
        }


        [HttpGet]
        public IActionResult Delete(int? id)
        {
            if (id is null || id.Value <= 0)
                return NotFound();

            Property property = _propertyRepository.Retrieve(id.Value);

            if (property == null)
                return NotFound();

            var viewModel = new PropertyViewModel
            {
                Property = property
            };

            return View(viewModel);
        }

        [HttpPost]
        public IActionResult ConfirmDelete(int? id)
        {
            if (id is null || id.Value <= 0)
                return NotFound();

            if (!_propertyRepository.DeleteById(id.Value))
                return NotFound();

            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Update(int id)
        {
            var property = _propertyRepository.Retrieve(id);
            var viewModel = new PropertyViewModel
            {
                Property = property,
                Categories = _categoryRepository.RetrieveAll(),
                Addresses = _addressRepository.RetrieveAll()
            };
            return View(viewModel);
        }

        [HttpGet]
        public IActionResult ExportDelimitedFile()
        {
            string fileContent = string.Empty;

            foreach (Property c in PropertyData.RealStates)
            {
                fileContent +=
                    $"{c.PropertyId};{c.Name};{c.Description};{c.BedRooms};" +
                    $"{c.GarageVacancies};{c.CurrentPricePerNight};{c.Category?.Name};" +
                    $"{c.Category?.Description};{c.Address?.Street};" +
                    $"{c.Address?.Street1};{c.Address?.City};" +
                    $"{c.Address?.State};{c.Address?.PostalCode};{c.Address?.Country}\n";
            }

            SaveFile(fileContent, "DelimitedFile.txt");

            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult ExportFixedFile()
        {
            string fileContent = string.Empty;

            foreach (Property c in PropertyData.RealStates)
            {
                fileContent +=
                    string.Format("{0,5}{1,-64}{2,-64}{3,5}", c.PropertyId, c.Name, c.Description, c.BedRooms) +
                    string.Format("{0,5}", c.GarageVacancies) +
                    string.Format("{0,32}", c.CurrentPricePerNight) +
                    string.Format("{0,-32}", c.Category?.Name) +
                    string.Format("{0,-32}", c.Category?.Description) +
                    string.Format("{0,-64}", c.Address?.Street) +
                    string.Format("{0,-64}", c.Address?.Street1) +
                    string.Format("{0,-64}", c.Address?.City) +
                    string.Format("{0,-64}", c.Address?.State) +
                    string.Format("{0,-64}", c.Address?.PostalCode) +
                    string.Format("{0,-64}", c.Address?.Country) +
                    "\n";
            }

            SaveFile(fileContent, "FixedFile.txt");
            return RedirectToAction("Index");
        }

        private bool SaveFile(string content, string fileName)
        {
            if (string.IsNullOrEmpty(content) || string.IsNullOrEmpty(fileName))
                return false;

            var path = Path.Combine(environment.WebRootPath, "TextFiles");

            try
            {
                if (!Directory.Exists(path))
                    Directory.CreateDirectory(path);

                var filepath = Path.Combine(path, fileName);

                System.IO.File.WriteAllText(filepath, content);
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
