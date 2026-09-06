namespace RestaurantOrder.Domain.Enums;

public enum OrderStatus
{
    Pending,    // esperando comfirmacion
    Confirmed,  // comfirmado
    Preparing, //Preparando
    Ready,      // Listo
    Delivered,   // Entregado

    Cancelled   // Cancelado
    
}