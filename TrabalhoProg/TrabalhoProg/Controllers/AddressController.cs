using Microsoft.AspNetCore.Mvc;
using TrabalhoProg.Modelo;
using TrabalhoProg.Repository;

namespace TrabalhoProg.Controllers
{
    public class AddressController : Controller
    {
        private readonly IWebHostEnvironment environment;
        private readonly AddressRepository _addressRepository;

        public AddressController(IWebHostEnvironment environment)
        {
            this.environment = environment;
            _addressRepository = new AddressRepository();
        }

        [HttpGet]
        public IActionResult Index()
        {
            var addresses = _addressRepository.RetrieveAll();
            return View(addresses);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View(new Address());
        }

        [HttpPost]
        public IActionResult Create(Address address)
        {
            _addressRepository.Save(address);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Delete(int? id)
        {
            if (id is null || id.Value <= 0)
                return NotFound();

            var address = _addressRepository.Retrieve(id.Value);
            if (address == null)
                return NotFound();

            return View(address);
        }

        [HttpPost]
        public IActionResult ConfirmDelete(int? id)
        {
            if (id is null || id.Value <= 0)
                return NotFound();

            if (!_addressRepository.DeleteById(id.Value))
                return NotFound();

            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Update(int? id)
        {
            if (id is null || id.Value <= 0)
                return NotFound();

            var address = _addressRepository.Retrieve(id.Value);
            if (address == null)
                return NotFound();

            return View(address);
        }

        [HttpPost]
        public IActionResult Update(Address address)
        {
            _addressRepository.Update(address);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult ExportDelimitedFile()
        {
            string fileContent = string.Empty;
            foreach (var c in AddressData.Addresses)
            {
                fileContent += $"{c.AddressId};{c.Street};{c.Street1};{c.City};{c.State};{c.PostalCode};{c.Country}\n";
            }

            SaveFile(fileContent, "DelimitedFile.txt");
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult ExportFixedFile()
        {
            string fileContent = string.Empty;
            foreach (var c in AddressData.Addresses)
            {
                fileContent += string.Format("{0,-5}{1,-64}{2,-64}{3,-32}{4,-5}{5,-32}{6,-2}\n",
                    c.AddressId, c.Street, c.Street1, c.City, c.State, c.PostalCode, c.Country);
            }

            SaveFile(fileContent, "FixedFile.txt");
            return RedirectToAction("Index");
        }

        private bool SaveFile(string content, string fileName)
        {
            if (string.IsNullOrEmpty(content) || string.IsNullOrEmpty(fileName))
                return false;

            try
            {
                var path = Path.Combine(environment.WebRootPath, "TextFiles");

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
