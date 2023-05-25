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