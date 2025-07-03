using Microsoft.AspNetCore.Mvc;
using TrabalhoProg.Repository;
using TrabalhoProg.Modelo;

namespace TrabalhoProg.Controllers
{
    public class CategoryController : Controller
    {
        private readonly IWebHostEnvironment environment;
        private readonly CategoryRepository _categoryRepository;

        public CategoryController(IWebHostEnvironment environment)
        {
            this.environment = environment;
            _categoryRepository = new CategoryRepository();
        }

        [HttpGet]
        public IActionResult Index()
        {
            var categories = _categoryRepository.RetrieveAll();
            return View(categories);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View(new Category());
        }

        [HttpPost]
        public IActionResult Create(Category category)
        {
            _categoryRepository.Save(category);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Delete(int? id)
        {
            if (id == null || id <= 0)
                return NotFound();

            var category = _categoryRepository.Retrieve(id.Value);
            if (category == null)
                return NotFound();

            return View(category);
        }

        [HttpPost]
        public IActionResult ConfirmDelete(int? id)
        {
            if (id == null || id <= 0)
                return NotFound();

            if (!_categoryRepository.DeleteById(id.Value))
                return NotFound();

            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Update(int id)
        {
            var category = _categoryRepository.Retrieve(id);
            if (category == null)
                return NotFound();

            return View(category);
        }

        [HttpPost]
        public IActionResult Update(Category category)
        {
            _categoryRepository.Update(category);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult ExportDelimitatedFile()
        {
            string fileContent = string.Join("\n", CategoryData.Categories
                .Select(c => $"{c.CategoryId};{c.Name};{c.Description}"));

            SaveFile(fileContent, "DelimitatedFile.txt");
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult ExportFixedFile()
        {
            string fileContent = string.Join("\n", CategoryData.Categories
                .Select(c => $"{c.CategoryId,-5}{c.Name,-64}{c.Description,-32}"));

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

                var filePath = Path.Combine(path, fileName);
                System.IO.File.WriteAllText(filePath, content);
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
