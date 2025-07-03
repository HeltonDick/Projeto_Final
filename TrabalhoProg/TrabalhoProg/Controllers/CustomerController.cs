using Microsoft.AspNetCore.Mvc;
using TrabalhoProg.Modelo;
using TrabalhoProg.Repository;

namespace TrabalhoProg.Controllers
{
    public class CustomerController : Controller
    {
        private readonly IWebHostEnvironment environment;
        private readonly CustomerRepository _customerRepository;

        public CustomerController(IWebHostEnvironment environment)
        {
            this.environment = environment;
            _customerRepository = new CustomerRepository();
        }

        [HttpGet]
        public IActionResult Index()
        {
            var customers = _customerRepository.RetrieveAll();
            return View(customers);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View(new Customer());
        }

        [HttpPost]
        public IActionResult Create(Customer c)
        {
            _customerRepository.Save(c);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Update(int id)
        {
            var customer = _customerRepository.Retrieve(id);
            return View(customer);
        }

        [HttpPost]
        public IActionResult Update(Customer customer)
        {
            _customerRepository.Update(customer);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Delete(int? id)
        {
            if (id == null || id <= 0)
                return NotFound();

            var customer = _customerRepository.Retrieve(id.Value);
            if (customer == null)
                return NotFound();

            return View(customer);
        }

        [HttpPost]
        public IActionResult ConfirmDelete(int? id)
        {
            if (id == null || id <= 0)
                return NotFound();

            if (!_customerRepository.DeleteById(id.Value))
                return NotFound();

            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult ExportDelimitedFile()
        {
            var fileContent = string.Join("\n",
                CustomerData.Customers.Select(c =>
                    $"{c.CustomerId};{c.Name};{c.Email};{c.Phone};" +
                    $"{c.Address?.AddressId};{c.Address?.Street};{c.Address?.Street1};" +
                    $"{c.Address?.City};{c.Address?.State};{c.Address?.PostalCode};{c.Address?.Country}"
                )
            );

            SaveFile(fileContent, "DelimitedFile.txt");
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult ExportFixedFile()
        {
            var fileContent = string.Join("\n",
                CustomerData.Customers.Select(c =>
                    $"{c.CustomerId,5}{c.Name,-20}{c.Email,-30}{c.Phone,-15}" +
                    $"{c.Address?.AddressId,5}{c.Address?.Street,-32}{c.Address?.Street1,-32}" +
                    $"{c.Address?.City,-32}{c.Address?.State,-32}{c.Address?.PostalCode,-10}{c.Address?.Country,-20}"
                )
            );

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
