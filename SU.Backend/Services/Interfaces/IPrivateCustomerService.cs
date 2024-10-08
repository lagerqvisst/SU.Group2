﻿using SU.Backend.Models.Customers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SU.Backend.Services.Interfaces
{
    public interface IPrivateCustomerService
    {
        Task<(bool Success, string Message, PrivateCustomer Customer)> GenerateRandomPrivateCustomer();
        Task<(bool Success, string Message, List<PrivateCustomer>)> GetPrivateCustomers();
        Task<(bool Success, string Message, PrivateCustomer Customer)> GetPrivateCustomerById(PrivateCustomer privateCustomer);
        Task<(bool Success, string Message, PrivateCustomer PrivateCustomer)> CreateNewPrivateCustomer(PrivateCustomer privateCustomer);



    }
}
