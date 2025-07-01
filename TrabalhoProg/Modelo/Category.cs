using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrabalhoProg.Modelo
{
    public class Category {
        public int CategoryId { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public bool Validate() {
            bool IsValid = true;

            IsValid = (CategoryId > 0) &&
                      (!string.IsNullOrEmpty(this.Name)) &&
                      (!string.IsNullOrEmpty(this.Description));

            return IsValid;
        }

        /*
        public Category Retrieve() {
            return new Category();
        }

        public void Save(Category category) {  
        }*/
    }
}