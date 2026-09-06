using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RestaurantOrder.Domain.Enums;

namespace RestaurantOrder.Domain.Entities;

public class MenuItem
{

    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public decimal Price { get; set; }
    public MenuItemCategory Category { get; set; }

    public bool IsAvailable { get; set; }

    private MenuItem()
    {
        
        
    }

    public static MenuItem Create(string name, string description, decimal price, MenuItemCategory category)
    {



        return new MenuItem
        {
            Name = name,
            Description = description,
            Price = price,
            Category = category

        };

    }

    public void UpdatePrice(decimal newPrice)
    {
        if (newPrice > 0)
        {
            Price = newPrice;
        }

    }

    public void SetAvailability(bool isAvailable)
    {
        IsAvailable = isAvailable;
    }



}