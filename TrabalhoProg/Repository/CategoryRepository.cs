using TrabalhoProg.Modelo;

namespace TrabalhoProg.Repository {

    public class CategoryRepository
    {
        public Category Retrieve(int id)
        {
            foreach (Category c in CategoryData.Categories)
            {
                if (c.CategoryId == id)
                    return c;

                
            }
            return null!;
        }

        public List<Category> RetrieveByName(string name)
        {
            List<Category> ret = new List<Category>();

            foreach (Category c in CategoryData.Categories)
                if (c.Name!.ToLower().Contains(name.ToLower()))
                    ret.Add(c);

            return ret;
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
            Category category = Retrieve(id);

            if (category != null)
                return Delete(category);

            return false;
        }

        public void Update(Category newCategory)
        {
            Category oldCategory = Retrieve(newCategory.CategoryId);
            oldCategory.Name = newCategory.Name;
            oldCategory.Description = newCategory.Description;
        }

        public int GetCount()
        {
            return CategoryData.Categories.Count;
        }
    }
    
}