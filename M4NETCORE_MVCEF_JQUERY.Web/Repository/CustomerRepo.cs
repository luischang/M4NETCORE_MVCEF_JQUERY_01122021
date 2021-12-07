using M4NETCORE_MVCEF_JQUERY.Web.Models;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace M4NETCORE_MVCEF_JQUERY.Web.Repository
{
    public class CustomerRepo
    {
        public static IEnumerable<Customer> GetCustomers()
        {
            using var data = new SalesDB2021Context();
            return data.Customer.ToList();
        }

        public static IEnumerable<Customer> GetCustomersSP()
        {
            using var data = new SalesDB2021Context();
            return data.Customer.FromSqlRaw("SP_GET_CUSTOMERS");
        }

        public static async Task<IEnumerable<Customer>> GetCustomersAsync()
        {
            //using var data = new SalesDB2021Context();
            //return await data.Customer.ToListAsync();
            using var httpClient = new HttpClient();
            using var response = await httpClient
                 .GetAsync("http://localhost:22846/api/customer/customer");
            string apiResponse = await response.Content.ReadAsStringAsync();
            var customers = JsonConvert.DeserializeObject<IEnumerable<Customer>>(apiResponse);
            return customers;
        }

        public static async Task<Customer> GetCustomerById(int id)
        {
            //using var data = new SalesDB2021Context();

            //return await data.Customer.Where(x => x.Id == id).FirstOrDefaultAsync();
            using var httpClient = new HttpClient();
            using var response = await httpClient
                 .GetAsync("http://localhost:22846/api/customer/customerbyid?id=" + id);
            string apiResponse = await response.Content.ReadAsStringAsync();
            var customer = JsonConvert.DeserializeObject<Customer>(apiResponse);
            return customer;
        }

        public static async Task<bool> Insert(Customer customer)
        {
            //bool exito = true;
            //try
            //{
            //    using var data = new SalesDB2021Context();
            //    await data.Customer.AddAsync(customer);
            //    await data.SaveChangesAsync();
            //}
            //catch (Exception)
            //{
            //    exito = false;
            //}

            var json = JsonConvert.SerializeObject(customer);
            var data = new StringContent(json, Encoding.UTF8, "application/json");
            using var httpClient = new HttpClient();
            using var response = await httpClient
                   .PostAsync("http://localhost:22846/api/customer/postcustomer", data);
            string apiresponse = await response.Content.ReadAsStringAsync();
            var customerResponse = JsonConvert.DeserializeObject<bool>(apiresponse);
            return customerResponse;
        }

        public static async Task<bool> Update(Customer customer)
        {
            //    bool exito = true;
            //    try
            //    {
            //        using var data = new SalesDB2021Context();
            //        var customerDB = await data.Customer.Where(x => x.Id == customer.Id).FirstOrDefaultAsync();
            //        customerDB.FirstName = customer.FirstName;
            //        customerDB.LastName = customer.LastName;
            //        customerDB.City = customer.City;
            //        customerDB.Country = customer.Country;
            //        customerDB.Phone = customer.Phone;

            //        await data.SaveChangesAsync();

            //    }
            //    catch (Exception)
            //    {
            //        exito = false;
            //    }

            var json = JsonConvert.SerializeObject(customer);
            var data = new StringContent(json, Encoding.UTF8, "application/json");
            using var httpClient = new HttpClient();
            using var response = await httpClient
                   .PutAsync("http://localhost:22846/api/customer/putcustomer", data);
            string apiresponse = await response.Content.ReadAsStringAsync();
            var customerResponse = JsonConvert.DeserializeObject<bool>(apiresponse);
            return customerResponse;

            //return exito;
        }
        /// <summary>
        /// Delete Customers by ID
        /// </summary>
        /// <param name="id">Customer ID</param>
        /// <returns></returns>
        public static async Task<bool> Delete(int id)
        {
            //bool exito = true;
            //try
            //{
            //    using var data = new SalesDB2021Context();
            //    var customerDB = await data.Customer.FindAsync(id);
            //    data.Customer.Remove(customerDB);
            //    await data.SaveChangesAsync();
            //}
            //catch (Exception)
            //{

            //    exito = false;
            //}

            //return exito;

            bool exito = true;
            using var httpClient = new HttpClient();
            using var response = await httpClient
                .DeleteAsync("http://localhost:22846/api/customer/deletecustomer?id=" + id);
            string apiResponse = await response.Content.ReadAsStringAsync();
            if ((int)response.StatusCode == 400)
                exito = false;
            return exito;

        }

    }
}
