using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestaurantOrder.Domain.Entities;

public class OrderItem
{
    public int Id { get; set; }
    public int OrderId { get; set; }
    public int MenuItemId { get; set; }
    public string MenuItemName { get; set; }
    public int Quantity { get; set; }
    public decimal PriceAtOrderTime { get; set; }



    private OrderItem()
    {

    }

    public static OrderItem Create(int menuItemId, string menuItemName, int quantity, decimal priceAtOrderTime)
    {
        if (string.IsNullOrWhiteSpace(menuItemName))
            throw new ArgumentException("MenuItemName cannot be null", nameof(menuItemName));

        if (quantity <= 0)
            throw new ArgumentException("quantity must be greater than 0", nameof(quantity));

        if (priceAtOrderTime <= 0)
            throw new ArgumentException("priceAtOrderTime must be greater than 0", nameof(priceAtOrderTime));

        return new OrderItem
        {
            MenuItemId = menuItemId,
            MenuItemName = menuItemName,
            Quantity = quantity,
            PriceAtOrderTime = priceAtOrderTime
        };


    }
    public decimal CalculateSubtotal()
    {
        return Quantity * PriceAtOrderTime;
    }
}