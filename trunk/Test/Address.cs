using System;
using System.Collections.Generic;
using System.Text;

namespace ObjectBindingSourceDemo
{
    public class Address : BusinessBase
    {
        private string _zipCode;
        private string _streetAddress;
        private string _city;
        private string _country;


        public Address()
        {
        }

        public Address(string zipCode, string streetAddress, string city, string country)
        {
            _zipCode = zipCode;
            _streetAddress = streetAddress;
            _city = city;
            _country = country;
        }


        public string ZipCode
        {
            get { return _zipCode; }
            set {
                _zipCode = value;
                OnPropertyChanged("ZipCode");
            }
        }

        public string StreetAddress
        {
            get { return _streetAddress; }
            set { 
                _streetAddress = value;
                OnPropertyChanged("StreetAddress");
            }
        }

        public string City
        {
            get { return _city; }
            set { 
                _city = value;
                OnPropertyChanged("City");
            }
        }

        public string Country
        {
            get { return _country; }
            set { 
                _country = value;
                OnPropertyChanged("Country");
            }
        }

        public override string ToString()
        {
            return String.Format("<Address:{0}>", _streetAddress);
        }

    }
}
