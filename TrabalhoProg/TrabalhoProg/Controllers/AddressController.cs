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
        public IActionResult Delete(int id)
        {
            _addressRepository.DeleteById(id);
            var realState = _addressRepository.RetrieveAll();
            return View("Index", realState);
        }

        [HttpPost]
        public IActionResult Update(Address address)
        {
            _addressRepository.Update(address);
            var addresses = _addressRepository.RetrieveAll();
            return View("Index", addresses);
        }
    }
}