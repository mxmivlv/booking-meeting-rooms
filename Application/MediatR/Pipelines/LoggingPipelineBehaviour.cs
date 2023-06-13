using MediatR;
using Serilog;

namespace Application.Mediatr.Pipelines;

public class LoggingPipelineBehaviour<TIn, TOut> : IPipelineBehavior<TIn, TOut> where TIn : IRequest<TOut>
{
    #region Метод

    /// <summary>
    /// Метод для записи логов
    /// </summary>
    /// <param name="request">Запрос, который пришел</param>
    /// <param name="next">Метод, который должен выполниться</param>
    /// <param name="cancellationToken">Токен</param>
    /// <returns>Возвращаемый тип, метода, который должен выполниться</returns>
    public async Task<TOut> Handle(TIn request, RequestHandlerDelegate<TOut> next, CancellationToken cancellationToken)
    {
        Log.Information("Команда {TIn} начала выполнение", typeof(TIn));
        try
        {
            var result = await next();
            Log.Information("Команда {TIn} закончило выполнение", typeof(TIn));

            return result;
        }
        catch (Exception e)
        {
            Log.Warning("Команда {TIn} закончило выполнение", typeof(TIn));
            throw;
        }
    }

    #endregion
}