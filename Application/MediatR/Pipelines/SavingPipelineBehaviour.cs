using Application.Mediatr.Interfaces.Commands;
using Infrastructure;
using MediatR;
using Serilog;

namespace Application.Mediatr.Pipelines;

public class SavingPipelineBehaviour<TIn, TOut> : IPipelineBehavior<TIn, TOut> where TIn : IRequest<TOut>
{
    #region Поле

    private readonly Context _context;

    #endregion

    #region Конструктор

    public SavingPipelineBehaviour(Context context)
    {
        _context = context;
    }

    #endregion

    #region Метод

    /// <summary>
    /// Метод для сохранения данных в бд
    /// </summary>
    /// <param name="request">Запрос, который пришел</param>
    /// <param name="next">Метод, который должен выполниться</param>
    /// <param name="cancellationToken">Токен</param>
    /// <returns>Возвращаемый тип, метода, который должен выполниться</returns>
    public async Task<TOut> Handle(TIn request, RequestHandlerDelegate<TOut> next, CancellationToken cancellationToken)
    {
        var result = await next();
        
        if (request is ICommand<TOut>)
        {
            //Log.Information("Выполняется сохранение в базе данных");
            await _context.SaveChangesAsync();
        }

        return result;
    }

    #endregion
}