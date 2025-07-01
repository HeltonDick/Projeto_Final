using TrabalhoProg.Modelo;

namespace TrabalhoProg.Repository
{
    public class CustomerRepository
    {
        public Customer Retrieve(int id) {
            foreach (Customer c in CustomerData.Customers) {
                if (c.CustomerId == id)
                    return c;

                
            }
            return null!;
        }

        public List<Customer> RetrieveByName(string name)
        {
            List<Customer> ret = new List<Customer>();

            foreach (Customer c in CustomerData.Customers)
                if (c.Name!.ToLower().Contains(name.ToLower()))
                    ret.Add(c);

            return ret;
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
            Customer customer = Retrieve(id);

            if (customer != null)
                return Delete(customer);

            return false;
        }

        public void Update(Customer newCustomer)
        {
            Customer oldCustomer = Retrieve(newCustomer.CustomerId);
            oldCustomer.Name = newCustomer.Name;
            oldCustomer.Email = newCustomer.Email;
            oldCustomer.Phone = newCustomer.Phone;
            oldCustomer.Address = newCustomer.Address;
        }

        public int GetCount()
        {
            return CustomerData.Customers.Count;
        }
    }
}