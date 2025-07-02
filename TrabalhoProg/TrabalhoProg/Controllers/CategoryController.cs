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

            List<Category> categories =
                _categoryRepository.RetrieveAll();

            return View("Index", categories);
        }

        [HttpGet]
        public IActionResult Delete(int? id)
        {
            if (id is null || id.Value <= 0)
                return NotFound();

            Category category=
                _categoryRepository.Retrieve(id.Value);

            if (category == null)
                return NotFound();

            return View(category);
        }

        [HttpPost]
        public IActionResult ConfirmDelete(int? id)
        {
            if (id is null || id.Value <= 0)
                return NotFound();

            if (!_categoryRepository.DeleteById(id.Value))
                return NotFound();

            return RedirectToAction("Index");
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

        [HttpGet]
        public IActionResult ExportDelimitatedFile()
        {
            string fileContent = string.Empty;
            foreach (Category c in CategoryData.Categories)
            {
                fileContent +=
                    $"{c.CategoryId};{c.Name};{c.Description}\n";
            }

            SaveFile(fileContent, "DelimitatedFile.txt");

            return View();
        }

        [HttpGet]
        public IActionResult ExportFixedFile()
        {
            string fileContent = string.Empty;
            foreach (Category c in CategoryData.Categories)
            {
                fileContent +=
                    String.Format("{0:5}{1:64}", c.CategoryId, c.Name) +
                    String.Format("{0:5}", c.Description) +
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