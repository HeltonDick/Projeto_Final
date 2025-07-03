using TrabalhoProg.Modelo;

namespace TrabalhoProg.Repository
{
    public class AddressRepository
    {
        public Address? Retrieve(int id)
        {
            return AddressData.Addresses.FirstOrDefault(address => address.AddressId == id);
        }

        public List<Address> RetrieveAll()
        {
            return AddressData.Addresses;
        }

        public void Save(Address address)
        {
            address.AddressId = AddressData.Addresses.Count + 1;
            AddressData.Addresses.Add(address);
        }

        public bool Delete(Address address)
        {
            return AddressData.Addresses.Remove(address);
        }

        public bool DeleteById(int id)
        {
            var address = Retrieve(id);
            if (address != null)
            {
                return AddressData.Addresses.Remove(address);
            }
            return false;
        }

        public void Update(Address newAddress)
        {
            var oldAddress = Retrieve(newAddress.AddressId);
            if (oldAddress != null)
            {
                oldAddress.Street = newAddress.Street;
                oldAddress.Street1 = newAddress.Street1;
                oldAddress.City = newAddress.City;
                oldAddress.State = newAddress.State;
                oldAddress.PostalCode = newAddress.PostalCode;
                oldAddress.Country = newAddress.Country;
            }
        }
    }
}
