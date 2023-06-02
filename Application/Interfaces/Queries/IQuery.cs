using MediatR;

namespace Application.Interfaces.Queries;

/// <summary>
/// Интерфейс для запросов
/// </summary>
/// <typeparam name="TOut">Тип возвращаемого значения</typeparam>
public interface IQuery<out TOut> : IRequest<TOut> { }