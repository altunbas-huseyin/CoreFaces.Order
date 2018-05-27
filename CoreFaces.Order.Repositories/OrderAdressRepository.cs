using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using CoreFaces.Order.Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CoreFaces.Order.Models.Domain;
using CoreFaces.Licensing;

namespace CoreFaces.Order.Repositories
{
    public interface IOrderAddressRepository : IBaseRepository<Models.Domain.OrderAddress>
    {
        List<Models.Domain.OrderAddress> GetAll();
    }

    public class OrderAddressRepository : Licence, IOrderAddressRepository
    {
        private readonly OrderDatabaseContext _orderDatabaseContext;

        public OrderAddressRepository(OrderDatabaseContext orderDatabaseContext, IOptions<OrderSettings> productSettings, IHttpContextAccessor iHttpContextAccessor) : base("Order", iHttpContextAccessor, productSettings.Value.OrderLicenseDomain, productSettings.Value.OrderLicenseKey)
        {
            _orderDatabaseContext = orderDatabaseContext;
        }

        public Models.Domain.OrderAddress GetById(Guid id)
        {
            Models.Domain.OrderAddress model = _orderDatabaseContext.Set<Models.Domain.OrderAddress>().Where(p => p.Id == id).FirstOrDefault();
            return model;
        }

        public Guid Save(Models.Domain.OrderAddress product)
        {
            _orderDatabaseContext.Add(product);
            _orderDatabaseContext.SaveChanges();
            return product.Id;
        }

        public bool Delete(Guid id)
        {
            Models.Domain.OrderAddress model = this.GetById(id);
            _orderDatabaseContext.Remove(model);
            int result = _orderDatabaseContext.SaveChanges();
            return Convert.ToBoolean(result);
        }

        public bool Update(Models.Domain.OrderAddress model)
        {
            _orderDatabaseContext.Update(model);
            int result = _orderDatabaseContext.SaveChanges();
            return Convert.ToBoolean(result);
        }

        public List<OrderAddress> GetAll()
        {
            List<Models.Domain.OrderAddress> model = _orderDatabaseContext.Set<Models.Domain.OrderAddress>().ToList();
            return model;
        }
    }

}
