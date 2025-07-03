using TrabalhoProg.Modelo;

namespace TrabalhoProg.Repository
{
    public class CategoryRepository
    {
        public Category? Retrieve(int id)
        {
            return CategoryData.Categories.FirstOrDefault(c => c.CategoryId == id);
        }

        public List<Category> RetrieveByName(string name)
        {
            return CategoryData.Categories
                .Where(c => c.Name != null && c.Name.ToLower().Contains(name.ToLower()))
                .ToList();
        }

        public List<Category> RetrieveAll()
        {
            return CategoryData.Categories;
        }

        public void Save(Category category)
        {
            category.CategoryId = GetCount() + 1;
            CategoryData.Categories.Add(category);
        }

        public bool Delete(Category category)
        {
            return CategoryData.Categories.Remove(category);
        }

        public bool DeleteById(int id)
        {
            var category = Retrieve(id);
            if (category != null)
                return Delete(category);
            return false;
        }

        public void Update(Category newCategory)
        {
            var oldCategory = Retrieve(newCategory.CategoryId);
            if (oldCategory != null)
            {
                oldCategory.Name = newCategory.Name;
                oldCategory.Description = newCategory.Description;
            }
        }

        public int GetCount()
        {
            return CategoryData.Categories.Count;
        }
    }
}
