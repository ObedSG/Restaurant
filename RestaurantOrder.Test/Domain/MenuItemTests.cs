using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RestaurantOrder.Domain.Entities;
using RestaurantOrder.Domain.Enums;
using FluentAssertions;
using Xunit;

namespace RestaurantOrder.Test.Domain;

public class MenuItemTests
{
    [Fact]
    public void Create_WithValidData_ReturnsMenuItem()
    {
        //Arrange
        string name = "Eggs", description = "Fried Eggs";
        decimal price = 2;
        MenuItemCategory category = new MenuItemCategory();

        //Act
        MenuItem menu = MenuItem.Create(name, description, price, category);

        //Assert
        menu.Should().NotBeNull();

    }

    [Fact]
    public void Create_WithEmptyName_ThrowsArgumentException()
    {
        //Arrange
        string name = " ", description = "Fried Eggs";
        decimal price = 2;
        MenuItemCategory category = new MenuItemCategory();

        //Act
        Action act = () => MenuItem.Create(name, description, price, category);

        //Assert
        act.Should().Throw<ArgumentException>();
    }

    [Theory]
    [InlineData("")]
    public void Create_WithEmptyDescription_ThrowsArgumentException(string d)
    {
        //Arrange
        string name = "Eggs";
        decimal price = 2;
        MenuItemCategory category = new MenuItemCategory();

        //Act
        Action act = () => MenuItem.Create(name, d, price, category);

        //Assert
        act.Should().Throw<ArgumentException>();
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-2)]

    public void Create_WithZeroOrNegativePrice_ThrowsArgumentException(decimal p)
    {
        //Arrange
        string name = "Eggs", description = "Fried eggs";
        // decimal price = -1;
        MenuItemCategory category = new MenuItemCategory();


        // Act
        Action act = () => MenuItem.Create(name, description, p, category);

        // Assert
        act.Should().Throw<ArgumentException>().WithMessage("Price must be greater than zero*"); // * Ignora el sufijo, lo que sigue despues.
    }

    [Fact]
    public void UpdatePrice_WithValidPrice_UpdatesPrice()
    {
        // Arrang
        string name = "soup";
        string description = "hot soup";
        decimal price = 5;
        MenuItemCategory category = MenuItemCategory.MainCourse;

        // When
        MenuItem menu = MenuItem.Create(name, description, price, category);
        menu.UpdatePrice(6);

        // Then
        menu.Price.Should().Be(6);
    }

    [Fact]
    public void UpdatePrice_WithInvalidPrice_UpdatesPrice()
    {

        //arrange
        var menu = MenuItem.Create("soup", "Hot", 6, MenuItemCategory.Beverage);

        //Act
        Action action = () => menu.UpdatePrice(-6);

        //Assert
        action.Should().Throw<ArgumentException>();

    }
}