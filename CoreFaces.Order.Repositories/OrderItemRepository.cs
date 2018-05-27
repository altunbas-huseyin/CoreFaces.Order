using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using CoreFaces.Order.Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CoreFaces.Order.Models.Domain;
using CoreFaces.Licensing;
using Kendo.DynamicLinq;

namespace CoreFaces.Order.Repositories
{
    public interface IOrderItemRepository : IBaseRepository<Models.Domain.OrderItem>
    {
        List<Models.Domain.OrderItem> GetAll();
        List<Models.Domain.OrderItem> GetAll(List<Guid> orderItems);
        decimal GetOrderTotalPoint(List<Guid> orderItems, List<int> statusList);
        List<Models.Domain.Order> Get(int take, int skip, List<Kendo.DynamicLinq.Sort> sort, Kendo.DynamicLinq.Filter filter, List<Aggregator> aggregates);
    }

    public class OrderItemRepository : Licence, IOrderItemRepository
    {
        private readonly OrderDatabaseContext _productDatabaseContext;

        public OrderItemRepository(OrderDatabaseContext productDatabaseContext, IOptions<OrderSettings> productSettings, IHttpContextAccessor iHttpContextAccessor) : base("Order", iHttpContextAccessor, productSettings.Value.OrderLicenseDomain, productSettings.Value.OrderLicenseKey)
        {
            _productDatabaseContext = productDatabaseContext;
        }

        public Models.Domain.OrderItem GetById(Guid id)
        {
            Models.Domain.OrderItem model = _productDatabaseContext.Set<Models.Domain.OrderItem>().Where(p => p.Id == id).FirstOrDefault();
            return model;
        }

        public Guid Save(Models.Domain.OrderItem product)
        {
            _productDatabaseContext.Add(product);
            _productDatabaseContext.SaveChanges();
            return product.Id;
        }

        public bool Delete(Guid id)
        {
            Models.Domain.OrderItem model = this.GetById(id);
            _productDatabaseContext.Remove(model);
            int result = _productDatabaseContext.SaveChanges();
            return Convert.ToBoolean(result);
        }

        public bool Update(Models.Domain.OrderItem model)
        {
            _productDatabaseContext.Update(model);
            int result = _productDatabaseContext.SaveChanges();
            return Convert.ToBoolean(result);
        }

        public List<OrderItem> GetAll()
        {
            return _productDatabaseContext.Set<Models.Domain.OrderItem>().ToList();
        }

        public List<OrderItem> GetAll(List<Guid> orderItems)
        {
            return _productDatabaseContext.Set<Models.Domain.OrderItem>().Where(u => orderItems.Contains(u.OrderId)).ToList();
        }

        public decimal GetOrderTotalPoint(List<Guid> orderItems, List<int> statusList)
        {
            return _productDatabaseContext.Set<Models.Domain.OrderItem>().Where(u => orderItems.Contains(u.OrderId) && statusList.Contains(u.StatusId)).Sum(p => p.Price);

        }

        public List<Models.Domain.Order> Get(int take, int skip, List<Kendo.DynamicLinq.Sort> sort, Kendo.DynamicLinq.Filter filter, List<Aggregator> aggregates)
        {
            List<Models.Domain.Order> result = (List<Models.Domain.Order>)_productDatabaseContext.Set<Models.Domain.Order>()
                      .OrderBy(p => p.CreateDate) // EF requires ordering for paging
                   .ToDataSourceResult(take, skip, sort, filter, aggregates).Data;
            return result;
        }
    }

}
