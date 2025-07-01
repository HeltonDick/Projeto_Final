namespace TrabalhoProg.Modelo
{
    public class Property
    {
        public int PropertyId { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public int BedRooms { get; set; }
        public int GarageVacancies { get; set; }
        public Address? Address { get; set; }
        public Category? Category { get; set; }
        public double CurrentPricePerNight { get; set; }

        public static int instanceCount = 0;
        public int objectCount = 0;

        public bool Validate() {
            bool IsValid = true;

            IsValid = (PropertyId > 0) &&
                      (!string.IsNullOrEmpty(this.Name)) &&
                      (!string.IsNullOrEmpty(this.Description)) &&
                      (BedRooms < 0) &&
                      (GarageVacancies < 0) &&
                      (Address != null) &&
                      (Category != null) &&
                      (CurrentPricePerNight < 0);

            return IsValid;
        }

        public Property Retrieve() {
            return new Property();
        }

        public void Save(Property property) {
        }
    }
}