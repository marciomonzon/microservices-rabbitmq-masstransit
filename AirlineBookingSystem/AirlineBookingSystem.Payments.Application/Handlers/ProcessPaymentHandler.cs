﻿using AirlineBookingSystem.BuildingBlocks.Contracts.EventBus.Messages;
using AirlineBookingSystem.Payments.Application.Commands;
using AirlineBookingSystem.Payments.Core.Entities;
using AirlineBookingSystem.Payments.Core.Repositories;
using MassTransit;
using MediatR;

namespace AirlineBookingSystem.Payments.Application.Handlers
{
    public class ProcessPaymentHandler : IRequestHandler<ProcessPaymentCommand, Guid>
    {
        private readonly IPaymentRepository _paymentRepository;
        private readonly IPublishEndpoint _publishEndpoint;

        public ProcessPaymentHandler(IPaymentRepository paymentRepository, IPublishEndpoint publishEndpoint)
        {
            _paymentRepository = paymentRepository;
            _publishEndpoint = publishEndpoint;
        }

        public async Task<Guid> Handle(ProcessPaymentCommand request, CancellationToken cancellationToken)
        {
            var payment = new Payment
            {
                Id = Guid.NewGuid(),
                Amount = request.Amount,
                BookingId = request.BookingId,
                PaymentDate = DateTime.UtcNow
            };

            await _paymentRepository.ProcessPaymentAsync(payment);

            await _publishEndpoint.Publish(new PaymentProcessedEvent(payment.Id,
                                                                     payment.BookingId,
                                                                     payment.Amount,
                                                                     payment.PaymentDate));

            return payment.Id;
        }
    }
}
