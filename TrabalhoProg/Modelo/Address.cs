using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrabalhoProg.Modelo
{
    public class Address() {
        public int AddressId { get; set; }
        public string? Street { get; set; }
        public string? Street1 { get; set; }
        public string? City { get; set; }
        public string? State { get; set; }
        public string? PostalCode { get; set; }
        public string? Country { get; set; }

        public bool Validate() {
            bool IsValid = true;

            IsValid = (AddressId > 0) &&
                      (!string.IsNullOrEmpty(this.Street)) &&
                      (!string.IsNullOrEmpty(this.City)) &&
                      (!string.IsNullOrEmpty(this.State)) &&
                      (!string.IsNullOrEmpty(this.PostalCode)) &&
                      (!string.IsNullOrEmpty(this.Country));

            return IsValid;
        }

        /*public Address Retrieve() {
            return new Address();
        }

        public void Save(Address address) {
        }*/
    }
}