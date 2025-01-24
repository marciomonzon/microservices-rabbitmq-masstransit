﻿using AirlineBookingSystem.Payments.Core.Entities;

namespace AirlineBookingSystem.Payments.Core.Repositories
{
    public interface IPaymentRepository
    {
        Task ProcessPaymentAsync(Payment payment);
        Task RefundPaymentsAsync(Guid id);
    }
}
