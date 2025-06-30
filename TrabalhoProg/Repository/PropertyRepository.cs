using Modelo;

namespace Repository {
    public class PropertyRepository {
        public Property Retrieve(int id) {
            foreach (Property p in CustomerData.Properties) {
                if (p.PropertyId == id)
                    return p;
                return null!;
            }
        }

        public List<Property> RetrieveByName(string name)
        {
            List<Property> ret = new List<Property>();

            foreach (Property p in CustomerData.Properties)
                if (p.Name!.ToLower().Contains(name.ToLower()))
                    ret.Add(p);

            return ret;
        }

        public List<Property> RetrieveAll()
        {
            return CustomerData.Properties;
        }

        public void Save(Property property)
        {
            property.PropertyId = GetCount() + 1;
            CustomerData.Properties.Add(property);
        }

        public bool Delete(Property property)
        {
            return CustomerData.Properties.Remove(property);
        }

        public bool DeleteById(int id)
        {
            Property property = Retrieve(id);

            if (property != null)
                return Delete(property);

            return false;
        }

        public void Update(Property newProperty)
        {
            Property oldProperty = Retrieve(newProperty.PropertyId);
            oldProperty.Name = newProperty.Name;
            oldProperty.Description = newProperty.Description;
            oldProperty.BedRooms = newProperty.BedRooms;
            oldProperty.GarageVacancies = newProperty.GarageVacancies;
            oldProperty.Address = newProperty.Address;
            oldProperty.Category = newProperty.Category;
            oldProperty.CurrentPricePerNight = newProperty.CurrentPricePerNight;
        }

        public int GetCount()
        {
            return CustomerData.Properties.Count;
        }
    }
}