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
    public interface IOrderRepository : IBaseRepository<Models.Domain.Order>
    {
        List<Models.Domain.Order> GetAll();
        List<Models.Domain.Order> GetAll(Guid UserId);
        //All rows count.
        int GetRowsCount();
        List<Models.Domain.Order> GetByUserId(Guid userId);
        List<Models.Domain.Order> GetByUserId(Guid userId, List<int> statusList);
    }

    public class OrderRepository : Licence, IOrderRepository
    {
        private readonly OrderDatabaseContext _orderDatabaseContext;

        public OrderRepository(OrderDatabaseContext orderDatabaseContext, IOptions<OrderSettings> productSettings, IHttpContextAccessor iHttpContextAccessor) : base("Order", iHttpContextAccessor, productSettings.Value.OrderLicenseDomain, productSettings.Value.OrderLicenseKey)
        {
            _orderDatabaseContext = orderDatabaseContext;
        }

        public Models.Domain.Order GetById(Guid id)
        {
            Models.Domain.Order model = _orderDatabaseContext.Set<Models.Domain.Order>().Where(p => p.Id == id).FirstOrDefault();
            return model;
        }

        public Guid Save(Models.Domain.Order product)
        {
            _orderDatabaseContext.Add(product);
            _orderDatabaseContext.SaveChanges();
            return product.Id;
        }

        public bool Delete(Guid id)
        {
            Models.Domain.Order model = this.GetById(id);
            _orderDatabaseContext.Remove(model);
            int result = _orderDatabaseContext.SaveChanges();
            return Convert.ToBoolean(result);
        }

        public bool Update(Models.Domain.Order model)
        {
            _orderDatabaseContext.Update(model);
            int result = _orderDatabaseContext.SaveChanges();
            return Convert.ToBoolean(result);
        }

        public int GetRowsCount()
        {
            return _orderDatabaseContext.Set<Models.Domain.Order>().Count();
        }

        public List<Models.Domain.Order> GetAll()
        {
            return _orderDatabaseContext.Set<Models.Domain.Order>().ToList();
        }

        public List<Models.Domain.Order> GetByUserId(Guid userId)
        {
            return _orderDatabaseContext.Set<Models.Domain.Order>().Where(p => p.UserId == userId).ToList();
        }

        public List<Models.Domain.Order> GetAll(Guid UserId)
        {
            return _orderDatabaseContext.Set<Models.Domain.Order>().Where(p => p.UserId == UserId).ToList();
        }

        public List<Models.Domain.Order> GetByUserId(Guid userId, List<int> statusList)
        {
            return _orderDatabaseContext.Set<Models.Domain.Order>().Where(p => p.UserId == userId && statusList.Contains(p.StatusId)).ToList();
        }
    }

}
