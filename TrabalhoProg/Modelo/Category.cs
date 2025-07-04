﻿namespace TrabalhoProg.Modelo
{
    public class Category
    {
        public int CategoryId { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public bool Validate()
        {
            bool IsValid = true;

            IsValid = (CategoryId > 0) &&
                      (!string.IsNullOrEmpty(this.Name)) &&
                      (!string.IsNullOrEmpty(this.Description));

            return IsValid;
        }
    }
}