using System.Text;
using Application.Interfaces;
using Contracts.Interface;
using Infrastructure.Interfaces.Connections;
using Newtonsoft.Json;
using RabbitMQ.Client;

namespace Application.Services;

/// <summary>
/// Сервис для отправки сообщений в шину с помощью RabbitMq
/// </summary>
public class RabbitMqService: IPublishBusService<IMessage>
{
    #region Поле
    
    /// <summary>
    /// Подключение к RabbitMq
    /// </summary>
    private readonly IConnectionRabbitMq _connectRabbit;

    #endregion

    #region Конструктор

    public RabbitMqService(IConnectionRabbitMq connectRabbit)
    {
        _connectRabbit = connectRabbit;
    }

    #endregion

    #region Методы

    /// <summary>
    /// Отправка сообщений в очередь для пользователей
    /// </summary>
    /// <param name="classMessage">Класс - сообщение</param>
    public async Task SendMessageUserAsync(IMessage classMessage)
    {
        await Task.Run(() =>
        {
            var tempString = JsonConvert.SerializeObject(classMessage);

            _connectRabbit.Channel.QueueDeclare
            (
                queue: _connectRabbit.Settings.QueueUser,
                durable: true,
                exclusive: false,
                autoDelete: false,
                arguments: null
            );

            // Создание массива байтов из строки
            var body =  Encoding.UTF8.GetBytes(tempString);

            // Отправка сообщения
            _connectRabbit.Channel.BasicPublish
            (
                exchange: "",
                routingKey: _connectRabbit.Settings.QueueUser,
                basicProperties: null,
                body: body
            );
        });
    }
    
    /// <summary>
    /// Отправка сообщений в очередь для администраторов
    /// </summary>
    /// <param name="classMessage">Класс - сообщение</param>
    public async Task SendMessageAdminAsync(IMessage classMessage)
    {
        await Task.Run(() =>
        {
            var tempString = JsonConvert.SerializeObject(classMessage);

            _connectRabbit.ChannelAdmin.QueueDeclare
            (
                queue: _connectRabbit.Settings.QueueAdmin,
                durable: true,
                exclusive: false,
                autoDelete: false,
                arguments: null
            );

            // Создание массива байтов из строки
            var body =  Encoding.UTF8.GetBytes(tempString);

            // Отправка сообщения
            _connectRabbit.ChannelAdmin.BasicPublish
            (
                exchange: "",
                routingKey: _connectRabbit.Settings.QueueAdmin,
                basicProperties: null,
                body: body
            );
        });
    }

    #endregion
}