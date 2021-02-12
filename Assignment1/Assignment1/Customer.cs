using System;
using System.Collections.Generic;
using System.Text;

namespace Assignment1
{
    public class Customer
    {
        public Customer(string firstName, string lastName, string streetNumber, string street, string city, string province, string postalCode, string country, string phoneNumber, string emailAddress,DateTime fileDate)
        {
            this.firstName = firstName;
            this.lastName = lastName;
            this.streetNumber = streetNumber;
            this.street = street;
            this.city = city;
            this.province = province;
            this.postalCode = postalCode;
            this.country = country;
            this.phoneNumber = phoneNumber;
            this.emailAddress = emailAddress;
            this.fileDate = fileDate;
        }

        public string firstName { get; set; }
        public string lastName { get; set; }
        public string streetNumber { get; set; }
        public string street { get; set; }
        public string city { get; set; }
        public string province { get; set; }
        public string postalCode { get; set; }
        public string country { get; set; }
        public string phoneNumber { get; set; }
        public string emailAddress { get; set; }

        public DateTime fileDate { get; set; }
    }

}
