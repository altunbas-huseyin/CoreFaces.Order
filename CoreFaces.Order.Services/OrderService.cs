using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using CoreFaces.Order.Models.Models;
using CoreFaces.Order.Repositories;
using System;
using System.Collections.Generic;
using System.Text;
using CoreFaces.Order.Models.Domain;
using System.Linq;

namespace CoreFaces.Order.Services
{
    public interface IOrderService : IBaseService<Models.Domain.Order>
    {
        //Guid Save(Models.Domain.Order order, List<Models.Domain.OrderItem> orderItems);
        //Guid Save(Models.Domain.Order order, List<Models.Domain.OrderItem> orderItems, Models.Domain.OrderAddress orderAddress);
        List<Models.Domain.Order> GetAll();
        List<Models.Domain.Order> GetAll(Guid UserId);
        int GetRowsCount();
        string GenerateOrderNumber();
        List<Models.Domain.Order> GetByUserId(Guid userId);
        decimal GetOrderTotalPoint(Guid userId, List<int> statusList);
        List<Models.Domain.Order> GetByUserId(Guid userId, List<int> statusList);
    }
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IOrderItemRepository _orderItemRepository;
        private readonly IOrderAddressRepository _orderAddressRepository;
        private readonly OrderDatabaseContext _orderDatabaseContext;
        private readonly IOptions<OrderSettings> _ordertSettings;
        public OrderService(OrderDatabaseContext orderDatabaseContext, IOptions<OrderSettings> orderSettings, IHttpContextAccessor iHttpContextAccessor)
        {
            _ordertSettings = orderSettings;
            _orderDatabaseContext = orderDatabaseContext;
            _orderRepository = new OrderRepository(orderDatabaseContext, orderSettings, iHttpContextAccessor);
            _orderItemRepository = new OrderItemRepository(orderDatabaseContext, orderSettings, iHttpContextAccessor);
            _orderAddressRepository = new OrderAddressRepository(orderDatabaseContext, orderSettings, iHttpContextAccessor);
        }

        public Models.Domain.Order GetById(Guid id)
        {
            return _orderRepository.GetById(id);
        }

        public bool Delete(Guid id)
        {
            return _orderRepository.Delete(id);
        }

        public bool Update(Models.Domain.Order model)
        {
            return _orderRepository.Update(model);
        }

        public Models.Domain.Order GetProductId(Guid Id, Guid apiUserId)
        {
            Models.Domain.Order model = _orderRepository.GetById(Id);
            return model;
        }

        public Guid Save(Models.Domain.Order model)
        {
            model.OrderNumber = GenerateOrderNumber();
            _orderRepository.Save(model);
            return model.Id;
        }

        public int GetRowsCount()
        {
            return _orderRepository.GetRowsCount();
        }

        public string GenerateOrderNumber()
        {
            int rowCount = GetRowsCount() + 1000;
            return (rowCount + 1).ToString();
        }

        public List<Models.Domain.Order> GetAll()
        {
            return _orderRepository.GetAll();
        }

        public List<Models.Domain.Order> GetByUserId(Guid userId)
        {
            return _orderRepository.GetByUserId(userId);
        }

        public decimal GetOrderTotalPoint(Guid userId, List<int> statusList)
        {
            List<Models.Domain.Order> orders = GetByUserId(userId, statusList);
            orders = orders.Where(p => p.StatusId != 5).ToList();
            List<Guid> orderIdList = new List<Guid>();
            foreach (var item in orders)
            {
                orderIdList.Add(item.Id);
            }

            _orderItemRepository.GetOrderTotalPoint(orderIdList, statusList);
            return _orderItemRepository.GetOrderTotalPoint(orderIdList, statusList);
        }

        public List<Models.Domain.Order> GetAll(Guid UserId)
        {
            return _orderRepository.GetAll(UserId);
        }

        public List<Models.Domain.Order> GetByUserId(Guid userId, List<int> statusList)
        {
            return _orderRepository.GetByUserId(userId, statusList);
        }
    }

}
