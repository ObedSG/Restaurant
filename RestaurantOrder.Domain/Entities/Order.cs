using System;
using RestaurantOrder.Domain.Enums;
using RestaurantOrder.Domain.Events;
namespace RestaurantOrder.Domain.Entities;


public class Order
{
    // Propiedades

    public int Id { get; set; }

    public string CustomerId { get; set; }

    public OrderStatus Status { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdateAt { get; set; }

    public decimal TotalPrice { get; set; }


    private readonly List<DomainEvent> _events = new();
    public IReadOnlyList<DomainEvent> Events => _events.AsReadOnly();

    // End Properties


    //cto privado para solo instancear con metodo estatico (Factory Method)
    private Order()
    {

    }


    //Fcatory Method
    public static Order Create(string customerId)
    {
        if (string.IsNullOrWhiteSpace(customerId))
            throw new ArgumentException("CustomerId cannot be null", nameof(customerId));

        var order = new Order
        {
            CustomerId = customerId,
            Status = OrderStatus.Pending,
            CreatedAt = DateTime.UtcNow,
            UpdateAt = DateTime.UtcNow,
            TotalPrice = 0

        };
        order._events.Add(new OrderCreatedEvent(order.Id));

        return order;

    }


    //Method State 
    public void Confirm()
    {
        if (Status == OrderStatus.Pending)
            Status = OrderStatus.Confirmed;
        else
            throw new InvalidOperationException($"Cannot confirm order. Current status: {Status}, expected: Pending");
        UpdateAt = DateTime.UtcNow;

        _events.Add(new OrderConfirmedEvent(Id));

    }

    public void StartPreparing()
    {
        if (Status == OrderStatus.Confirmed)
            Status = OrderStatus.Preparing;
        else
            throw new InvalidOperationException($"Cannot start order. Current status: {Status}, expected: Confirmed");
        UpdateAt = DateTime.UtcNow;
        _events.Add(new OrderPreparingEvent(Id));


    }

    public void MarkReady()
    {
        if (Status == OrderStatus.Preparing)
            Status = OrderStatus.Ready;
        else
            throw new InvalidOperationException($"Cannot mark order. Current status: {Status}, expected: Preparing");
        UpdateAt = DateTime.UtcNow;
        _events.Add(new OrderReadyEvent(Id));



    }

    public void Deliver()
    {
        if (Status == OrderStatus.Ready)
            Status = OrderStatus.Delivered;
        else
            throw new InvalidOperationException($"Cannot deliver order. Current status: {Status}, expected: Ready");
        UpdateAt = DateTime.UtcNow;
        _events.Add(new OrderDeliveredEvent(Id));


    }

    public void Cancel(string reason)
    {
        if (string.IsNullOrWhiteSpace(reason))
            throw new ArgumentException("Reason cannot be null", nameof(reason));

        if (Status == OrderStatus.Delivered || Status == OrderStatus.Cancelled)
            throw new InvalidOperationException($"Status cannot be status: {Status}");

        Status = OrderStatus.Cancelled;

        UpdateAt = DateTime.UtcNow;

        _events.Add(new OrderCancelledEvent(Id, reason));


    }

    public void ClearEvents() => _events.Clear();






}