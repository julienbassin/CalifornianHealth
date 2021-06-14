﻿using CalendarApi.Domain.Models.DTOs;
using CalendarApi.Domain.Models.Entities;
using CalendarApi.Messaging.Receive.Options.v1;
using CalendarApi.Service.v1.Services;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CalendarApi.Messaging.Receive.Receiver.v1
{
    public class AppointmentUpdateReceiver : BackgroundService
    {
        private IModel _channel;
        private IConnection _connection;
        private readonly IAppointmentUpdateService _appointmentUpdateService;
        private readonly string _hostname;
        private readonly string _queueName;
        private readonly string _username;
        private readonly string _password;

        public AppointmentUpdateReceiver(IAppointmentUpdateService appointmentUpdateService, IOptions<RabbitMqConfiguration> rabbitMqOptions)
        {
            _hostname = rabbitMqOptions.Value.Hostname;
            _queueName = rabbitMqOptions.Value.QueueName;
            _username = rabbitMqOptions.Value.UserName;
            _password = rabbitMqOptions.Value.Password;
            _appointmentUpdateService = appointmentUpdateService;
            InitializeRabbitMqListener();
        }

        private void InitializeRabbitMqListener()
        {
            var factory = new ConnectionFactory
            {
                HostName = _hostname,
                UserName = _username,
                Password = _password
            };

            _connection = factory.CreateConnection();
            _connection.ConnectionShutdown += RabbitMQ_ConnectionShutdown;
            _channel = _connection.CreateModel();
            _channel.QueueDeclare(queue: _queueName, durable: false, exclusive: false, autoDelete: false, arguments: null);
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            stoppingToken.ThrowIfCancellationRequested();

            var consumer = new EventingBasicConsumer(_channel);
            consumer.Received += (ch, ea) =>
            {
                var content = Encoding.UTF8.GetString(ea.Body.ToArray());
                var appointmentModel = JsonConvert.DeserializeObject<AppointmentDTO>(content);

                HandleMessage(appointmentModel);

                _channel.BasicAck(ea.DeliveryTag, false);
            };
            consumer.Shutdown += OnConsumerShutdown;
            consumer.Registered += OnConsumerRegistered;
            consumer.Unregistered += OnConsumerUnregistered;
            consumer.ConsumerCancelled += OnConsumerCancelled;

            _channel.BasicConsume(_queueName, false, consumer);

            return Task.CompletedTask;
        }

        private async Task HandleMessage(AppointmentDTO appointment)
        {
            try
            {
                await _appointmentUpdateService.SaveAppointment(appointment);
                //return new BookingResponse { Response = "Ok" };
            }
            catch (Exception ex)
            {

                //return new BookingResponse { Response = ex.Message };
            }            
        }

        private void RabbitMQ_ConnectionShutdown(object sender, ShutdownEventArgs e)
        {

        }

        private void OnConsumerCancelled(object sender, ConsumerEventArgs e)
        {
            
        }

        private void OnConsumerUnregistered(object sender, ConsumerEventArgs e)
        {
            
        }

        private void OnConsumerRegistered(object sender, ConsumerEventArgs e)
        {
            
        }

        private void OnConsumerShutdown(object sender, ShutdownEventArgs e)
        {
            
        }
    }
}
