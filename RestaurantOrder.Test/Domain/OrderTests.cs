using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;
using RestaurantOrder.Domain.Entities;
using FluentAssertions;
using RestaurantOrder.Domain.Enums;
using RestaurantOrder.Domain.Events;

namespace RestaurantOrder.Test.Domain
{

    //     1.Create_WithValidCustomerId_ReturnsOrder
    //     Assert: Status == Pending, CustomerId correcto,
    //     TotalPrice == 0, CreatedAt es una fecha reciente

    public class OrderTests
    {
        [Theory]
        [InlineData("1")]
        [InlineData("customer")] //acepta nombres para el IdCustomer

        public void Create_WithValidCustomerId_ReturnsOrder(string o)
        {

            //Arrange 
            Order order = Order.Create(o);

            //Act
            order.Status.Should().Be(OrderStatus.Pending);
            order.TotalPrice.Should().Be(0);
            order.CreatedAt.Minute.Should().Be(DateTime.UtcNow.Minute);
        }


        //     Create_WithEmptyCustomerId_ThrowsArgumentException
        //    → Igual patrón que MenuItem

        [Fact]
        public void Create_WithEmptyCustomerId_ThrowsArgumentException()
        {
            // Given
            Action order = () => Order.Create("");

            // When
            order.Should().Throw<ArgumentException>("CustomerId cannot be null*");

            // Then
        }


        [Fact] //3
        public void Create_EmitsOrderCreatedEvent()
        {
            // Given
            var order = Order.Create("1");


            Assert.IsType<OrderCreatedEvent>(order.Events[0]);
        }

        [Fact] //4
        public void Confirm_FromPending_ChangesStatusToConfirmed()
        {
            var order = Order.Create("2");

            //act
            order.Confirm();

            //Arrange
            order.Status.Should().Be(OrderStatus.Confirmed);
        }


        [Fact]
        public void Confirm_FromNonPendingStatus_ThrowsInvalidOperationException()
        {
            // Given
            var order = Order.Create("23");

            // When
            order.Confirm();
            Action action = () => order.Confirm();
            // Then

            action.Should().Throw<InvalidOperationException>();



        }

        [Fact]
        public void StartPreparing_FromConfirmed_ChangesStatusToPreparing()
        {
            // Given
            var order = Order.Create("1");
            // When
            order.Confirm();
            order.StartPreparing();
            // Then
            order.Status.Should().Be(OrderStatus.Preparing);
        }

        [Fact]
        public void StartPreparing_FromPending_ThrowsInvalidOperationException()
        {
            // Given
            var order = Order.Create("1");

            // When
            Action action = () => order.StartPreparing();

            // Then

            action.Should().Throw<InvalidOperationException>();
        }


    }
}