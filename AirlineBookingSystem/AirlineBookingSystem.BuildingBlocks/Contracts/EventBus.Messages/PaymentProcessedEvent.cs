﻿namespace AirlineBookingSystem.BuildingBlocks.Contracts.EventBus.Messages
{
    public record PaymentProcessedEvent(Guid PaymentId,
                                        Guid BookingId,
                                        decimal Amount,
                                        DateTime PaymentDate);
}
