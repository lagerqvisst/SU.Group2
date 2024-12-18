﻿using SU.Backend.Models.Customers;

namespace SU.Backend.Services.Interfaces;

/// <summary>
///     This interface is responsible for defining the methods that the PrivateCustomerService class must implement.
/// </summary>
public interface IPrivateCustomerService
{
    Task<(bool success, string message, PrivateCustomer customer)> GetPrivateCustomerById(
        PrivateCustomer privateCustomer);

    Task<(bool success, string message)> CreateNewPrivateCustomer(PrivateCustomer privateCustomer);
    Task<(bool success, string message)> DeletePrivateCustomer(PrivateCustomer privateCustomer);
    Task<(bool success, string message)> UpdatePrivateCustomer(PrivateCustomer privateCustomer);
    Task<(bool success, string message, List<PrivateCustomer> privateCustomers)> GetAllPrivateCustomers();
}