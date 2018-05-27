using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using CoreFaces.Order.Models.Models;
using CoreFaces.Order.Repositories;
using System;
using System.Collections.Generic;
using System.Text;
using CoreFaces.Order.Models.Domain;

namespace CoreFaces.Order.Services
{
    public interface IOrderAddressService : IBaseService<Models.Domain.OrderAddress>
    {
        List<Models.Domain.OrderAddress> GetAll();
    }
    public class OrderAddressService : IOrderAddressService
    {
        private readonly IOrderAddressRepository _orderAddressRepositoryRepository;
        private readonly OrderDatabaseContext _orderDatabaseContext;
        private readonly IOptions<OrderSettings> _ordertSettings;
        public OrderAddressService(OrderDatabaseContext orderDatabaseContext, IOptions<OrderSettings> productSettings, IHttpContextAccessor iHttpContextAccessor)
        {
            _ordertSettings = productSettings;
            _orderDatabaseContext = orderDatabaseContext;
            _orderAddressRepositoryRepository = new OrderAddressRepository(orderDatabaseContext, productSettings, iHttpContextAccessor);
        }

        public Models.Domain.OrderAddress GetById(Guid id)
        {
            return _orderAddressRepositoryRepository.GetById(id);
        }

    
        public bool Delete(Guid id)
        {
            return _orderAddressRepositoryRepository.Delete(id);
        }

        public bool Update(Models.Domain.OrderAddress model)
        {
            return _orderAddressRepositoryRepository.Update(model);
        }

        public Models.Domain.OrderAddress GetProductId(Guid Id, Guid apiUserId)
        {
            Models.Domain.OrderAddress model = _orderAddressRepositoryRepository.GetById(Id);
            return model;
        }

        public Guid Save(Models.Domain.OrderAddress model)
        {
            _orderAddressRepositoryRepository.Save(model);
            return model.Id;
        }

        public List<OrderAddress> GetAll()
        {
            return _orderAddressRepositoryRepository.GetAll();
        }
    }

}
