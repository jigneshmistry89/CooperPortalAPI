using System;
using System.Collections.Generic;

namespace Coopers.BusinessLayer.Model.DTO
{
    public class AccountSummary
    {

        public List<CustomerStatus> Status { get; set; }

        public List<Customer> Customers { get; set; }

    }


    public class CustomerStatus
    {
        public int Count { get; set; }

        public string Status  { get; set; }

        public string Title { get; set; }

        public double Amount { get; set; }

    }

    public class Customer
    {
        public long ID { get; set; }

        public string Name { get; set; }

        public string Address { get; set; }

        public DateTime ActivationDate { get; set; }

        public DateTime ExpiryDate { get; set; }

        public double Amount { get; set; }

        public string Status { get; set; }

        public string Subscription { get; set; }

        public string ContactName { get; set; }

        public string ContactEmail { get; set; }

        public string ContactNumber { get; set; }

        public int NumberOfSensors { get; set; }

        public int NumberOfGateways { get; set; }

    }

}
