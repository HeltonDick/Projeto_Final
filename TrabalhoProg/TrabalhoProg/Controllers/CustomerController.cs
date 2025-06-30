using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using TrabalhoProg.Models;
using Modelo;
using Repository;

namespace TrabalhoProg.Controllers
{
    public class CustomerController : Controller
    {
        private readonly IWebHostEnvironment environment;

        private CustomerRepository _customerRepository;

        public CustomerController(IWebHostEnvironment environment, CustomerRepository customerRepository) {
            this.environment = environment;
            _customerRepository = new customerRepository;
        }

        [HttpGet]
        public IActionResult Index() {
            List<Customer> customers = _customerRepository.RetrieveAll();

            return View(customers);
        }

        [HttpGet]
        public IActionResult Create() {
            _customerRepository.save(c)

            List<Customer> customers = _customerRepository.RetrieveAll();

            return View("Index", customers);
        }

        [HttpPost]
        public IActionResult Create() {
            return View();
        }

        [HttpGet]
        public IActionResult ExportDelimitatedFile() {
            string fileContent = string.empty;

            foreach (Customer c in CustomerData.customers)
            {
                fileContent = +$"{c.CustomerId};{c.Name};{c.Email};{c.Phone};{c.Address.AddressId};" +
                    $"{c.Address.Street};{c.Address.Street1};{c.Address.City};{c.Address.State};" +
                    $"{c.Address.PostalCode};{c.Address.Country})\n";

                SaveFile(fileContent, "DelimitatedFile.txt");

                return View();
            }
        }

        [HttpPost]
        public IActionResult ExportDelimitatedFile()
        {
            string fileContent = string.Empty;
            foreach (Customer c in CustomerData.Customers)
            {
                fileContent +=
                    String.Format("{0:5}{1:64}", c.CustomerId, c.Name, c.Email, c.Phone) +
                    String.Format("{0:5}", c.Address!.AddressId) +
                    String.Format("{0:32}", c.Address!.Street) +
                    String.Format("{0:2}", c.Address!.Street1) +
                    String.Format("{0:32}", c.Address!.City) +
                    String.Format("{0:64}", c.Address!.State) +
                    String.Format("{0:64}", c.Address!.PostalCode) +
                    String.Format("{0:64}", c.Address!.Country) +
                    "\n";
            }

            SaveFile(fileContent, "FixedFile.txt");
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Delete(int? id)
        {
            if (id is null || id.Value <= 0)
                return NotFound();

            Customer customer =
                _customerRepository.Retrieve(id.Value);

            if (customer == null)
                return NotFound();

            return View(customer);
        }

        [HttpPost]
        public IActionResult ConfirmDelete(int? id)
        {
            if (id is null || id.Value <= 0)
                return NotFound();

            if (!_customerRepository.DeleteById(id.Value))
                return NotFound();

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
