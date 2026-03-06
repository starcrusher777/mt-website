using AutoMapper;
using MT.Domain.Entities;
using MT.Domain.Exceptions;
using MT.Domain.Interfaces;
using MT.Infrastructure.Models;

namespace MT.Application.Services;

public class OrderService
{
    private readonly IOrderRepository _orderRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly IFileRepository _fileRepository;
    
    public OrderService(IOrderRepository orderRepository, IUnitOfWork unitOfWork, IMapper mapper, IFileRepository fileRepository)
    {
        _orderRepository = orderRepository;
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _fileRepository = fileRepository;
    }
    
    public async Task CreateOrderAsync(OrderCreateFormModel orderModel)
    {
        var order = new OrderEntity
        {
            OrderName = orderModel.OrderName,
            Status = orderModel.Status,
            Type = orderModel.Type,
            UserId = orderModel.UserId,
            Item = new ItemEntity
            {
                Name = orderModel.Item.Name,
                Description = orderModel.Item.Description,
                Price = orderModel.Item.Price,
                Quantity = orderModel.Item.Quantity,
                Images = new List<ItemImageEntity>()
            }
        };

        if (orderModel.Images != null)
        {
            foreach (var file in orderModel.Images)
            {
                var fileName = $"{Guid.NewGuid()}_{file.FileName}";
                var filePath = Path.Combine("wwwroot/images", fileName);

                await using var stream = new FileStream(filePath, FileMode.Create);
                await file.CopyToAsync(stream);

                order.Item.Images.Add(new ItemImageEntity
                {
                    ImageUrl = $"/images/{fileName}"
                });
            }
        }
        else
        {
            Console.WriteLine("No files received");
        }

        await _orderRepository.CreateOrderAsync(order);
        await _unitOfWork.SaveChangesAsync();
    }

    public async Task<List<OrderEntity>> GetOrdersAsync()
    {
        var orders = await _orderRepository.GetOrdersAsync();
        return _mapper.Map<List<OrderEntity>>(orders);
    }

    public async Task<(List<OrderEntity> Orders, int TotalCount)> GetOrdersPagedAsync(int page, int pageSize)
    {
        var skip = (page - 1) * pageSize;
        var take = Math.Max(1, pageSize);
        var orders = await _orderRepository.GetOrdersAsync(skip, take);
        var total = await _orderRepository.GetOrdersCountAsync();
        return (orders, total);
    }

    public async Task<OrderEntity> GetOrderAsync(long orderId)
    {
        var order = await _orderRepository.GetOrderAsync(orderId);
        if (order == null)
            throw new NotFoundException("Order", orderId);
        return _mapper.Map<OrderEntity>(order);
    }

    public async Task<List<OrderEntity>> GetOrdersByUserIdAsync(long userId)
    {
        var orders = await _orderRepository.GetOrdersByUserIdAsync(userId);
        return orders;
    }

    public async Task<OrderEntity> UpdateOrderAsync(long orderId, OrderUpdateModel updatedOrder)
    {
        var order = await _orderRepository.GetOrderAsync(orderId);
        if (order == null)
            throw new NotFoundException("Order", orderId);
        
        order.OrderName = updatedOrder.OrderName;
        order.Status = updatedOrder.Status;
        order.Type = updatedOrder.Type;

        order.Item.Name = updatedOrder.OrderName;
        order.Item.Description = updatedOrder.Item.Description;
        order.Item.Price = updatedOrder.Item.Price;
        order.Item.Quantity = updatedOrder.Item.Quantity;
        
        if (updatedOrder.ImagesToDelete.Any())
        {
            var imagesToRemove = order.Item.Images
                .Where(img => updatedOrder.ImagesToDelete.Contains(img.Id))
                .ToList();

            foreach (var img in imagesToRemove)
            {
                _fileRepository.DeleteFileAsync(img.ImageUrl);
                order.Item.Images.Remove(img);
            }
        }
        
        if (updatedOrder.Images != null)
        {
            foreach (var file in updatedOrder.Images)
            {
                var fileName = $"{Guid.NewGuid()}_{file.FileName}";
                var filePath = Path.Combine("wwwroot/images", fileName);

                await using var stream = new FileStream(filePath, FileMode.Create);
                await file.CopyToAsync(stream);

                order.Item.Images.Add(new ItemImageEntity
                {
                    ImageUrl = $"/images/{fileName}"
                });
            }
        }
        
        order.ModifiedAt = DateTime.UtcNow;
        order.Item.ModifiedAt = DateTime.UtcNow;
        
        _orderRepository.UpdateOrder(order);
        await _unitOfWork.SaveChangesAsync();

        return order;
    }
}