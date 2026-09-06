using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestaurantOrder.Domain.Events;

public abstract class DomainEvent
{
    public int OrderId { get; set; }
    public DateTime OccurredAt { get; set; } = DateTime.UtcNow;



}