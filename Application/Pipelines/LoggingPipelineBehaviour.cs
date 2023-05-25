using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.Pipelines;

public class LoggingPipelineBehaviour<TIn, TOut> : IPipelineBehavior<TIn, TOut> where TIn : IRequest<TOut>
{
    #region Поля

    private ILogger<LoggingPipelineBehaviour<TIn, TOut>> _logger;

    #endregion

    #region Конструктор

    public LoggingPipelineBehaviour(ILogger<LoggingPipelineBehaviour<TIn, TOut>> logger)
    {
        _logger = logger;
    }

    #endregion

    #region Метод

    /// <summary>
    /// Метод выполняется каждый раз перед выполнением любого метода Mediatr
    /// </summary>
    /// <param name="request">Запрос, модель метода, который должен выполниться</param>
    /// <param name="next">Метод, который должен выполниться</param>
    /// <param name="cancellationToken">Токен</param>
    /// <returns>Возвращаемый тип, метода, который должен выполниться</returns>
    public async Task<TOut> Handle(TIn request, RequestHandlerDelegate<TOut> next, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Команда {TIn} начала выполнение", typeof(TIn));
        try
        {
            var result = await next();
            _logger.LogInformation("Команда {TIn} закончило выполнение", typeof(TIn));
            
            return result;
        }
        catch (Exception e)
        {
            _logger.LogWarning("Команда {TIn} закончило выполнение с ошибкой", typeof(TIn));
            throw;
        }
    }

    #endregion
}