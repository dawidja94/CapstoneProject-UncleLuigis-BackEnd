﻿using RestaurantWebAPI.Models.ServiceRequests;
using RestaurantWebAPI.Models.ServiceResponses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestaurantWebAPI.Services
{
    public interface ICustomerService
    {
        GetCustomerResponse GetCustomer(GetCustomerRequest request);
        CreateCustomerResponse CreateCustomer(CreateCustomerRequest request);
        UpdateCustomerResponse UpdateCustomerByCustomer(UpdateCustomerRequest request);
        UpdateCustomerResponse UpdateCustomerAdministrative(UpdateCustomerRequest request);
    }
}
