using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using TrabalhoProg.Models;
using Modelo;
using Repository;

namespace TrabalhoProg.Controllers
{
    public class CategoryController : Controller
    {
        private readonly IWebHostEnvironment environment;
        private readonly CategoryRepository _categoryRepository;
        public CategoryController(IWebHostEnvironment environment)
        {
            this.environment = environment;
            _categoryRepository = categoryRepository;
        }

        [HttpGet]
        public IActionResult Index() {
            List<Category> categories = _categoryRepository.RetrieveAll();
            return View(categories);
        }

        [HttpGet]
        public IActionResult Create()
        {
            Category categories = new Category();
            return View(categories);
        }

        [HttpPost]
        public IActionResult Create(Category category)
        {
            _categoryRepository.Save(category);

            List<Category> category =
                _categoryRepository.RetrieveAll();

            return View("Index", category);
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            _categoryRepository.DeleteById(id);
            var realState = _categoryRepository.RetrieveAll();
            return View("Index", realState);
        }

        [HttpGet]
        public IActionResult Update(int id)
        {
            Category category = _categoryRepository.Retrieve(id);

            return View(category);
        }

        [HttpPost]
        public IActionResult Update(Category category)
        {
            _categoryRepository.Update(category);
            var categories = _categoryRepository.RetrieveAll();
            return View("Index", categories);
        }
    }
}