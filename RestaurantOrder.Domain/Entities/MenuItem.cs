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

        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Name cannot be empty", nameof(name));
        if (string.IsNullOrWhiteSpace(description))
            throw new ArgumentException("Description cannot be empty", nameof(description));
        if (price <= 0)
            throw new ArgumentException("Price must be greater than zero", nameof(price));


        return new MenuItem
        {
            Name = name,
            Description = description,
            Price = price,
            Category = category,
            IsAvailable = true

        };

    }

    public void UpdatePrice(decimal newPrice)
    {
        if (newPrice <= 0)
            throw new ArgumentException("Price cannot be minor than zero");

        Price = newPrice;


    }

    public void SetAvailability(bool isAvailable)
    {
        IsAvailable = isAvailable;

    }



}