using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestaurantOrder.Domain.Events;

public class OrderCreatedEvent : DomainEvent
{
    public OrderCreatedEvent(int orderId) => OrderId = orderId;
}

public class OrderConfirmedEvent : DomainEvent
{
    public OrderConfirmedEvent(int orderId) => OrderId = orderId;
}

public class OrderPreparingEvent : DomainEvent
{
    public OrderPreparingEvent(int orderId) => OrderId = orderId;
}

public class OrderReadyEvent : DomainEvent
{
    public OrderReadyEvent(int orderId) => OrderId = orderId;
}

public class OrderDeliveredEvent : DomainEvent
{
    public OrderDeliveredEvent(int orderId) => OrderId = orderId;
}

public class OrderCancelledEvent : DomainEvent
{
    public string Reason { get; private set; }
    public OrderCancelledEvent(int orderId, string reason)
    {
        Reason = reason;
        OrderId = orderId;
    }
}

public class ItemAddedToOrderEvent : DomainEvent
{
    public int MenuItemId { get; set; }
    public int Quantity { get; set; }
    public ItemAddedToOrderEvent(int orderId, int menuItemId, int quantity)
    {
        OrderId = orderId;
        MenuItemId = menuItemId;
        Quantity = quantity;
    }
}

