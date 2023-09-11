﻿using AutoMapper;
using Storage.Application.DTOs.OrderItem;
using Storage.Application.DTOs.Order;
using Storage.Application.Features.Order.Commands;
using Storage.Application.Features.Order.Queries;
using Storage.Application.Interfaces;
using Storage.Application.Parameters.Order;
using Storage.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Storage.Application.Features.OrderItem.Queries;
using Storage.Application.Features.OrderItem.Commands;

namespace Storage.Application.Mappings
{
    public class OrderProfile : Profile
    {
        private readonly IOrderRepository _OrderRepo;
        public OrderProfile(IOrderRepository OrderRepo)
        {
            _OrderRepo = OrderRepo;

            CreateMap<GetAllOrderQuery, OrderDTO>();
            CreateMap<GetOrderByIdQuery, OrderDTO>();
            CreateMap<CreateOrderDTO, Order>();
            CreateMap<EditOrderDTO, Order>();
            CreateMap<DeleteOrderCommand, Order>();
            CreateMap<CreateOrderDTO, Order>();
            CreateMap<EditOrderDTO, Order>();
            CreateMap<Order, OrderDTO>()
              .PreserveReferences()
              .ForMember(d => d.OrderItems, opt => opt.MapFrom(src => _OrderRepo.GetItemsByOrderId(src.Id).Result));
            CreateMap<Order, GetAllOrderParameter>();
            CreateMap<GetAllOrderQuery, GetAllOrderParameter>();

            CreateMap<GetAllOrderItemQuery, OrderItemDTO>();
            CreateMap<GetOrderItemByIdQuery, OrderItemDTO>();
            CreateMap<CreateOrderItemDTO, OrderItem>()
     .ForMember(dest => dest.Order, opt => opt.Ignore());
            CreateMap<EditOrderItemDTO, OrderItem>()
                 .ForMember(dest => dest.Order, opt => opt.Ignore());
            CreateMap<DeleteOrderItemCommand,OrderItem>();
            CreateMap<OrderItem, OrderItemDTO>();
            CreateMap<CreateOrderItemDTO, OrderItem>();

        }
    }
}
