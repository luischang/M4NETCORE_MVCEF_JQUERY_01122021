using M4NETCORE_MVCEF_JQUERY.Web.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
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
            using var data = new SalesDB2021Context();
            return await data.Customer.ToListAsync();

        }

        public static async Task<Customer> GetCustomerById(int id)
        {
            using var data = new SalesDB2021Context();

            return await data.Customer.Where(x => x.Id == id).FirstOrDefaultAsync();
        }

        public static async Task<bool> Insert(Customer customer)
        {
            bool exito = true;
            try
            {
                using var data = new SalesDB2021Context();
                await data.Customer.AddAsync(customer);
                await data.SaveChangesAsync();
            }
            catch (Exception)
            {
                exito = false;
            }

            return exito;
        }

        public static async Task<bool> Update(Customer customer)
        {
            bool exito = true;
            try
            {
                using var data = new SalesDB2021Context();
                var customerDB = await data.Customer.Where(x => x.Id == customer.Id).FirstOrDefaultAsync();
                customerDB.FirstName = customer.FirstName;
                customerDB.LastName = customer.LastName;
                customerDB.City = customer.City;
                customerDB.Country = customer.Country;
                customerDB.Phone = customer.Phone;

                await data.SaveChangesAsync();

            }
            catch (Exception)
            {
                exito = false;
            }

            return exito;
        }
        /// <summary>
        /// Delete Customers by ID
        /// </summary>
        /// <param name="id">Customer ID</param>
        /// <returns></returns>
        public static async Task<bool> Delete(int id)
        {
            bool exito = true;
            try
            {
                using var data = new SalesDB2021Context();
                var customerDB = await data.Customer.FindAsync(id);
                data.Customer.Remove(customerDB);
                await data.SaveChangesAsync();
            }
            catch (Exception)
            {

                exito = false;
            }

            return exito;
        }

    }
}
