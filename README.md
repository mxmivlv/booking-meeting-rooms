    Итерация 1.
        1. Domain:
            - реализованно две модели по принципу богатой модели.
            - описан интерфейс для работы с бд.
        2. Infrastructure:
            - используется PostgreSQL.
            - работа с бд через Entity Framework, Fluent Api, с использованием миграции.
        3. Application:
            - используется MediatR, команды, запросы и модели.
            - реализован маппинг с помощью AutoMapper, описаны модели dto.
        4. Presentation:
            - реализован хостед сервис для разбронирования комнат.
            - используется два контроллера, DI контейнеры, swagger.
    Итерация 2.
        1. Infrastructure:
            - поменяна логика бронирования комнат, добавлена распределенная блокировка Redis.
            - добавлен класс ServiceCollectionExtensions.
        2. Presentation:
            - добавлен фильтр исключений.
            - добавлен класс ServiceCollectionExtensions.
            - настроен Logger с помощью serilog.
        3. Application:
            - добавлен класс ServiceCollectionExtensions.
            - реализован Pipelines с помощью MediatR (пишутся логи с информацией, какие методы выполняются).
    Итерация 3.
        1. Application:
            - реализован Pipelines с помощью MediatR (сохранение в бд).
            - добавлен сервис с распределенной блокировкой (блокировка была вынесена в этот сервис из репозитория).
        2. Presentation:
            - реализовано подключение к бд через строку из appsettings.json.
    Итерация 4.
        1. Application:
            - реализован Pipelines с помощью MediatR (отправка оповещения о новом бронировании) в шину RabbitMQ.
            - добавлен сервис с отправкой оповещения (если создана новая бронь и напоминания о бронировании) в шину RabbitMQ.
            - используется RabbitMQ.
        2. Infrastructure:
            - реализован новый класс для логики миграций.
            - реализовано подключение к шине.
            - вынесена логика подключения к Redis.
        3. Domain:
            - отредактированна модель bookingMeetingRoom (добавленно новое свойство).
        4. Presentation:
            - настройки приложения (строки подключения, логины, пароли и т.д.) вынесенны в appsettings.json.
            - используется IOptions.
            - добавлен новый hostedService (отправа напоминаний о бронировании).

        Реализовано второе приложение, которое служит для обработки сообщений из шины RabbitMQ и отправки в telegram.
        Приложение содержит: Application, Infrastructure, Presentation.
        Приложение называется Notification.

        5. Notification.Application:
            - реализована логика обработки сообщений из шины.
            - реализована логика отправки сообщений в telegram.
        6. Notification.Infrastructure:
            - реализовано подключение к шине.
            - реализовано подключение к telegram.
        7. Notification.Presentation:
            - реализован hostedService для прослушивания шины и отправки в telegram.
    Итерация 5.
        1. Application:
            - Реализована работа MassTransit.
        2. Notification.Application:
            - Реализована работа MassTransit.
            - Отправка сообщений происходит в канал с администраторами и в личные сообщения.
    Итерация 6.
        1. Infrastructure:
            - Реализовано подключение к Kafka.
            - Реализованы HealthCheck для шин, бд.
        2. Application:
            - Реализована отправка сообщений в шину Kafka.
            - Реализованы метрики OpenTelemetry в Prometheus.
            - Реализованы трассировки в Jaeger.
            - Реализован функционал по логированию в Elastic и Kibana.
            - Реализованы HealthCheckUI для просмотра.
        3. Notification.Infrastructure
            - Реализовано подключение к Kafka.
        4. Notification.Application:
            - Реализовано получение сообщений из шины Kafka и отправка в телеграм.

    *Все доп сервисы подняты локально в докере, кроме Prometheus и Kafka(подключение к Confluent Cloud)