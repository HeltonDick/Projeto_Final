using System.Diagnostics;
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
            List<Address> addresses = _addressRepository.RetrieveAll();

            return View(addresses);
        }

        [HttpGet]
        public IActionResult Create()
        {
            Address address = new Address();

            return View(address);
        }

        [HttpPost]
        public IActionResult Create(Address address)
        {
            _addressRepository.Save(address);

            List<Address> addresses =
                _addressRepository.RetrieveAll();

            return View("Index", addresses);
        }

        [HttpGet]
        public IActionResult Delete(int? id)
        {
            if (id is null || id.Value <= 0)
                return NotFound();

            Address address =
                _addressRepository.Retrieve(id.Value);

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

        [HttpPost]
        public IActionResult Update(Address address)
        {
            _addressRepository.Update(address);
            var addresses = _addressRepository.RetrieveAll();
            return View("Index", addresses);
        }


        [HttpGet]
        public IActionResult ExportDelimitedFile()
        {
            string fileContent = string.Empty;
            foreach (Address c in AddressData.Addresses)
            {
                fileContent +=
                    $"{c.AddressId};{c.Street};{c.Street1};{c.City};" +
                    $"{c.State};{c.PostalCode};" +
                    $"{c.Country}\n";
            }
            SaveFile(fileContent, "DelimitedFile.txt");
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult ExportFixedFile()
        {
            string fileContent = string.Empty;
            foreach (Address c in AddressData.Addresses)
            {
                fileContent +=
                    String.Format("{0:5}{1:64}", c.AddressId, c.Street, c.Street1, c.City) +
                    String.Format("{0:5}", c.State) +
                    String.Format("{0:32}", c.Street) +
                    String.Format("{0:2}", c.PostalCode) +
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