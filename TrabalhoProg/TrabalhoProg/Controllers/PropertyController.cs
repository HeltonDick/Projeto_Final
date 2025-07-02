using System.Diagnostics;
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
            var viewModel = new PropertyViewModel()
            {
                Categories = _categoryRepository.RetrieveAll(),
                Property = new Property(),
                Addresses = _addressRepository.RetrieveAll()
            };
            return View(viewModel);
        }

        [HttpGet]
        public IActionResult Delete(int? id)
        {
            if (id is null || id.Value <= 0)
                return NotFound();

            Property property =
                _propertyRepository.Retrieve(id.Value);

            if (property == null)
                return NotFound();

            return View(property);
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
        public IActionResult ExportDelimitatedFile()
        {
            string fileContent = string.Empty;

            foreach (Property c in PropertyData.RealStates) {
                fileContent +=
                    $"{c.PropertyId};{c.Name};{c.Description};{c.BedRooms};" +
                    $"{c.GarageVacancies};{c.CurrentPricePerNight};{c.Category?.Name};" +
                    $"{c.Category?.Description};{c.Address?.Street};" +
                    $"{c.Address?.Street1};{c.Address?.City};" +
                    $"{c.Address?.State};{c.Address?.PostalCode};{c.Address?.Country}\n";
            }

            SaveFile(fileContent, "DelimitatedFile.txt");

            return View();
        }

        [HttpPost]
        public IActionResult ExportFixedFile()
        {
            string fileContent = string.Empty;
            foreach (Property c in PropertyData.RealStates)
            {
                fileContent +=
                    String.Format("{0:5}{1:64}", c.PropertyId, c.Name, c.Description, c.BedRooms) +
                    String.Format("{0:5}", c.GarageVacancies) +
                    String.Format("{0:32}", c.CurrentPricePerNight) +
                    String.Format("{0:2}", c.Category?.Name) +
                    String.Format("{0:32}", c.Category?.Description) +
                    String.Format("{0:64}", c.Address?.Street) +
                    String.Format("{0:64}", c.Address?.Street1) +
                    String.Format("{0:64}", c.Address?.City) +
                    String.Format("{0:64}", c.Address?.State) +
                    String.Format("{0:64}", c.Address?.PostalCode) +
                    String.Format("{0:64}", c.Address?.Country) +
                    "\n";
            }

            SaveFile(fileContent, "FixedFile.txt");
            return RedirectToAction("Index");
        }

        private bool SaveFile(string content, string fileName)
        {
            bool ret = true;

            if (string.IsNullOrEmpty(content) || string.IsNullOrEmpty(fileName))
                return false;

            var path = Path.Combine(
                environment.WebRootPath,
                "TextFiles"
            );

            try
            {

                if (!System.IO.Directory.Exists(path))
                    System.IO.Directory.CreateDirectory(path);

                var filepath = Path.Combine(
                    path,
                    fileName
                );

                using (StreamWriter sw = System.IO.File.CreateText(filepath))
                {
                    sw.Write(content);
                }
            }
            catch (IOException ioEx)
            {
                string msg = ioEx.Message;
                ret = false;
            }
            catch (Exception ex)
            {
                string msg = ex.Message;
                ret = false;
            }

            return ret;
        }
    }
}