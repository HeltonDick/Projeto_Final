using Modelo;

namespace Repository {
    public class AddressRepository {
        public Address Retrieve(int id)
        {
            foreach (Address address in AddressData.Addresses)
            {
                if (address.AddressId == id)
                {
                    return endereco;
                }
            }
            return null!;
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
            foreach (Address address in AddressData.Addresses)
            {
                if (address.AddressId == id)
                {
                    return AddressData.Addresses.Remove(address);
                }
            }
            return false;
        }
        public void Update(Address newAddress)
        {
            Address oldAddress = Retrieve(newAddress.AddressId);
            oldAddress.Street = newAddress.Street;
            oldAddress.Street1 = newAddress.Street1;
            oldAddress.City = newAddress.City;
            oldAddress.State = newAddress.State;
            oldAddress.PostalCode = newAddress.PostalCode;
            oldAddress.Country = newAddress.Country;
        }
    }
}