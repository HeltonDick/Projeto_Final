using Modelo;

namespace Modelo
{
    public class LodgeProperty {
        public int LodgePropertyId { get; set; }
        public double PricePerNight { get; set; }
        public Property? Property { get; set; }
        public DateTime LodgeDate { get; set; } = DateTime.Now;

        public bool Validate() {
            bool IsValid = true;

            IsValid = (LodgePropertyId > 0) &&
                      (PricePerNight > 0) &&
                      (Property != null);

            return IsValid;
        }

        public LodgeProperty Retrieve() {
            return new LodgeProperty();
        }

        public void Save(LodgeProperty lodgeProperty) {
        }
    }
}