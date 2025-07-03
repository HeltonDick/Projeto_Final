using TrabalhoProg.Modelo;

namespace TrabalhoProg.Repository
{
    public class CustomerRepository
    {
        public Customer? Retrieve(int id)
        {
            return CustomerData.Customers.FirstOrDefault(c => c.CustomerId == id);
        }

        public List<Customer> RetrieveByName(string name)
        {
            return CustomerData.Customers
                .Where(c => !string.IsNullOrEmpty(c.Name) && c.Name.ToLower().Contains(name.ToLower()))
                .ToList();
        }

        public List<Customer> RetrieveAll()
        {
            return CustomerData.Customers;
        }

        public void Save(Customer customer)
        {
            customer.CustomerId = GetCount() + 1;
            CustomerData.Customers.Add(customer);
        }

        public bool Delete(Customer customer)
        {
            return CustomerData.Customers.Remove(customer);
        }

        public bool DeleteById(int id)
        {
            var customer = Retrieve(id);
            return customer != null && Delete(customer);
        }

        public void Update(Customer newCustomer)
        {
            var oldCustomer = Retrieve(newCustomer.CustomerId);
            if (oldCustomer != null)
            {
                oldCustomer.Name = newCustomer.Name;
                oldCustomer.Email = newCustomer.Email;
                oldCustomer.Phone = newCustomer.Phone;
                oldCustomer.Address = newCustomer.Address;
            }
        }

        public int GetCount()
        {
            return CustomerData.Customers.Count;
        }
    }
}
