using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using CoreFaces.Order.Models.Models;
using CoreFaces.Order.Repositories;
using System;
using System.Collections.Generic;
using System.Text;
using CoreFaces.Order.Models.Domain;
using Kendo.DynamicLinq;

namespace CoreFaces.Order.Services
{
    public interface IOrderItemService : IBaseService<Models.Domain.OrderItem>
    {
        List<Models.Domain.OrderItem> GetAll();
        List<OrderItem> GetAll(List<Guid> orderItems);
        decimal GetOrderTotalPoint(List<Guid> orderItems, List<int> statusList);
        List<Models.Domain.Order> Get(int take, int skip, List<Kendo.DynamicLinq.Sort> sort, Kendo.DynamicLinq.Filter filter, List<Aggregator> aggregates);
    }
    public class OrderItemService : IOrderItemService
    {
        private readonly IOrderItemRepository _orderItemRepository;
        private readonly OrderDatabaseContext _orderDatabaseContext;
        private readonly IOptions<OrderSettings> _ordertSettings;
        public OrderItemService(OrderDatabaseContext orderDatabaseContext, IOptions<OrderSettings> productSettings, IHttpContextAccessor iHttpContextAccessor)
        {
            _ordertSettings = productSettings;
            _orderDatabaseContext = orderDatabaseContext;
            _orderItemRepository = new OrderItemRepository(orderDatabaseContext, productSettings, iHttpContextAccessor);
           
        }

        public Models.Domain.OrderItem GetById(Guid id)
        {
            return _orderItemRepository.GetById(id);
        }

        public Guid Save(Models.Domain.OrderItem model)
        {
            _orderItemRepository.Save(model);
            return model.Id;
        }
       
        public bool Delete(Guid id)
        {
            return _orderItemRepository.Delete(id);
        }

        public bool Update(Models.Domain.OrderItem model)
        {
            return _orderItemRepository.Update(model);
        }

        public Models.Domain.OrderItem GetProductId(Guid Id)
        {
            Models.Domain.OrderItem model = _orderItemRepository.GetById(Id);
            return model;
        }

        public List<OrderItem> GetAll()
        {
            return _orderItemRepository.GetAll();
        }

        public List<OrderItem> GetAll(List<Guid> orderItems)
        {
            return _orderItemRepository.GetAll(orderItems);
        }

        public decimal GetOrderTotalPoint(List<Guid> orderItems, List<int> statusList)
        {
            return _orderItemRepository.GetOrderTotalPoint(orderItems, statusList);
        }

        public List<Models.Domain.Order> Get(int take, int skip, List<Sort> sort, Filter filter, List<Aggregator> aggregates)
        {
            return _orderItemRepository.Get(take, skip, sort, filter, aggregates);
        }
    }

}
