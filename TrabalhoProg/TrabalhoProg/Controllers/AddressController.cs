using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;

using TrabalhoProg.Modelo;


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
            List<Address> addresses = _AddressRepository.RetrieveAll();

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
            _AddressRepository.Save(address);

            List<Address> addresses =
                _AddressRepository.RetrieveAll();

            return View("Index", addresses);
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            _AddressRepository.DeleteById(id);
            var realState = _AddressRepository.RetrieveAll();
            return View("Index", realState);
        }

        [HttpPost]
        public IActionResult Update(Address address)
        {
            _AddressRepository.Update(address);
            var addresses = _AddressRepository.RetrieveAll();
            return View("Index", addresses);
        }
    }
}